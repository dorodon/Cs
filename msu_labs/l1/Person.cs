using System;

namespace l1
{
    public class Person
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string surname;
        public string Surname
        {
            get { return surname; }
            set { surname = value; }

        }
        private DateTime birthday;
        public DateTime Birthday
        {
            get { return birthday; }
            set { birthday = value; }
        }
        public int Year
        {
            get { return birthday.Year; }
            set { birthday.AddYears(value - birthday.Year); }
        }

        public Person(string name, string surname, DateTime birthday)
        {
            this.name = name;
            this.surname = surname;
            this.birthday = birthday;
        }
        public Person()
        {
            name = "DF_name";
            surname = "DF_surname";
            birthday = DateTime.MinValue;
        }
        public override string ToString()
        {
            return name + "\t" + surname + "\t" + birthday.ToString();
        }
        public virtual string ToShortString()
        {
            return name + "\t" + surname;
        }
    }
}
