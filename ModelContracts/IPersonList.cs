using System.Collections.Generic;

namespace Model
{
    public interface IPersonList
    {
        IList<IPerson> Persons { get; }

        IPerson AddPerson();
    }
}