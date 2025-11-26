using AhorroLand.Shared.Domain.Abstractions;
using AhorroLand.Shared.Domain.Abstractions.Results;
using AhorroLand.Shared.Domain.Interfaces;
using MediatR;

namespace AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Commands;

/// <summary>
/// Comando base genérico para operaciones de Actualización.
/// </summary>
/// <typeparam name="TEntity">La Entidad de Dominio que se va a actualizar.</typeparam>
/// <typeparam name="TDto">El DTO de respuesta que se espera.</typeparam>
public abstract record AbsUpdateCommand<TEntity, TId, TDto> : IRequest<Result<TDto>>
    where TEntity : AbsEntity<TId>
    where TId : IGuidValueObject
{
    // Propiedad requerida para identificar la entidad a actualizar.
    public Guid Id { get; init; }
}
