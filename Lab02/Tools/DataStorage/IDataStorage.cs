using System.Collections.Generic;
using Lab02.Model;
namespace Lab02.Tools.DataStorage
{
    internal interface IDataStorage
    {
        void AddPerson(Person p);
        void ChangePerson(Person p,Person changeTo);
        void RemovePerson(Person p);
        void Save();
        List<Person> PersonsList { get; set; }
    }
}