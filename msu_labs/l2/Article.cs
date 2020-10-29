using System;

namespace l2
{
    public class Article : IRateAndCopy
    {
        public Person Author { get; set; }
        public string Title { get; set; }
        public double Rating { get; set; }
        public Article(Person author, string title, double rating)
        {
            Author = author;
            Title = title;
            Rating = rating;
        }
        public Article()
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
            Article a = (Article)obj;
            return Author.Equals(a.Author) && Title.Equals(a.Title) && Rating.Equals(a.Rating);
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
    }
}