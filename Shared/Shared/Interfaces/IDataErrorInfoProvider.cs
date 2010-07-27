using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Shared.Interfaces
{
    public interface IDataErrorInfoProvider : IFluentInterface
    {
        IList<string> AllErrors { get; }

        void AddValidations<TValue>(Expression<Func<TValue>> propertySelector, params IValidation[] validations);

        void AddValidation<TValue>(Expression<Func<TValue>> propertySelector, string description, Func<bool> condition);

        void AddValidation(string propertyName, string description, Func<bool> condition);

        void AddValidations<TValue>(Expression<Func<TValue>> propertySelector, IDictionary<string, Func<bool>> validations);

        void AddValidations(string propertyName, IDictionary<string, Func<bool>> validations);

        void ClearAllErrors();

        void ClearErrorsOf(string propertyName);

        string Validate(string propertyName);

        string ValidateAll();
    }
}
