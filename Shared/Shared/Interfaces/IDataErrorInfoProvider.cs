using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Shared.Interfaces
{
    public interface IDataErrorInfoProvider : IFluentInterface
    {
        IList<string> AllErrors { get; }
        IDictionary<string, IList<string>> ErrorsForProperty { get; }

        void AddValidations<TValue>(Expression<Func<TValue>> propertySelector, params Func<IValidation>[] validations);
        void AddValidations(string propertyName, params Func<IValidation>[] validations);

        void ClearAllErrors();

        void ClearErrorsOf(string propertyName);

        string Validate(string propertyName);

        string ValidateAll();
    }
}
