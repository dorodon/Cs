using System;
using System.Linq;
using System.Text;

namespace l1
{
    public enum Frequency { Weekly, Monthly, Yearly }
    public class Magazine
    {
        private string title;
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        private Frequency frequency;
        public Frequency Frequency
        {
            get { return frequency; }
            set { frequency = value; }
        }
        private DateTime date;
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        private int circulation;
        public int Circulation
        {
            get { return circulation; }
            set { circulation = value; }
        }
        private Article[] articles;
        public Article[] Articles
        {
            get { return articles; }
            set { articles = value; }
        }
        public Magazine(string title, Frequency frequency, DateTime date, int circulation)
        {
            this.title = title;
            this.frequency = frequency;
            this.date = date;
            this.circulation = circulation;
        }
        public Magazine()
        {
            title = "DF_title";
            frequency = Frequency.Weekly;
            date = DateTime.MinValue;
            circulation = 0;
        }
        public double AvgRating
        {
            get
            {
                if (!(articles == null))
                {
                    int length = articles.Length;
                    double[] rating = new double[length];
                    for (int i = 0; i < length; i++)
                        rating[i] = articles[i].Rating;
                    return rating.Average();
                }
                else return 0;
            }
        }
        public bool this[Frequency frequency]
        {
            get { return this.frequency == frequency; }
        }
        public void AddArticles(params Article[] articles)
        {
            int begin = (this.articles == null) ? 0 : this.articles.Length;
            int end = begin + articles.Length;
            Array.Resize(ref this.articles, end);
            for (int i = begin; i < end; i++)
                this.articles[i] = articles[i - begin];
        }
        public override string ToString()
        {
            StringBuilder str = new StringBuilder(title + " (" + frequency + ")\t" + date + "\t" + circulation + "\n" +
                "-------------------------------------------------------------------\n");
            if (!(articles == null))
                foreach (var article in articles)
                    str.Append(article.ToString() + "\n");
            return str.ToString();

        }
        public virtual string ToShortString()
        {
            return title + " (" + frequency + ")\t" + date + "\t" + circulation + "\n" +
                "Average rating: " + AvgRating + "\n";
        }
    }
}