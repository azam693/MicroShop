using Dawn;
using Kernel.Entities;

namespace Catalog.Domain.Entities.Combinations;

public class Combination : GuidEntity
{
    private Combination() { }

    public Combination(string name)
    {
        Name = Guard.Argument(name, nameof(name)).NotWhiteSpace();
    }

    public string Name { get; protected set; }
}