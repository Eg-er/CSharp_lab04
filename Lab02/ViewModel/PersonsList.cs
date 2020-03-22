using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Lab02.Model;
using Lab02.Tools;
using Lab02.Tools.Managers;

namespace Lab02.ViewModel
{
    internal class PersonsList:BaseViewModel,ILoaderOwner
    {
        private ObservableCollection<Person> _persons;
        private Visibility _loaderVisibility = Visibility.Hidden;
        private bool _isControlEnabled = true;
        private Person _selectedPerson;
        private RelayCommand<object> _submit;
        private RelayCommand<object> _addPerson;
        private RelayCommand<object> _removePerson;
        private RelayCommand<object> _changePerson;
        private RelayCommand<object> _sortByName;
        private RelayCommand<object> _sortBySurname;
        private RelayCommand<object> _sortByBirthDate;
        private RelayCommand<object> _sortByEmail;
        private RelayCommand<object> _sortByChineseSign;
        private RelayCommand<object> _sortBySunSign;
        private RelayCommand<object> _sortByAdultness;
        private RelayCommand<object> _sortByTodayBirthday;
        
        internal PersonsList()
        {
            
            LoaderManager.Instance.Initialize(this);
            LoaderManager.Instance.ShowLoader();
            _persons = new ObservableCollection<Person>(StationManager.DataStorage.PersonsList);
            LoaderManager.Instance.HideLoader();
        }


        public ObservableCollection<Person> Persons
        {
            get => _persons;
            set
            {
                _persons = value;
                OnPropertyChanged();
            }
        }

        public Person SelectedPerson
        {
            get => _selectedPerson;
            set => _selectedPerson = value;
        }

        public RelayCommand<object> SubmitCommand
        {
            get
            {
                return _submit ?? (_submit = new RelayCommand<object>(Submit));
            }
        }
        public RelayCommand<object> AddPersonCommand
        {
            get
            {
                return _addPerson ?? (_addPerson = new RelayCommand<object>(AddPerson));
            }
        }
        public RelayCommand<object> ChangePersonCommand
        {
            get
            {
                return _changePerson ?? (_changePerson = new RelayCommand<object>(ChangePerson));
            }
        }
        public RelayCommand<object> RemovePersonCommand
        {
            get
            {
                return _removePerson ?? (_removePerson = new RelayCommand<object>(RemovePerson));
            }
        }
        public RelayCommand<object> SortByNameCommand
        {
            get
            {
                return _sortByName ?? (_sortByName = new RelayCommand<object>(persons => Sort(persons,1)));
            }
        }
        public RelayCommand<object> SortBySurnameCommand
        {
            get
            {
                return _sortBySurname ?? (_sortBySurname = new RelayCommand<object>(persons => Sort(persons,2)));
            }
        }
        public RelayCommand<object> SortByBirthDateCommand
        {
            get
            {
                return _sortByBirthDate ?? (_sortByBirthDate = new RelayCommand<object>(persons => Sort(persons,3)));
            }
        }
        public RelayCommand<object> SortByEmailCommand
        {
            get
            {
                return _sortByEmail ?? (_sortByEmail = new RelayCommand<object>(persons => Sort(persons,4)));
            }
        }
        public RelayCommand<object> SortByChineseSignCommand
        {
            get
            {
                return _sortByChineseSign ?? (_sortByChineseSign = new RelayCommand<object>(persons => Sort(persons,5)));
            }
        }
        public RelayCommand<object> SortByWesternSignCommand
        {
            get
            {
                return  _sortBySunSign ?? (_sortBySunSign = new RelayCommand<object>(persons => Sort(persons,6)));
            }
        }
        public RelayCommand<object> SortByAdultnessCommand
        {
            get
            {
                return _sortByAdultness ?? (_sortByAdultness = new RelayCommand<object>(persons => Sort(persons,7)));
            }
        }
        public RelayCommand<object> SortByTodayBirthdayCommand
        {
            get
            {
                return _sortByTodayBirthday ?? (_sortByTodayBirthday = new RelayCommand<object>(persons => Sort(persons,8)));
            }
        }

        private async void Submit(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() => { StationManager.DataStorage.Save(); }
            );
            LoaderManager.Instance.HideLoader();
        }

        private void AddPerson(object o)
        {
            IsControlEnabled = false;
            WorkingWithPersonWindow work = new WorkingWithPersonWindow();
            work.ShowDialog();
            Persons = new ObservableCollection<Person>(StationManager.DataStorage.PersonsList);
            IsControlEnabled = true;
        }

        private void ChangePerson(object o)
        {
            if (_selectedPerson != null)
            {
                IsControlEnabled = false;
                WorkingWithPersonWindow work = new WorkingWithPersonWindow(_selectedPerson);
                work.ShowDialog();
                Persons = new ObservableCollection<Person>(StationManager.DataStorage.PersonsList);
                IsControlEnabled = true;
            }else
            {
                MessageBox.Show("Select person!!","Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            
        }

        private void RemovePerson(object o)
        {
            if (_selectedPerson != null)
            {
                StationManager.DataStorage.RemovePerson(_selectedPerson);
                _selectedPerson = null;
                Persons = new ObservableCollection<Person>(StationManager.DataStorage.PersonsList);
            }else
            {
                MessageBox.Show("Select person!!","Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private async void Sort(object o,int order)
        {
         LoaderManager.Instance.ShowLoader();
         await Task.Run(() =>
         {
             switch (order)
             {
              case 1:
                  Persons = new ObservableCollection<Person>(from i in Persons orderby i.Name select i);
                  break;
              case 2:
                  Persons = new ObservableCollection<Person>(from i in Persons orderby i.Surname select i);
                  break;
              case 3:
                  Persons = new ObservableCollection<Person>(from i in Persons orderby i.BirthDate select i);
                  break;
              case 4:
                  Persons = new ObservableCollection<Person>(from i in Persons orderby i.Email select i);
                  break;
              case 5:
                  Persons = new ObservableCollection<Person>(from i in Persons orderby i.ChineseSign select i);
                  break;
              case 6:
                  Persons = new ObservableCollection<Person>(from i in Persons orderby i.SunSign select i);
                  break;
              case 7:
                  Persons = new ObservableCollection<Person>(from i in Persons orderby i.IsAdult select i);
                  break;
              case 8:
                  Persons = new ObservableCollection<Person>(from i in Persons orderby i.IsBirthday select i);
                  break;
             }

             StationManager.DataStorage.PersonsList = Persons.ToList();
         });
         LoaderManager.Instance.HideLoader();
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
        
    }
}