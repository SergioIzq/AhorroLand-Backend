using AhorroLand.Shared.Domain.Abstractions;
using AhorroLand.Shared.Domain.Abstractions.Results;
using MediatR;

namespace AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;

/// <summary>
/// Query base abstracta para obtener elementos recientes.
/// Diseñada para ser ultra-rápida (<10ms) con resultados limitados.
/// Ideal para pre-cargar selectores con los elementos usados recientemente.
/// </summary>
public abstract record GetRecentQuery<TEntity, TDto> : IRequest<Result<IEnumerable<TDto>>>
    where TEntity : AbsEntity
    where TDto : class
{
    public Guid? UsuarioId { get; init; }
    public int Limit { get; init; }

    protected GetRecentQuery(int limit = 5)
    {
        Limit = limit > 50 ? 50 : limit; // Máximo 50 resultados
    }
}
