using System;

namespace l3
{
    public class Edition
    {
        private protected string title;
        internal string Title
        {
            get { return title; }
            set { title = value; }
        }
        private protected DateTime date;
        internal DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        private protected int circulation;
        internal int Circulation
        {
            get { return circulation; }
            set 
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("circulation", "Parameter should be >= 0");
                else
                    circulation = value;
            }
        }
        internal Edition(string title, DateTime date, int circulation)
        {
            this.title = title;
            this.date = date;
            this.circulation = circulation;
        }
        internal Edition()
        {
            title = "DF_title";
            date = DateTime.MinValue;
            circulation = 0;
        }
        public virtual object DeepCopy()
        {
            var item = new Edition { title = title, date = date, circulation = circulation };
            return item;
        }
        public override bool Equals(object obj)
        {
            Edition e = (Edition)obj;
            return title.Equals(e.title) && date.Equals(e.date) && circulation.Equals(e.circulation);
        }
        public static bool operator ==(Edition obj1, Edition obj2)
        {
            return obj1.Equals(obj2);
        }
        public static bool operator !=(Edition obj1, Edition obj2)
        {
            return !obj1.Equals(obj2);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                return title.GetHashCode() + date.GetHashCode() + circulation.GetHashCode();
            }
        }
        public override string ToString()
        {
            return title + "\t" + date.ToString("d") + "\t" + circulation;
        }
    }
}