namespace Catalog.Application.Common.Models.Responses;

public record CategoryResponse(
    Guid Id,
    string Name,
    Guid? ParentId);
