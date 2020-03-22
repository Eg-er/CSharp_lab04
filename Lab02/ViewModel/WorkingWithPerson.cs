using System;
using System.Windows;
using Lab02.Tools;
using Lab02.Model;
using Lab02.Tools.Managers;

namespace Lab02.ViewModel
{
   internal class WorkingWithPerson:BaseViewModel
   {
       private Person _workingPerson = null;
       private string _name;
       private string _surname;
       private string _email;
       private DateTime _birthDate = DateTime.Today;
       private RelayCommand<object> _submit;

       internal WorkingWithPerson()
       {
           
       }

       internal WorkingWithPerson(Person p)
       {
           _workingPerson = p;
           _name = p.Name;
           _surname = p.Surname;
           _email = p.Email;
           _birthDate = p.BirthDate;
       }
       public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        public string Surname
        {
            get { return _surname; }
            set
            {
                _surname = value;
                OnPropertyChanged();
            }
        }
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }
        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                _birthDate = value;
                OnPropertyChanged();
            }
        }
        public RelayCommand<object> SubmitCommand
        {
            get
            {
                return _submit ?? (_submit = new RelayCommand<object>(Submit,o => CheckInput()));
            }
        }

        private void Submit(object obj)
        {
            try
            {
                if (_workingPerson == null)
                {
                    StationManager.DataStorage.AddPerson(new Person(_name, _surname, _birthDate, _email));
                    CloseWindow();
                }
                else
                {
                    StationManager.DataStorage.ChangePerson(_workingPerson,
                        new Person(_name, _surname, _birthDate, _email));
                    CloseWindow();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public Action CloseWindow { get; set; }

        private bool CheckInput()
        {
            return (!string.IsNullOrWhiteSpace(_name) && !string.IsNullOrWhiteSpace(_surname) && !string.IsNullOrWhiteSpace(_email));
        }

   }
}