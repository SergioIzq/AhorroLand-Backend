using AhorroLand.Shared.Domain.Abstractions;
using AhorroLand.Shared.Domain.Abstractions.Results;
using AhorroLand.Shared.Domain.Interfaces;
using MediatR;

namespace AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;

/// <summary>
/// Query base abstracta para búsquedas de autocomplete.
/// Diseñada para ser ultra-rápida (<10ms) con resultados limitados.
/// </summary>
public abstract record SearchForAutocompleteQuery<TEntity, TDto, TId> : IRequest<Result<IEnumerable<TDto>>>
    where TEntity : AbsEntity<TId>
    where TDto : class
    where TId: IGuidValueObject
{
    public Guid? UsuarioId { get; init; }
    public string SearchTerm { get; init; }
    public int Limit { get; init; }

    protected SearchForAutocompleteQuery(string searchTerm, int limit = 10)
    {
        SearchTerm = searchTerm;
        Limit = limit > 50 ? 50 : limit; // Máximo 50 resultados
    }
}
