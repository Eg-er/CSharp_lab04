using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Lab02.Model;
using Lab02.Tools.Managers;
using Lab02.Tools;
namespace Lab02.ViewModel
{
    internal class Lab02ViewModel:BaseViewModel,ILoaderOwner
    {
        #region Fields

        private Person _person;
        private string _name;
        private string _surname;
        private DateTime _birthDate;
        private string _email;
        private string _allFields;
        private Visibility _loaderVisibility = Visibility.Hidden;
        private RelayCommand<object> _submitCommand;
        private bool _isControlEnabled = true;
        
        #endregion
        #region Properties

        private Person Person
        {
            get => _person;
            set
            {
                _person = value;
                OnPropertyChanged();
            }
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

       
        public Visibility LoaderVisibility
        {
            get { return _loaderVisibility; }
            set
            {
                _loaderVisibility = value;
                OnPropertyChanged();
            }
        }
        public bool IsControlEnabled
        {
            get { return _isControlEnabled; }
            set
            {
                _isControlEnabled = value;
                OnPropertyChanged();
            }
        } 
        public RelayCommand<object> SubmitCommand
        {
            get
            {
                return _submitCommand ?? (_submitCommand = new RelayCommand<object>(Submit,o => CheckInput()));
            }
        }

        public string AllFields
        {
            get => _allFields;
            set
            {
                _allFields = value;
                OnPropertyChanged();
            }
        }

        #endregion
        
        private bool CheckInput()
        {
            return (!string.IsNullOrWhiteSpace(_name) && !string.IsNullOrWhiteSpace(_surname) && !string.IsNullOrWhiteSpace(_email));
        }

        internal Lab02ViewModel()
        {
            BirthDate = DateTime.Today;
            LoaderManager.Instance.Initialize(this);
        }
        private async void Submit(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() =>
            {
                Thread.Sleep(2000);
                try
                {
                    Person = new Person(_name,_surname,_birthDate,_email);
                    if (_person.IsBirthday)
                    {
                        MessageBox.Show("З днем народження!!! Щастя, здоровля, зарахів у Бублика!!!");
                    }
                    AllFields = "Name: " + Name + "\nSurname: " + Surname + "\nEmail: " + Email + "\nBirth date: " +
                                BirthDate.Day + "." + BirthDate.Month + "." + BirthDate.Year + "\nIs adult?: " +
                                _person.IsAdult + "\nIs birthday?: " + _person.IsBirthday + "\nWestern sign: " +
                                _person.SunSign + "\nChinese sign: " + _person.ChineseSign;

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message,"Error",MessageBoxButton.OK,MessageBoxImage.Error);
                }
                
            });
            LoaderManager.Instance.HideLoader();
            
        }

    }
}