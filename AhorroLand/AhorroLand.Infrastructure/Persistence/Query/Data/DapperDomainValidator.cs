using AhorroLand.Shared.Domain.Abstractions;
using AhorroLand.Shared.Domain.Interfaces;
using Dapper;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;

namespace AhorroLand.Infrastructure.DataAccess;

public class DapperDomainValidator : IDomainValidator
{
    private readonly IDbConnection _dbConnection;

    public DapperDomainValidator(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    // ✅ CAMBIO 1: Ajustar la firma para aceptar <TEntity, TId>
    public async Task<bool> ExistsAsync<TEntity, TId>(TId id)
        where TEntity : AbsEntity<TId>
        where TId : IGuidValueObject
    {
        // 1. Asegurar conexión
        if (_dbConnection.State != ConnectionState.Open)
        {
            _dbConnection.Open();
        }

        // 2. Obtener tabla
        var tableName = GetTableName<TEntity>();

        // 3. ✅ CAMBIO 2: Obtener el valor primitivo real para la DB
        // Si TId es 'ClienteId', necesitamos el Guid de adentro.
        // Si TId es 'Guid', usamos el valor directo.
        var realIdValue = ExtractPrimitiveValue(id);

        // 4. Query (Nota: asume que la columna PK se llama 'id')
        var sql = $"SELECT 1 FROM {tableName} WHERE id = @Id LIMIT 1";

        // 5. Ejecutar usando el valor primitivo
        var result = await _dbConnection.ExecuteScalarAsync<int?>(sql, new { Id = realIdValue });

        return result.HasValue;
    }

    // --- Métodos Privados Auxiliares ---

    private static string GetTableName<TEntity>()
    {
        var type = typeof(TEntity);
        var tableAttr = type.GetCustomAttribute<TableAttribute>();

        if (tableAttr != null && !string.IsNullOrEmpty(tableAttr.Name))
        {
            return tableAttr.Name;
        }
        return type.Name.ToLower() + "s";
    }

    /// <summary>
    /// Extrae el valor primitivo si 'id' es un Value Object (tiene propiedad 'Value').
    /// Si es un tipo simple (Guid, int, string), lo devuelve tal cual.
    /// </summary>
    private static object ExtractPrimitiveValue<TId>(TId id)
    {
        if (id == null) return DBNull.Value;

        var type = typeof(TId);

        // Si es un tipo primitivo, string o Guid, devolverlo directamente
        if (type.IsPrimitive || type == typeof(string) || type == typeof(Guid) || type == typeof(decimal))
        {
            return id;
        }

        // Si es un Value Object (ej. ClienteId), buscamos la propiedad "Value"
        var valueProp = type.GetProperty("Value");
        if (valueProp != null)
        {
            var value = valueProp.GetValue(id);
            return value ?? DBNull.Value;
        }

        // Si no tiene propiedad Value, intentamos devolver el objeto (quizás tiene un TypeHandler registrado)
        return id;
    }
}