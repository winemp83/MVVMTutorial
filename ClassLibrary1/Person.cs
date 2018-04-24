using System;
using System.Collections.Generic;

namespace Model
{
    public class Person {
        public string Name { get; set; }
        public string Street { get; set; }
        public int PostalCode { get; set; }
        public string City { get; set; }

        public Person(string name = "unknown", string street = "unknown", int plz = 0, string city = "unknown"){
            Name = name;
            Street = street;
            PostalCode = plz;
            City = city;
        }

        public override bool Equals(object obj)
        {
            return obj is Person person &&
                   Name == person.Name &&
                   Street == person.Street &&
                   PostalCode == person.PostalCode &&
                   City == person.City;
        }

        public override int GetHashCode()
        {
            var hashCode = 1192275173;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Street);
            hashCode = hashCode * -1521134295 + EqualityComparer<int>.Default.GetHashCode(PostalCode);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(City);
            return hashCode;
        }

        public override string ToString() => String.Format("[Name : {0}, Straße : {1}, PLZ/Ort : {2} / {3}]", Name, Street, PostalCode, City);
    }
}
