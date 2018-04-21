namespace ViewModel
{
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows.Input;
    using Model;
    using ViewModelBase;

    /// <summary>
    /// A ViewModel for a list of Persons.
    /// </summary>
    public class PersonListViewModel : ViewModel
    {
        private ObservableCollection<PersonViewModel> persons;

        public ObservableCollection<PersonViewModel> Persons
        {
            get
            {
                return persons;
            }
            set
            {
                if (Persons != value)
                {
                    persons = value;
                    OnPropertyChanged("Persons");
                }
            }
        }

        private ICommand addPersonCommand;
        private ICommand delPersonCommand;

        public ICommand AddPersonCommand
        {
            get
            {
                if (addPersonCommand == null)
                {
                    addPersonCommand = new RelayCommand(p => ExecuteAddPersonCommand());
                }
                return addPersonCommand;
            }
        }

        public ICommand DelPersonCommand
        {
            get
            {
                if (delPersonCommand == null)
                {
                    delPersonCommand = new RelayCommand<object>(ExecuteDelPersonCommand);
                }
                return delPersonCommand;
            }
        }

        private void ExecuteAddPersonCommand()
        {
            Persons.Add(new PersonViewModel(new Person()));
        }

        private void ExecuteDelPersonCommand(object state) {
            Debug.WriteLine(state);
            PersonViewModel _tmp = new PersonViewModel(new Person("null"));
            foreach (PersonViewModel i in Persons)
                if (i.Name.Equals(state))
                    _tmp = i;
            if (_tmp.Name != "null")
                Persons.Remove(_tmp);
        }

        public PersonListViewModel()
        {
            persons = new ObservableCollection<PersonViewModel>(PersonList.Persons.Select(p => new PersonViewModel(p)));
            persons.CollectionChanged += Persons_CollectionChanged;
        }

        void Persons_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (PersonViewModel vm in e.NewItems)
                {
                    PersonList.Persons.Add(vm.Model);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (PersonViewModel vm in e.OldItems)
                {
                    PersonList.Persons.Remove(vm.Model);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                PersonList.Persons.Clear();
            }
        }

    }
}