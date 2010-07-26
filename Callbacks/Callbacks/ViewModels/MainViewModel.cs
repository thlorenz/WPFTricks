using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Callbacks.Base;
using Callbacks.Interfaces;
using System.Windows.Input;

namespace Callbacks.ViewModels
{
    public class MainViewModel : NotifyPropertyChanged
    {
        IUser _user;
        Predicate<string> _validateEmail;
        Predicate<string> _validateFirstName;
        Predicate<string> _validateLastName;
        
        ICommand _submitCommand;

        public ICommand SubmitCommand
        {
            get
            {
                return _submitCommand ?? (_submitCommand = new SimpleCommand
                {
                    ExecuteDelegate = _ => Console.WriteLine("Submitted"),
                    CanExecuteDelegate = ValidateAll
                });
            }
        }

        public MainViewModel(IUser user)
        {
            _user = user;
        }

        public string Email
        {
            get { return _user.Email; }
            set
            {
                _user.Email = value;
                RaisePropertyChanged(() => Email);
            }
        }

        public string FirstName
        {
            get { return _user.FirstName; }
            set
            {
                _user.FirstName = value;
                RaisePropertyChanged(() => FirstName);
            }
        }

        public string LastName
        {
            get { return _user.LastName; }
            set
            {
                _user.LastName = value;
                RaisePropertyChanged(() => LastName);
            }
        }

        public Predicate<string> ValidateEmail
        {
            get { return _validateEmail ?? (_validateEmail = email => !string.IsNullOrEmpty(email) && email.Contains('@')); }
        }

        public Predicate<string> ValidateFirstName
        {
            get { return _validateFirstName ?? (_validateFirstName = fname => !string.IsNullOrEmpty(fname)); }
        }

        public Predicate<string> ValidateLastName
        {
            get { return _validateLastName ?? (_validateLastName = lname => !string.IsNullOrEmpty(lname)); }
        }

        bool ValidateAll(object _)
        {
            return
                ValidateFirstName(FirstName) &&
                ValidateLastName(LastName) &&
                ValidateEmail(Email);
        }
    }
}
