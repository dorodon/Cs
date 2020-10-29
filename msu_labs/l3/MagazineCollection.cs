using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace l3
{
    delegate KeyValuePair<TKey, Magazine> GenerateElement<TKey, Magazine>(int j);
    delegate TKey KeySelector<TKey>(Magazine mg);
    class MagazineCollection<TKey>
    {
        Dictionary<TKey, Magazine> magazines;
        KeySelector<TKey> figureKey;
        internal double MaxRating
        {
            get
            {
                if (magazines.Count > 0)
                    return magazines.Values.Select(m => m.Rating).Max();
                else return 0;
            }
        }
        internal IEnumerable<IGrouping<Frequency, KeyValuePair<TKey, Magazine>>> Groups
        {
            get
            {
                return magazines.GroupBy(m => m.Value.Frequency);
            }
        }
        //передать лямбду?
        internal MagazineCollection(KeySelector<TKey> figureKey)
        {
            magazines = new Dictionary<TKey, Magazine>();
            this.figureKey = figureKey;
        }
        internal void AddDefaults()
        {
            Magazine magazine1 = new Magazine("Gamemania", Frequency.Monthly, new DateTime(2002, 4, 1), 15000);
            magazine1.AddEditors(
                new Person("Rob     ", "Pardo   ", new DateTime(1970, 6, 9)),
                new Person("Hideo   ", "Kojima  ", new DateTime(1963, 9, 24)));
            Article article1 = new Article(new Person("Hidetaka", "Miyazaki", new DateTime(1974, 1, 1)), "Death to Training", 100);
            Article article2 = new Article(new Person("Todd    ", "Howard  ", new DateTime(1971, 4, 25)), "Developing TES   ", 95);
            Article article3 = new Article(new Person("Casey   ", "Hudson  ", DateTime.MinValue), "About Andromeda  ", 70);
            magazine1.AddArticles(article1, article2, article3);

            Magazine magazine2 = new Magazine("CinemaAll", Frequency.Monthly, new DateTime(2016, 10, 6), 20000);
            magazine2.AddArticles(new Article(new Person("Quentin ", "Tarantino", new DateTime(1963, 3, 27)), "Making good films", 80));

            magazines.Add(figureKey(magazine1), magazine1);
            magazines.Add(figureKey(magazine2), magazine2);
        }
        internal void AddMagazines(params Magazine[] magazines)
        {
            foreach (var item in magazines)
                this.magazines.Add(figureKey(item), item);
        }
        internal IEnumerable<KeyValuePair<TKey, Magazine>> FrequencyGroup(Frequency value)
        {
            return magazines.Where(m => m.Value.Frequency == value);
        }
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            foreach (var item in magazines)
                str.Append("Key = " + item.Key + "\n" + item.Value.ToString() );
            return str.ToString() + "\n";
        }
        public string ToShortString()
        {
            StringBuilder str = new StringBuilder();
            foreach (var item in magazines)
                str.Append("Key = " + item.Key + "\n" + item.Value.ToShortString());
            return str.ToString() + "\n";
        }
    }
}
