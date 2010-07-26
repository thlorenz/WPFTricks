using System;
namespace Callbacks.Interfaces
{
    public interface IUser
    {
        string Email { get; set; }

        string FirstName { get; set; }

        string LastName { get; set; }
    }
}
