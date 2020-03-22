using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lab02.Tools.Managers;
using Lab02.Model;

namespace Lab02.Tools.DataStorage
{
    public class SerializedDataStorage:IDataStorage
    {
        private  List<Person> _persons;
        private static readonly string[] RandomNames = {"Vasya","Alex","John","Kane","Bob",
            "Egor","Vovan","Sergey","Frank","Andrii","Jane","James","Kim","Louis","Valera",
            "Danil","Daniel","George","Victoria","Svetlana","Tamara","Marie","Lesley","Kylie",
            "Lora","Alexandra"
        };

        private static readonly string[] RandomSurnames =
        {"Trump","Zelenskiy","Poroshenko","Jordan","Pivovar","Vozniuck","Apple","Glybovets",
            "Merkel","Boiko","McGregor","Shevchenko","Koval","Ahmad","Brown","Black","Putin",
            "Vlasenko","Lyashko","Khalifa","Pump","Lu"
        };
        public List<Person> PersonsList
        {
            get
            {
                return _persons.ToList();
            }
            set
            {
                _persons = value;
            }
        }

        internal SerializedDataStorage()
        {
            try
            {
                _persons = SerializationManager.Deserialize<List<Person>>(FileFolderHelper.StorageFilePath);
                
            }
            catch (FileNotFoundException)
            {
                _persons = new List<Person>();
                CreatePersonsList();
                Save();
            }
        }

        private void CreatePersonsList()
        {
            var r = new Random();
            for (int i = 0; i < 50; i++)
            {
                AddPerson(new Person(RandomNames[r.Next(0,RandomNames.Length-1)],
                    RandomSurnames[r.Next(0,RandomSurnames.Length-1)],
                    new DateTime(r.Next(1900,2020),r.Next(1,13),r.Next(1,28)),
                    $"{r.Next(1,1000)}email@gmail.com"));
            }
        }

        public void AddPerson(Person p)
        {
            _persons.Add(p);
            Save();
        }
        

        public void RemovePerson(Person p)
        {
            _persons.Remove(p);
            Save();
        }

        public void ChangePerson(Person p,Person changeTo)
        {
            _persons[_persons.IndexOf(p)] = changeTo;
            Save();
        }

        public void Save()
        {
            SerializationManager.Serialize(_persons,FileFolderHelper.StorageFilePath);
        }
    }
}