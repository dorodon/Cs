using System;

namespace l3
{
    class Person
    {
        internal string Name { get; set; }
        internal string Surname { get; set; }
        internal DateTime Birthday { get; set; }
        internal int Year
        {
            get { return Birthday.Year; }
            set { Birthday.AddYears(value - Birthday.Year); }
        }

        internal Person(string name, string surname, DateTime birthday)
        {
            Name = name;
            Surname = surname;
            Birthday = birthday;
        }
        internal Person()
        {
            Name = "DF_name ";
            Surname = "DF_surname";
            Birthday = DateTime.MinValue;
        }
        public override string ToString()
        {
            return Name + "\t" + Surname + "\t" + Birthday.ToString("d");
        }
        public virtual string ToShortString()
        {
            return Name + "\t" + Surname;
        }
        public override bool Equals(object obj)
        {
            Person p = (Person)obj;
            return Name.Equals(p.Name) && Surname.Equals(p.Surname) && Birthday.Equals(p.Birthday);
        }
        public static bool operator ==(Person obj1, Person obj2)
        {
            return obj1.Equals(obj2);
        }
        public static bool operator !=(Person obj1, Person obj2)
        {
            return !obj1.Equals(obj2);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                return Name.GetHashCode() + Surname.GetHashCode() + Birthday.GetHashCode();
            }
        }
        public virtual object DeepCopy()
        {
            var item = new Person { Name = Name, Surname = Surname, Birthday = Birthday };
            return item;
        }
    }
}
