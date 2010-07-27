using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Shared.Interfaces;
using Shared.MVVM;
using Shared.Validating;
using Shared.Extensions;
using System.Text.RegularExpressions;
using Val = Shared.Validating.Validation;
using System.Windows.Input;

namespace Validation.ViewModels
{
    public class ValidatingViewModel : NotifyPropertyChanged, IDataErrorInfo
    {
        int _age;
        IDataErrorInfoProvider _dataErrorInfoProvider;
        string _firstName;
        string _lastName;
        ICommand _submitCommand;

        public ValidatingViewModel(IDataErrorInfoProvider dataErrorInfoProvider)
        {
            _dataErrorInfoProvider = dataErrorInfoProvider;

            SetupValidations();
            _dataErrorInfoProvider.ValidateAll();
        }

        public int Age
        {
            get { return _age; }
            set
            {
                _age = value;
                RaisePropertyChanged(() => Age);
            }
        }

        public IList<string> AllErrors
        {
            get { return _dataErrorInfoProvider.AllErrors; }
        }

        public string Error
        {
            get { return null; }
        }

        public IDictionary<string, IList<string>> Errors
        {
            get { return _dataErrorInfoProvider.ErrorsForProperty; }
        }

        public string FirstName
        {
            get { return _firstName ?? string.Empty; }
            set
            {
                _firstName = value;
                RaisePropertyChanged(() => FirstName);
            }
        }

        public string LastName
        {
            get { return _lastName ?? string.Empty; }
            set
            {
                _lastName = value;
                RaisePropertyChanged(() => LastName);
            }
        }

        public ICommand SubmitCommand
        {
            get
            {
                return _submitCommand ?? (_submitCommand = new SimpleCommand
                {
                    ExecuteDelegate = _ => Console.WriteLine("Submitting"),
                    CanExecuteDelegate = _ => AllErrors.Count == 0
                });
            }
        }

        public string this[string propertyName]
        {
            get { return _dataErrorInfoProvider.Validate(propertyName); }
        }

        void SetupValidations()
        {
            _dataErrorInfoProvider.AddValidations(() => FirstName,
                () => FirstName.MustHaveMinimumLengthOf(2),
                () => FirstName.CanOnlyContainLetters(),
                () => FirstName.NeedsToStartWithCapitalLetter(),
                () => FirstName.CannotHaveLeadingWhiteSpaces());

            _dataErrorInfoProvider.AddValidations(() => LastName,
                () => LastName.MustHaveMinimumLengthOf(2),
                () => LastName.CanOnlyContainLetters(),
                () => LastName.NeedsToStartWithCapitalLetter(),
                () => LastName.CannotHaveLeadingWhiteSpaces(),
                () => new Val("{0} cannot be the same as the first name", LastName.IsEmpty() || !LastName.Equals(FirstName)));

            _dataErrorInfoProvider.AddValidations(() => Age,
                () => Age.MustBeGreaterOrEqualTo(18),
                () => Age.MustBeSmallerOrEqualTo(120));

        }
    }
}
