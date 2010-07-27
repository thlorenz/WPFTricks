using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using Shared.Extensions;
using Shared.Interfaces;

namespace Shared.Validating
{
    public class DataErrorInfoProvider : IDataErrorInfoProvider
    {
        IList<string> _allErrors = new ObservableCollection<string>();
        IDictionary<string, IList<IValidation>> _validations = new Dictionary<string, IList<IValidation>>();

        public IList<string> AllErrors
        {
            get { return _allErrors; }
        }

        public void AddValidation<TValue>(Expression<Func<TValue>> propertySelector, string description, Func<bool> condition)
        {
            var memberExpression = propertySelector.Body as MemberExpression;
            if (memberExpression != null)
            {
                AddValidation(memberExpression.Member.Name, description, condition);
            }
        }

        public void AddValidation(string propertyName, string description, Func<bool> condition)
        {
            AddValidations(propertyName, new Dictionary<string, Func<bool>> { { description, condition } });
        }

        public void AddValidations<TValue>(Expression<Func<TValue>> propertySelector, IDictionary<string, Func<bool>> validations)
        {
            var memberExpression = propertySelector.Body as MemberExpression;
            if (memberExpression != null)
            {
                AddValidations(memberExpression.Member.Name, validations);
            }
        }

        public void AddValidations(string propertyName, IDictionary<string, Func<bool>> validationsDic)
        { 
            var validations =  validationsDic
                .Select(item => new Validation(item.Key, item.Value))
                .Cast<IValidation>();

            AddValidations(propertyName, validations);
        }

        public void AddValidations<TValue>(Expression<Func<TValue>> propertySelector, params IValidation[] validations)
        {
            AddValidations(propertySelector, validations.Cast<IValidation>());
        }

        public void AddValidations<TValue>(Expression<Func<TValue>> propertySelector, IEnumerable<IValidation> validations)
        {
            var memberExpression = propertySelector.Body as MemberExpression;
            if (memberExpression != null)
            {
                AddValidations(memberExpression.Member.Name, validations);
            }
        }

        public void AddValidations(string propertyName, IEnumerable<IValidation> validations)
        {
            if (!_validations.ContainsKey(propertyName))
                _validations.Add(propertyName, new List<IValidation>());

            validations.ForEach(_validations[propertyName].Add);
        }

        public void ClearAllErrors()
        {
            _allErrors.Clear();
        }

        public void ClearErrorsOf(string propertyName)
        { }

        public string Validate(string propertyName)
        {
            Debug.Assert(!string.IsNullOrEmpty(propertyName));
         
            var propertyErrors = new List<string>();

            if (_validations.ContainsKey(propertyName))
                _validations[propertyName].ForEach(validation => {
                    if (!validation.Condition())
                    {
                        var descriptionWithName = string.Format(validation.Description, propertyName);
                        propertyErrors.Add(descriptionWithName);
                        if (!_allErrors.Contains(descriptionWithName))
                            _allErrors.Add(descriptionWithName);
                    }
                });

            return propertyErrors.IsNotEmpty()
                ? propertyErrors.Aggregate((errorsSoFar, extraErrors) => errorsSoFar += extraErrors + "\n")
                : null;
        }

        public string ValidateAll()
        {
            _allErrors.Clear();
            _validations.Keys.ForEach(propertyName => Validate(propertyName));

            return _allErrors.IsNotEmpty()
                ? _allErrors.Aggregate((errorsSoFar, extraErrors) => errorsSoFar += extraErrors + "\n")
                : null; 
        }
    }
}
