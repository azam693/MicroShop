namespace Catalog.Application.Common.Models.Responses;

public record ProductResponse(
    Guid Id,
    string Name,
    string Description);
