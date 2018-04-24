namespace ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows.Input;
    using Model;
    using ViewModelBase;
    using System.IO;

    /// <summary>
    /// A ViewModel for a list of Persons.
    /// </summary>
    public class PersonListViewModel : ViewModel
    {
        private ObservableCollection<PersonViewModel> persons;
        private PersonList pl ;
        private Funktionen.XMLDB.PersonenXML db = new Funktionen.XMLDB.PersonenXML("Person");

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
        private ICommand savePersonCommand;

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

        public ICommand SavePersonCommand
        {
            get
            {
                if (savePersonCommand == null)
                {
                    savePersonCommand = new RelayCommand(p => ExecuteSavePersonCommand());
                }
                return savePersonCommand;
            }
        }

        private void ExecuteAddPersonCommand()
        {
            Persons.Add(new PersonViewModel(model: new Person()));
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

        private void ExecuteSavePersonCommand() {
            try {
                foreach(PersonViewModel i in persons)
                {
                    Debug.WriteLine(i.ToString());
                }
            }catch(Exception)
            {
                
            }
        }

        public PersonListViewModel()
        {
            pl = PersonList.Instance();
            var _tmp = (List<Person>)db.ReadAllPerson();
            foreach (Person i in _tmp) {
                pl.Persons.Add(i);
            }
            persons = new ObservableCollection<PersonViewModel>(pl.Persons.Select(p => new PersonViewModel(p)));
            persons.CollectionChanged += Persons_CollectionChanged;
        }

        void Persons_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            File.Delete("Person.xml");
            db.CreatePersonDB();
            foreach(PersonViewModel vm in e.OldItems)
            {
                db.WritePersons(vm.Model);
            }
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (PersonViewModel vm in e.NewItems)
                {
                    pl.Persons.Add(vm.Model);
                    db.WritePersons(vm.Model);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (PersonViewModel vm in e.OldItems)
                {
                    pl.Persons.Remove(vm.Model);
                    db.RemovePerson(vm.Model);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                pl.Persons.Clear();
            }
        }

    }
}