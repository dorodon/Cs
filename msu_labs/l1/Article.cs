using System;

namespace l1
{
    public class Article
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
    }
}