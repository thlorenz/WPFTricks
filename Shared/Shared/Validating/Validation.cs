using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shared.Interfaces;

namespace Shared.Validating
{
    public class Validation : IValidation
    {
        Func<bool> _condition;
        string _description;

        public Validation(string description, Func<bool> condition)
        {
            _condition = condition;
            _description = description;
        }

        public Func<bool> Condition
        {
            get { return _condition; }
        }

        public string Description
        {
            get { return _description; }
        }
    }
}
