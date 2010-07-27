using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shared.Interfaces;

namespace Shared.Validating
{
    public class Validation : IValidation
    {
        public Validation(string description, bool condition)
        {
            Condition = condition;
            Description = description;
        }

        public bool Condition { get; private set; }

        public string Description { get; private set; }
    }
}
