using System.Collections.Generic;

namespace Model
{
    public class PersonList
    {
        private static PersonList _PLInstance;
        public IList<Person> Persons { get; private set; }

        protected PersonList()
        {
            Persons = new List<Person>();
        }

        public Person AddPerson(){
            var person = new Person();
            Persons.Add(person);
            return person;
        }

        public static PersonList Instance(){
            if (_PLInstance == null)
            {
                //lock (syncLock)
                //{
                    if (_PLInstance == null)
                    {
                        _PLInstance = new PersonList();
                    }
                //}
            }

            return _PLInstance;
        }
    }
    
}
