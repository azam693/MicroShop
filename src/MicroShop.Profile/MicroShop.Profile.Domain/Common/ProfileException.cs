namespace MicroShop.Profile.Domain.Common;

public sealed class ProfileException : Exception
{
    public ProfileException(string message)
        : base(message)
    {
        
    }
}
