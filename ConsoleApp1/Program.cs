using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var person in Model.PersonList.Persons)
            {
                Console.WriteLine(person);
            }
            Console.ReadKey();
        }
    }
}
