using AhorroLand.Domain;
using AhorroLand.Infrastructure.Persistence.Query;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.ValueObjects;
using Dapper;

namespace AhorroLand.Infrastructure.Persistence.Data.Conceptos
{
    public class ConceptoReadRepository : AbsReadRepository<Concepto, ConceptoDto>, IConceptoReadRepository
    {
        public ConceptoReadRepository(IDbConnectionFactory dbConnectionFactory)
      : base(dbConnectionFactory, "conceptos")
   {
        }

    /// <summary>
      /// 🔥 Alias de la tabla principal para usar en JOINs.
        /// </summary>
        protected override string GetTableAlias()
      {
            return "c";
        }

 /// <summary>
        /// 🔥 Query de conteo que usa el alias correcto y los JOINs necesarios.
      /// </summary>
        protected override string BuildCountQuery()
        {
         return @"SELECT COUNT(*) FROM conceptos c
LEFT JOIN categorias cat ON c.id_categoria = cat.id";
        }

        /// <summary>
   /// 🔥 Query específico para Concepto con todas sus columnas incluyendo CategoriaNombre.
      /// </summary>
        protected override string BuildGetByIdQuery()
        {
        return @"
SELECT 
    c.id as Id,
    c.nombre as Nombre,
    c.id_categoria as CategoriaId,
    COALESCE(cat.nombre, '') as CategoriaNombre,
    c.id_usuario as UsuarioId
FROM conceptos c
LEFT JOIN categorias cat ON c.id_categoria = cat.id
WHERE c.id = @id";
    }

      /// <summary>
        /// 🔥 Query para obtener todos los conceptos con CategoriaNombre.
        /// </summary>
        protected override string BuildGetAllQuery()
   {
            return @"
SELECT 
    c.id as Id,
 c.nombre as Nombre,
 c.id_categoria as CategoriaId,
    COALESCE(cat.nombre, '') as CategoriaNombre,
    c.id_usuario as UsuarioId
FROM conceptos c
LEFT JOIN categorias cat ON c.id_categoria = cat.id";
     }

    /// <summary>
        /// 🔥 IMPORTANTE: Query para paginación (debe incluir los mismos JOINs).
        /// </summary>
        protected override string BuildGetPagedQuery()
        {
      return BuildGetAllQuery();
   }

        /// <summary>
      /// 🔥 ORDER BY por nombre ascendente.
/// </summary>
        protected override string GetDefaultOrderBy()
        {
            return "ORDER BY c.nombre ASC";
        }

        /// <summary>
        /// 🔥 Columna WHERE para filtrar por usuario (con alias).
        /// </summary>
        protected override string GetUserIdColumn()
   {
       return "c.id_usuario";
        }

        /// <summary>
        /// 🔥 NUEVO: Define las columnas por las que se puede ordenar.
        /// </summary>
        protected override Dictionary<string, string> GetSortableColumns()
   {
 return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
   {
                { "Nombre", "c.nombre" },
  { "CategoriaNombre", "cat.nombre" },
        { "FechaCreacion", "c.fecha_creacion" }
       };
        }

        /// <summary>
      /// 🔥 NUEVO: Define las columnas en las que se puede buscar.
        /// </summary>
        protected override List<string> GetSearchableColumns()
        {
        return new List<string>
      {
      "c.nombre",
         "cat.nombre"
};
        }

   public async Task<bool> ExistsWithSameNameAsync(Nombre nombre, UsuarioId usuarioId, CancellationToken cancellationToken = default)
  {
     using var connection = _dbConnectionFactory.CreateConnection();

 const string sql = @"
SELECT EXISTS(
    SELECT 1 
    FROM conceptos 
    WHERE nombre = @Nombre AND id_usuario = @UsuarioId
) as Exists";

            var exists = await connection.ExecuteScalarAsync<bool>(
   new CommandDefinition(sql,
          new { Nombre = nombre.Value, UsuarioId = usuarioId.Value },
           cancellationToken: cancellationToken));

  return exists;
 }

        public async Task<bool> ExistsWithSameNameExceptAsync(Nombre nombre, UsuarioId usuarioId, Guid excludeId, CancellationToken cancellationToken = default)
        {
       using var connection = _dbConnectionFactory.CreateConnection();

          const string sql = @"
SELECT EXISTS(
SELECT 1 
    FROM conceptos 
    WHERE nombre = @Nombre AND id_usuario = @UsuarioId AND id != @ExcludeId
) as Exists";

          var exists = await connection.ExecuteScalarAsync<bool>(
    new CommandDefinition(sql,
    new { Nombre = nombre.Value, UsuarioId = usuarioId.Value, ExcludeId = excludeId },
 cancellationToken: cancellationToken));

 return exists;
        }
    }
}