using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Shared.Interfaces;
using Shared.MVVM;
using Shared.Validating;
using System.Text.RegularExpressions;
using It = Shared.Validating.DataErrorInfoDescriptions;
using Val = Shared.Validating.Validation;

namespace Validation.ViewModels
{
    public class ValidatingViewModel : NotifyPropertyChanged, IDataErrorInfo
    {
        IDataErrorInfoProvider _dataErrorInfoProvider;
        string _firstName;

        public ValidatingViewModel(IDataErrorInfoProvider dataErrorInfoProvider)
        {
            _dataErrorInfoProvider = dataErrorInfoProvider;

            SetupValidations();
            _dataErrorInfoProvider.ValidateAll();
        }

        void SetupValidations()
        {
            //_dataErrorInfoProvider.AddValidations(() => FirstName, new Dictionary<string, Func<bool>> { 
            //    {It.CanOnlyContainLetters, () => FirstName.CanOnlyContainLetters() },
            //    {It.NeedsToStartWithCapitalLetter, () => FirstName .NeedsToStartWithCapitalLetter() },
            //    { It.CannotHaveLeadingWhiteSpaces, () => FirstName.CannotHaveLeadingWhiteSpaces()},
            //    { "{0} must be at least two letters long!" , () => FirstName.Trim().Length > 1 },
            //    { "{0} cannot be empty!", FirstName.CannotBeEmpty() }
            // });

            _dataErrorInfoProvider.AddValidations(() => FirstName,
              FirstName.MustHaveMinimumLengthOf(2)); 
            
        }



        public IList<string> AllErrors
        {
            get { return _dataErrorInfoProvider.AllErrors; }
        }

        public string Error
        {
            get { return null; }
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

        public string this[string propertyName]
        {
            get { return _dataErrorInfoProvider.Validate(propertyName); }
        }
    }
}
