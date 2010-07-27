using System;
namespace Shared.Interfaces
{
    public interface IValidation
    {
        Func<bool> Condition { get; }
        string Description { get; }
    }
}
