using AhorroLand.Domain;
using AhorroLand.Shared.Application.Abstractions.Messaging.Abstracts.Queries;
using AhorroLand.Shared.Application.Dtos;

namespace AhorroLand.Application.Features.Personas.Queries;

/// <summary>
/// Query para búsqueda rápida de personas (autocomplete).
/// </summary>
public sealed record SearchPersonasQuery : SearchForAutocompleteQuery<Persona, PersonaDto>
{
    public SearchPersonasQuery(string searchTerm, int limit = 10)
        : base(searchTerm, limit)
    {
    }
}
