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
        IDictionary<string, IList<string>> _errorsForProperty = new Dictionary<string, IList<string>>();
        IDictionary<string, IList<Func<IValidation>>> _validations = new Dictionary<string, IList<Func<IValidation>>>();

        public IList<string> AllErrors
        {
            get { return _allErrors; }
        }

        public IDictionary<string, IList<string>> ErrorsForProperty
        {
            get { return _errorsForProperty; }
        }

        public void AddValidations<TValue>(Expression<Func<TValue>> propertySelector, params Func<IValidation>[] validations)
        {
            var memberExpression = propertySelector.Body as MemberExpression;
            if (memberExpression != null)
                AddValidations(memberExpression.Member.Name, validations);
        }

        public void AddValidations(string propertyName, params Func<IValidation>[] validations)
        {
            if (!_validations.ContainsKey(propertyName))
                _validations.Add(propertyName, new List<Func<IValidation>>());

            validations.ForEach(_validations[propertyName].Add);
        }

        public void ClearAllErrors()
        {
            _allErrors.Clear();
        }

        public void ClearErrorsOf(string propertyName)
        {
            if (_errorsForProperty.ContainsKey(propertyName))
                _errorsForProperty[propertyName].Clear();
        }

        public string Validate(string propertyName)
        {
            Debug.Assert(!string.IsNullOrEmpty(propertyName));

            if (!_errorsForProperty.ContainsKey(propertyName))
                _errorsForProperty.Add(propertyName, new ObservableCollection<string>());
            else
                _errorsForProperty[propertyName].Clear();

            if (_validations.ContainsKey(propertyName))
            {
                _validations[propertyName].ForEach(getValidation => {

                    var validation = getValidation();
                    var descriptionWithName = string.Format(validation.Description, propertyName);

                    if (!validation.Condition)
                    {
                        if (!_errorsForProperty[propertyName].Contains(descriptionWithName))
                            _errorsForProperty[propertyName].Add(descriptionWithName);
                    }
                });

                UpdateAllErrors();
            }

            return _errorsForProperty[propertyName].IsNotEmpty()
                ? _errorsForProperty[propertyName].Aggregate((errorsSoFar, extraErrors) => errorsSoFar += extraErrors + "\n")
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
        void UpdateAllErrors()
        {
            _allErrors.Clear();
            _errorsForProperty.Values
                .ForEach(items => items.ForEach(_allErrors.Add));
        }
    }
}
