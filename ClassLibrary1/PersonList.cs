using System.Collections.Generic;

namespace Model
{
    public static class PersonList
    {
        public static IList<Person> Persons { get; private set; }

        static PersonList()
        {
            Persons = new List<Person>()
        {
            new Person(){Name = "John Doe",
                Street = "Onestreet",
                PostalCode = 12345,
                City = "Onecity"},
            new Person(){Name = "Jane Doe",
                Street = "Onestreet",
                PostalCode = 12345,
                City = "Onecity"},
            new Person(){Name = "Foo Bar",
                Street = "Twostreet",
                PostalCode = 54321,
                City = "Twocity"}
        };
        }
    }
}
