using AhorroLand.Domain;
using AhorroLand.Infrastructure.Persistence.Query;
using AhorroLand.Shared.Application.Dtos;
using AhorroLand.Shared.Domain.ValueObjects.Ids;

namespace AhorroLand.Infrastructure.Persistence.Data.IngresosProgramados
{
    public class IngresoProgramadoReadRepository : AbsReadRepository<IngresoProgramado, IngresoProgramadoDto, IngresoProgramadoId>, IIngresoProgramadoReadRepository
    {
        public IngresoProgramadoReadRepository(IDbConnectionFactory dbConnectionFactory)
            : base(dbConnectionFactory, "ingresos_programados")
        {
        }

        protected override string GetTableAlias() => "ip";

        protected override string BuildCountQuery()
        {
            return @"SELECT COUNT(*) FROM ingresos_programados ip
LEFT JOIN conceptos con ON ip.id_concepto = con.id
LEFT JOIN categorias cat ON con.id_categoria = cat.id
LEFT JOIN clientes cli ON ip.id_cliente = cli.id
LEFT JOIN personas per ON ip.id_persona = per.id
LEFT JOIN cuentas cta ON ip.id_cuenta = cta.id
LEFT JOIN formas_pago fp ON ip.id_forma_pago = fp.id";
        }

        protected override string BuildGetByIdQuery()
        {
            return @"
SELECT 
    ip.id as Id,
    ip.importe as Importe,
    ip.fecha_ejecucion as FechaEjecucion,
    ip.descripcion as Descripcion,
    ip.frecuencia as Frecuencia,
    ip.activo as Activo,
    ip.hangfire_job_id as HangfireJobId,
    ip.id_concepto as ConceptoId,
    COALESCE(con.nombre, '') as ConceptoNombre,
    con.id_categoria as CategoriaId,
    COALESCE(cat.nombre, '') as CategoriaNombre,
    ip.id_cliente as ClienteId,
    COALESCE(cli.nombre, '') as ClienteNombre,
    ip.id_persona as PersonaId,
    COALESCE(per.nombre, '') as PersonaNombre,
    ip.id_cuenta as CuentaId,
    COALESCE(cta.nombre, '') as CuentaNombre,
    ip.id_forma_pago as FormaPagoId,
    COALESCE(fp.nombre, '') as FormaPagoNombre,
    ip.id_usuario as UsuarioId
FROM ingresos_programados ip
LEFT JOIN conceptos con ON ip.id_concepto = con.id
LEFT JOIN categorias cat ON con.id_categoria = cat.id
LEFT JOIN clientes cli ON ip.id_cliente = cli.id
LEFT JOIN personas per ON ip.id_persona = per.id
LEFT JOIN cuentas cta ON ip.id_cuenta = cta.id
LEFT JOIN formas_pago fp ON ip.id_forma_pago = fp.id
WHERE ip.id = @id";
        }

        protected override string BuildGetAllQuery()
        {
            return @"
SELECT 
    ip.id as Id,
    ip.importe as Importe,
    ip.fecha_ejecucion as FechaEjecucion,
    ip.descripcion as Descripcion,
    ip.frecuencia as Frecuencia,
    ip.activo as Activo,
    ip.hangfire_job_id as HangfireJobId,
    ip.id_concepto as ConceptoId,
    COALESCE(con.nombre, '') as ConceptoNombre,
    con.id_categoria as CategoriaId,
    COALESCE(cat.nombre, '') as CategoriaNombre,
    ip.id_cliente as ClienteId,
    COALESCE(cli.nombre, '') as ClienteNombre,
    ip.id_persona as PersonaId,
    COALESCE(per.nombre, '') as PersonaNombre,
    ip.id_cuenta as CuentaId,
    COALESCE(cta.nombre, '') as CuentaNombre,
    ip.id_forma_pago as FormaPagoId,
    COALESCE(fp.nombre, '') as FormaPagoNombre,
    ip.id_usuario as UsuarioId
FROM ingresos_programados ip
LEFT JOIN conceptos con ON ip.id_concepto = con.id
LEFT JOIN categorias cat ON con.id_categoria = cat.id
LEFT JOIN clientes cli ON ip.id_cliente = cli.id
LEFT JOIN personas per ON ip.id_persona = per.id
LEFT JOIN cuentas cta ON ip.id_cuenta = cta.id
LEFT JOIN formas_pago fp ON ip.id_forma_pago = fp.id";
        }

        protected override string BuildGetPagedQuery()
        {
            return BuildGetAllQuery();
        }

        protected override string GetDefaultOrderBy()
        {
            return "ORDER BY ip.fecha_ejecucion DESC, ip.id DESC";
        }

        protected override Dictionary<string, string> GetSortableColumns()
        {
            return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "FechaEjecucion", "ip.fecha_ejecucion" },
                { "Importe", "ip.importe" },
                { "ConceptoNombre", "con.nombre" },
                { "CategoriaNombre", "cat.nombre" },
                { "ClienteNombre", "cli.nombre" },
                { "PersonaNombre", "per.nombre" },
                { "CuentaNombre", "cta.nombre" },
                { "FormaPagoNombre", "fp.nombre" },
                { "Frecuencia", "ip.frecuencia" },
                { "Activo", "ip.activo" }
            };
        }

        protected override List<string> GetSearchableColumns()
        {
            return new List<string>
            {
                "ip.descripcion",
                "con.nombre",
                "cat.nombre",
                "cli.nombre",
                "per.nombre",
                "cta.nombre",
                "fp.nombre",
                "ip.frecuencia"
            };
        }

        protected override List<string> GetNumericSearchableColumns()
        {
            return new List<string>
            {
                "ip.importe"
            };
        }

        protected override List<string> GetDateSearchableColumns()
        {
            return new List<string>
            {
                "ip.fecha_ejecucion"
            };
        }
    }
}