
using Kernel.Contexts;

public abstract class DomainEvent
{
    public DomainEvent(DateTime createDate, IOperationContext operationContext)
    {
        if (createDate == DateTime.MinValue)
            throw new ArgumentException("Create date can't be empty");
        if (operationContext is null)
            throw new ArgumentNullException(nameof(operationContext));

        CreateDate = createDate;
        OperationContext = operationContext;
    }

    public DateTime CreateDate { get; }
    public IOperationContext OperationContext { get; }
}
