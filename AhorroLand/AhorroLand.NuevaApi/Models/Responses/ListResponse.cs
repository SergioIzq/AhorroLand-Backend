namespace AhorroLand.NuevaApi.Models.Responses;

/// <summary>
/// Respuesta para listas simples (no paginadas).
/// Usada para endpoints como /search y /recent.
/// </summary>
/// <typeparam name="T">Tipo de elementos en la lista</typeparam>
public sealed record ListResponse<T>
{
    /// <summary>
    /// Lista de elementos
    /// </summary>
    public IEnumerable<T> Items { get; init; }

    /// <summary>
    /// Total de elementos en la lista
    /// </summary>
    public int Count { get; init; }

    public ListResponse(IEnumerable<T> items)
    {
      Items = items;
        Count = items?.Count() ?? 0;
  }

/// <summary>
    /// Crea una respuesta vacía
    /// </summary>
    public static ListResponse<T> Empty()
  => new(Enumerable.Empty<T>());
}
