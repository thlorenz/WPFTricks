using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Callbacks.Interfaces;

namespace Callbacks.DomainModel
{
    public class User : IUser
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
