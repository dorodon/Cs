using System;
using System.Collections.Generic;

namespace l3
{
    public class Article : IRateAndCopy, IComparable<Article>, IComparer<Article>
    {
        internal Person Author { get; set; }
        internal string Title { get; set; }
        public double Rating { get; set; }
        internal Article(Person author, string title, double rating)
        {
            Author = author;
            Title = title;
            Rating = rating;
        }
        internal Article()
        {
            Author = new Person();
            Title = "DF_title";
            Rating = 0;
        }
        public override string ToString()
        {
            return Author.ToString() + "\t" + Title + "\t" + Rating;
        }
        public override bool Equals(object obj)
        {
            try
            {
                Article a = (Article)obj;
                return Author.Equals(a.Author) && Title.Equals(a.Title) && Rating.Equals(a.Rating);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }    
        }
        public static bool operator ==(Article obj1, Article obj2)
        {
            return obj1.Equals(obj2);
        }
        public static bool operator !=(Article obj1, Article obj2)
        {
            return !obj1.Equals(obj2);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                return Author.GetHashCode() + Title.GetHashCode() + Rating.GetHashCode();
            }
        }
        public virtual object DeepCopy()
        {
            var item = new Article { Author = Author, Title = Title, Rating = Rating };
            return item;
        }

        public int CompareTo(Article other)
        {
            return Title.CompareTo(other.Title);
        }

        public int Compare(Article x, Article y)
        {
            return x.Author.Surname.CompareTo(y.Author.Surname);
        }
    }
}