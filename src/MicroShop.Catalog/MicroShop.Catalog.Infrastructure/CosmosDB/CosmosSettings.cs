namespace MicroShop.Catalog.Infrastructure.CosmosDB;

public sealed record CosmosSettings
{
    public string Account { get; init; }
    public string PrimaryKey { get; init; }
    public string DatabaseName { get; init; }
}
