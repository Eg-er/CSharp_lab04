using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Lab02.Exceptions;

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

        private string Name
        {
            get => _name;
            set
            {
                if (Regex.IsMatch(value, "^[a-zA-Z]+(([- ][a-zA-Z ])?[a-zA-Z]*)*$"))
                {
                    _name = value;
                }
                else
                {
                    throw new BadNameException("Error: typo in field \"name\" ");
                }
            } 
        }

        private string Surname
        {
            get => _surname;
            set
            {
                if (Regex.IsMatch(value, "^[a-zA-Z]+(([- ][a-zA-Z ])?[a-zA-Z]*)*$"))
                {
                    _surname = value;
                }
                else
                {
                    throw new BadSurnameException("Error: typo in field \"surname\" ");
                }
            }
        }

        private DateTime BirthDate
        {
            get => _birthDate;
            set => _birthDate = value;
        }

        private string Email
        {
            get => _email;
            set
            {
                
                if (new EmailAddressAttribute().IsValid(value))
                {
                    _email = value;
                }
                else
                {
                    throw new InvalidEmailException("Invalid email!!!");
                }
            }
        }

        internal string SunSign => _sunSign;

        internal string ChineseSign => _chineseSign;

        internal bool IsAdult => _isAdult;

        internal bool IsBirthday => _isBirthday;

        #endregion

        internal Person(string name,string surname,DateTime birthDate, string email)
        {
            Name = name;
            Surname = surname;
            _birthDate = birthDate;
            Email = email;

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
            if (age < 0)
            {
                throw new FutureBirthDateException("Invalid date of birth!!!(You selected future date)");
            }

            if (age > 135)
            {
                throw new TooOldException("Sorry, but your are too old :(((");
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