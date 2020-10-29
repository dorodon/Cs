using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Collections;

namespace l2
{
    public enum Frequency { Weekly, Monthly, Yearly }
    public class Magazine : Edition, IRateAndCopy
    {
        private Frequency frequency;
        public Frequency Frequency
        {
            get { return frequency; }
            set { frequency = value; }
        }
        private List<Person> editors;
        public List<Person> Editors
        {
            get { return editors; }
            set { editors = value; }
        }
        private List<Article> articles;
        public List<Article> Articles
        {
            get { return articles; }
            set { articles = value; }
        }
        public Edition Edition
        {
            get 
            {
                return (Edition)base.DeepCopy(); 
            }
            set 
            {
                title = value.Title;
                date = value.Date;
                circulation = value.Circulation;
            }
        }
        public Magazine(string title, Frequency frequency, DateTime date, int circulation) 
            : base(title, date, circulation)
        {
            this.frequency = frequency;
            editors = new List<Person>(1);
            articles = new List<Article>(1);
        }
        public Magazine()
            : base()
        {
            frequency = Frequency.Weekly;
            editors = new List<Person>(1);
            articles = new List<Article>(1);
        }
        public double Rating
        {
            get
            {
                if (!(articles == null))
                {
                    
                    int length = articles.Count;
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
        public void AddEditors(params Person[] editors)
        {
            Editors.AddRange(editors);
        }
        public void AddArticles(params Article[] articles)
        {
            Articles.AddRange(articles);
        }
        public override string ToString()
        {
            StringBuilder str = new StringBuilder(title + " (" + frequency + ")\t" + date + "\t" + circulation + "\n" +
                "-------------------------------------------------------------------\n");
            if (!(editors == null))
                foreach (var editor in editors)
                    str.Append(editor.ToString() + "\n");
            str.Append("-------------------------------------------------------------------\n");
            if (!(articles == null))
                foreach (var article in articles)
                    str.Append(article.ToString() + "\n");
            return str.ToString() + "\n";

        }
        public virtual string ToShortString()
        {
            return title + " (" + frequency + ")\t" + date + "\t" + circulation + "\n" +
                "Average rating: " + Rating + "\n";
        }
        public override bool Equals(object obj)
        {
            Magazine m = (Magazine)obj;
     
            bool exp = title.Equals(m.title) && frequency.Equals(m.frequency) && date.Equals(m.date)
                && circulation.Equals(m.circulation);
            int i, count;

            count = editors.Count;
            bool editorsMatch = count == m.editors.Count;

            if (editorsMatch)
                for (i = 0; i < count; i++)
                {
                    editorsMatch &= (editors[i].Equals(m.editors[i]));
                }

            count = articles.Count;
            bool articlesMatch = count == m.articles.Count;
            if (editorsMatch)
                for (i = 0; i < count; i++)
                {
                    articlesMatch &= (articles[i].Equals(m.articles[i]));
                }
                
            return exp && editorsMatch && articlesMatch;
        }
        public static bool operator ==(Magazine obj1, Magazine obj2)
        {
            return obj1.Equals(obj2);
        }
        public static bool operator !=(Magazine obj1, Magazine obj2)
        {
            return !obj1.Equals(obj2);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int editorsHash = 0;
                for (int i = 0; i < editors.Count; i++)
                    editorsHash = editors[i].GetHashCode();
                int articlesHash = 0;
                for (int i = 0; i < articles.Count; i++)
                    articlesHash = articles[i].GetHashCode();
                return title.GetHashCode() + frequency.GetHashCode() + date.GetHashCode()
                    + circulation.GetHashCode() + editorsHash + articlesHash;
            }
        }
        
        public new virtual object DeepCopy()
        {
            var item = new Magazine { title = title, frequency = frequency, date = date, circulation = circulation };
            
            item.editors.Capacity = editors.Capacity;
            item.editors.AddRange(editors);
            
            item.articles.Capacity = articles.Capacity;
            item.articles.AddRange(articles);    

            return item;
        }
        public IEnumerable GetArticlesWithRatingMoreThan(double rating)
        {
            for (int i = 0; i < articles.Count; i++)
            {
                if (articles[i].Rating > rating)
                    yield return articles[i];
            }
        }
        public IEnumerable GetArticlesWithTitle(string substring)
        {
            for (int i = 0; i < articles.Count; i++)
            {
                if (articles[i].Title.IndexOf(substring, StringComparison.CurrentCultureIgnoreCase) != -1)
                    yield return articles[i];
            }
        }
        public IEnumerable GetArticlesWrittenByEditors()
        {
            bool toReturn;
            for (int i = 0; i < articles.Count; i++)
            {
                toReturn = false;
                for (int j = 0; j < editors.Count; j++)
                    if (articles[i].Author == editors[j])
                        toReturn = true;
                if (toReturn) yield return articles[i];
            }
        }
        public IEnumerable GetEditorsWithNoArticles()
        {
            bool toReturn;
            for (int i = 0; i < editors.Count; i++)
            {
                toReturn = true;
                for (int j = 0; j < articles.Count; j++)
                    if (editors[i] == articles[j].Author)
                        toReturn = false;
                if (toReturn) yield return editors[i];
            }
        }
        public IEnumerator GetEnumerator()
        {
            return new MagazineEnumerator(editors, articles);
        }
    }
}