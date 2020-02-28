using System;
namespace Lab02.Model
{
    internal class Person
    {
        #region Zodiacs
        
        private static readonly string[] WesternZodiacs =
        {
            "Aquarius", "Pisces", "Aries", "Taurus", "Gemini", "Cancer", "Leo", "Virgo", "Libra", "Scorpio",
            "Sagittarius", "Capricorn"
        };

        private static readonly string[] ChineseZodiacs =
            {"Monkey", "Rooster", "Dog", "Pig","Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Goat"};
        
        #endregion
        
        #region Fields
        private string _name;
        private string _surname;
        private DateTime _birthDate;
        private string _email;
        private readonly bool _isAdult;
        private readonly string _sunSign;
        private readonly string _chineseSign;
        private readonly bool _isBirthday;
        
        #endregion
        #region Properties

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string Surname
        {
            get => _surname;
            set => _surname = value;
        }

        public DateTime BirthDate
        {
            get => _birthDate;
            set => _birthDate = value;
        }

        public string Email
        {
            get => _email;
            set => _email = value;
        }

        public string SunSign => _sunSign;

        public string ChineseSign => _chineseSign;

        public bool IsAdult => _isAdult;

        public bool IsBirthday => _isBirthday;

        #endregion

        internal Person(string name,string surname,DateTime birthDate, string email)
        {
            _name = name;
            _surname = surname;
            _birthDate = birthDate;
            _email = email;

            var age = CalculateAge();
            _isAdult = age >= 18;
            _isBirthday = ((DateTime.Today.Day == BirthDate.Day) && (DateTime.Today.Month == BirthDate.Month));
            _sunSign = CalculateWesternZodiac();
            _chineseSign = CalculateChineseZodiac();

        }

        private string CalculateChineseZodiac()
        {
            var year = BirthDate.Year;
            var res = ChineseZodiacs[year % 12];
            return res;
        }

        private string CalculateWesternZodiac()
        {
            var month = BirthDate.Month;
            var day = BirthDate.Day;
            var res = "";
            switch (month)
            {
                case 2:
                    res+= day >= 19 ? WesternZodiacs[month - 1] : WesternZodiacs[month - 2];
                    break;
                case 1: case 4:
                    res+= day>=20? WesternZodiacs[month-1] :(month==1?WesternZodiacs[WesternZodiacs.Length-1]:WesternZodiacs[month-2]);
                    break;
                case 3: case 5: case 6:
                    res+= day>=21? WesternZodiacs[month - 1] : WesternZodiacs[month - 2];
                    break;
                case 11: case 12:
                    res+= day>=22? WesternZodiacs[month - 1] : WesternZodiacs[month - 2];
                    break;
                case 7: case 8: case 9: case 10:
                    res+= day>=23? WesternZodiacs[month - 1] : WesternZodiacs[month - 2];
                    break;
            }

            return res;
        }

        private int CalculateAge()
        {
            var now = DateTime.Today;
            var age = now.Year - BirthDate.Year;
            if ((now.Month < BirthDate.Month) || (now.Month == BirthDate.Month && now.Day < BirthDate.Day))
            {
                --age;
            }
            if (age < 0 || age > 135)
            {
                throw new ArgumentException("It is impossible to have such age!!!");
            }
            

            return age;
        }

        internal Person(string name, string surname, string email):
        
            this(name,surname,DateTime.Today,email)

        {
        }
        internal Person(string name, string surname, DateTime birthDate):
        
            this(name,surname,birthDate,"vozniuck@ukma.edu.ua")

        {
        }

    }
}