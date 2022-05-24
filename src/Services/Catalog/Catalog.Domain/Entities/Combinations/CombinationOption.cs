using Dawn;
using Kernel.Entities;

namespace Catalog.Domain.Entities.Combinations;

public class CombinationOption : GuidEntity
{
    private CombinationOption() { }

    public CombinationOption(
        string name,
        Combination combination)
    {
        Name = Guard.Argument(name, nameof(name)).NotWhiteSpace();
        Combination = Guard.Argument(combination, nameof(combination)).NotNull();
    }

    public string Name { get; protected set; }

    public Combination Combination { get; protected set; }
}
