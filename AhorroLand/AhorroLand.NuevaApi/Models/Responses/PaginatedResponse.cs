namespace AhorroLand.NuevaApi.Models.Responses;

/// <summary>
/// Respuesta paginada que incluye metadata de paginación.
/// </summary>
/// <typeparam name="T">Tipo de elementos en la lista</typeparam>
public sealed record PaginatedResponse<T>
{
    /// <summary>
    /// Lista de elementos de la página actual
    /// </summary>
    public IEnumerable<T> Items { get; init; }

    /// <summary>
    /// Total de elementos en todas las páginas
    /// </summary>
    public int Total { get; init; }

    /// <summary>
    /// Número de página actual (base 1)
    /// </summary>
    public int Page { get; init; }

    /// <summary>
    /// Tamaño de página (elementos por página)
    /// </summary>
    public int PageSize { get; init; }

    /// <summary>
    /// Total de páginas disponibles
    /// </summary>
    public int TotalPages { get; init; }

    /// <summary>
    /// Indica si hay una página anterior
/// </summary>
    public bool HasPreviousPage { get; init; }

    /// <summary>
    /// Indica si hay una página siguiente
    /// </summary>
    public bool HasNextPage { get; init; }

    public PaginatedResponse(
        IEnumerable<T> items,
   int total,
        int page,
        int pageSize)
    {
        Items = items;
        Total = total;
        Page = page;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(total / (double)pageSize);
    HasPreviousPage = page > 1;
        HasNextPage = page < TotalPages;
    }
}
