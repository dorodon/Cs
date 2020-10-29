using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Collections;

namespace l3
{
    internal enum Frequency { Weekly, Monthly, Yearly }
    class Magazine : Edition, IRateAndCopy
    {
        internal Frequency Frequency { get; set; }
        internal List<Person> Editors { get; set; }
        internal List<Article> Articles { get; set; }

        internal Edition Edition
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
        internal Magazine(string title, Frequency frequency, DateTime date, int circulation)
            : base(title, date, circulation)
        {
            Frequency = frequency;
            Editors = new List<Person>(1);
            Articles = new List<Article>(1);
        }
        internal Magazine()
            : base()
        {
            Frequency = Frequency.Weekly;
            Editors = new List<Person>(1);
            Articles = new List<Article>(1);
        }
        public double Rating
        {
            get
            {
                if (Articles.Count > 0)
                {
                    int length = Articles.Count;
                    double[] rating = new double[length];
                    for (int i = 0; i < length; i++)
                        rating[i] = Articles[i].Rating;
                    return Math.Round(rating.Average(), 2);
                }
                else return 0;
            }
        }
        internal bool this[Frequency frequency]
        {
            get { return Frequency == frequency; }
        }
        internal void AddEditors(params Person[] editors)
        {
            Editors.AddRange(editors);
        }
        internal void AddArticles(params Article[] articles)
        {
            Articles.AddRange(articles);
        }
        public override string ToString()
        {
            StringBuilder str = new StringBuilder(title + " (" + Frequency + ")\t" + date.ToString("d") + "\t" + circulation + "\n" +
                "-------------------------------------------------------------------\n");
            if (Editors != null)
                foreach (var editor in Editors)
                    str.Append(editor.ToString() + "\n");
            str.Append("-------------------------------------------------------------------\n");
            if (Articles != null)
                foreach (var article in Articles)
                    str.Append(article.ToString() + "\n");
            return str.ToString() + "\n";
        }
        public virtual string ToShortString()
        {
            return title + " (" + Frequency + ")\t" + date.ToString("d") + "\t" + circulation + "\n" +
                "Average rating: " + Rating + "\n\n";
        }
        public override bool Equals(object obj)
        {
            try
            {
                Magazine m = (Magazine)obj;

                bool editorsMatch = Editors.Count == m.Editors.Count;
                bool articlesMatch = Articles.Count == m.Articles.Count;
                int i;

                if (editorsMatch)
                    for (i = 0; i < Editors.Count; i++)
                        editorsMatch &= (Editors[i].Equals(m.Editors[i]));

                for (i = 0; i < Articles.Count; i++)
                    articlesMatch &= (Articles[i].Equals(m.Articles[i]));

                bool exp = title.Equals(m.title) && Frequency.Equals(m.Frequency) && date.Equals(m.date)
                    && circulation.Equals(m.circulation);

                return exp && editorsMatch && articlesMatch;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
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
                int i;

                int editorsHash = 0;
                for (i = 0; i < Editors.Count; i++)
                    editorsHash = Editors[i].GetHashCode();

                int articlesHash = 0;
                for (i = 0; i < Articles.Count; i++)
                    articlesHash = Articles[i].GetHashCode();

                return title.GetHashCode() + Frequency.GetHashCode() + date.GetHashCode()
                    + circulation.GetHashCode() + editorsHash + articlesHash;
            }
        }

        public new virtual object DeepCopy()
        {
            var item = new Magazine { title = title, Frequency = Frequency, date = date, circulation = circulation };

            item.Editors.Capacity = Editors.Capacity;
            item.Editors.AddRange(Editors);

            item.Articles.Capacity = Articles.Capacity;
            item.Articles.AddRange(Articles);

            return item;
        }
        internal IEnumerable GetArticlesWithRatingMoreThan(double rating)
        {
            for (int i = 0; i < Articles.Count; i++)
                if (Articles[i].Rating > rating)
                    yield return Articles[i];
        }
        internal IEnumerable GetArticlesWithTitle(string substring)
        {
            for (int i = 0; i < Articles.Count; i++)
                if (Articles[i].Title.IndexOf(substring, StringComparison.CurrentCultureIgnoreCase) != -1)
                    yield return Articles[i];
        }
        internal IEnumerable GetArticlesWrittenByEditors()
        {
            bool toReturn;
            for (int i = 0; i < Articles.Count; i++)
            {
                toReturn = false;
                for (int j = 0; j < Editors.Count; j++)
                    if (Articles[i].Author == Editors[j])
                        toReturn = true;
                if (toReturn) yield return Articles[i];
            }
        }
        internal IEnumerable GetEditorsWithNoArticles()
        {
            bool toReturn;
            for (int i = 0; i < Editors.Count; i++)
            {
                toReturn = true;
                for (int j = 0; j < Articles.Count; j++)
                    if (Editors[i] == Articles[j].Author)
                        toReturn = false;
                if (toReturn) yield return Editors[i];
            }
        }
        public IEnumerator GetEnumerator()
        {
            return new MagazineEnumerator(Editors, Articles);
        }
        internal Magazine OrderByTitle()
        {
            Articles.Sort();
            return this;
        }
        internal Magazine OrderBySurname()
        {
            Articles.Sort(new Article());
            return this;
        }
        internal Magazine OrderByRating()
        {
            Articles.Sort(new ArticleComparer());
            return this;
        }
    }
}