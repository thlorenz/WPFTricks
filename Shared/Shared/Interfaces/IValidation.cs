using System;
namespace Shared.Interfaces
{
    public interface IValidation
    {
        bool Condition { get; }
        string Description { get; }
    }
}
