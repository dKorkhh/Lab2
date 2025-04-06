using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class Person
    {
        private string firstName;
        private string lastName;
        private string email;
        private DateTime birthDate;

        private bool isAdult;
        private string sunSign;
        private string chineseSign;
        private bool isBirthday;

        public Person(string firstName, string lastName, string email, DateTime birthDate)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.birthDate = birthDate;

            CalculateProperties();
        }

        public Person(string firstName, string lastName, string email)
            : this(firstName, lastName, email, DateTime.MinValue) { }

        public Person(string firstName, string lastName, DateTime birthDate)
            : this(firstName, lastName, "", birthDate) { }


        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public DateTime BirthDate
        {
            get { return birthDate; }
            set
            {
                birthDate = value;
                CalculateProperties();
            }
        }

        public bool IsAdult
        {
            get { return isAdult; }
        }

        public string SunSign
        {
            get { return sunSign; }
        }

        public string ChineseSign
        {
            get { return chineseSign; }
        }

        public bool IsBirthday
        {
            get { return isBirthday; }
        }


        private void CalculateProperties()
        {
            if (birthDate != DateTime.MinValue)
            {
                int age = DateTime.Now.Year - birthDate.Year;
                if (DateTime.Now < birthDate.AddYears(age)) age--;

                isAdult = (age >= 18);
                WesternZodiac westernZodiac = new WesternZodiac();
                ChineseZodiac chineseZodiac = new ChineseZodiac();

                string zodiacSignResult = westernZodiac.getZodiacSign(birthDate);
                string chineseZodiacResult = chineseZodiac.getZodiacSign(birthDate);

                sunSign = zodiacSignResult;
                chineseSign = chineseZodiacResult;
                isBirthday = (birthDate.Day == DateTime.Now.Day && birthDate.Month == DateTime.Now.Month);
            }
        }
    }
}
