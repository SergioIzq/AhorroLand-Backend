namespace AhorroLand.Shared.Application.Dtos
{
    /// <summary>
    /// Representación de la categoría para ser enviada fuera de la capa de aplicación.
    /// </summary>
    public record CategoriaDto
    {
        public Guid Id { get; init; }
        public string Nombre { get; init; } = string.Empty;
        public string? Descripcion { get; init; }
        public Guid UsuarioId { get; init; }
        public DateTime FechaCreacion { get; init; }
    }
}
