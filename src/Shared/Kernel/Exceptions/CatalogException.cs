namespace Kernel.Exceptions;

public class ValidationModelException : Exception
{
    public ValidationModelException(string message)
        : base(message)
    {
        
    }
}