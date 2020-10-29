using System;
using System.Diagnostics;
using System.Linq;
using C = System.Console;

namespace l3
{
    class Program
    {
        static void Main()
        {
            //1
            C.WriteLine("-1-\n");

            Article[] articles = new Article[3];
            articles[0] = new Article(new Person("Hidetaka", "Miyazaki", new DateTime(1974, 1, 1)),  "Death to Training", 100);
            articles[1] = new Article(new Person("Todd    ", "Howard  ", new DateTime(1971, 4, 25)), "Developing TES   ", 95);
            articles[2] = new Article(new Person("Casey   ", "Hudson  ", DateTime.MinValue),         "About Andromeda  ", 70);
            Magazine magazine = new Magazine("1st Player", Frequency.Yearly, new DateTime(2002, 4, 1), 15000);
            magazine.AddArticles(articles);

            C.WriteLine(magazine.OrderByTitle().ToString());
            C.WriteLine(magazine.OrderBySurname().ToString());
            C.WriteLine(magazine.OrderByRating().ToString());

            //2
            C.WriteLine("-2-\n");

            MagazineCollection<string> magazineCollection = new MagazineCollection<string>((Magazine mg) => mg.GetHashCode().ToString());
            magazineCollection.AddDefaults();
            magazineCollection.AddMagazines(magazine);
            C.WriteLine(magazineCollection.ToString());


            //3
            C.WriteLine("-3-\n");

            C.WriteLine(magazineCollection.MaxRating + "\n");

            foreach (var item in magazineCollection.FrequencyGroup(Frequency.Monthly))
                C.WriteLine(item.ToString());
            C.WriteLine("\n");

            foreach (var item in magazineCollection.Groups.Select(g => new { Name = g.Key, Count = g.Count() }))
                C.WriteLine(item.ToString());
            C.WriteLine("\n");

            //4
            C.WriteLine("-4-\n");

            bool inputSuccess = false;
            int count = 1;
            while (!inputSuccess || count < 1)
            {
                C.WriteLine("Enter size of test collection: ");
                inputSuccess = int.TryParse(C.ReadLine(), out count);
                if (!inputSuccess || count < 1) C.WriteLine("Exception caught: 'size' should be > 0\n");
            }

            TestCollections<Edition, Magazine> testCollections = new TestCollections<Edition, Magazine>(count
                , (int par) => TestCollections<Edition, Magazine>.GenerateKeyValuePair(par));

            Edition FirstElement = new Edition(0.ToString(), DateTime.MinValue, 0);
            Edition CenterElement = new Edition((count/2).ToString(), DateTime.MinValue, 0);
            Edition LastElement = new Edition((count-1).ToString(), DateTime.MinValue, 0);
            Edition AbscentElement = new Edition((count).ToString(), DateTime.MinValue, 0);

            string First = "0";
            string Center = (count/2).ToString();
            string Last = (count-1).ToString();
            string Abscent = (count).ToString();

            Magazine FirstM = new Magazine(First, Frequency.Weekly, DateTime.MinValue, 0);
            Magazine CenterM = new Magazine(Center, Frequency.Weekly, DateTime.MinValue, 0);
            Magazine LastM = new Magazine(Last, Frequency.Weekly, DateTime.MinValue, 0);
            Magazine AbscentM = new Magazine(Abscent, Frequency.Weekly, DateTime.MinValue, 0);

            Stopwatch stopwatch = new Stopwatch();

            TestCollections<Edition, Magazine>.Time(ref stopwatch, testCollections.keys.Contains(FirstElement), "\nFirstElement in List<Edition>");
            TestCollections<Edition, Magazine>.Time(ref stopwatch, testCollections.keys.Contains(CenterElement), "CenterElement in List<Edition>");
            TestCollections<Edition, Magazine>.Time(ref stopwatch, testCollections.keys.Contains(LastElement), "LastElement in List<Edition>");
            TestCollections<Edition, Magazine>.Time(ref stopwatch, testCollections.keys.Contains(AbscentElement), "AbscentElement in List<Edition>");

            TestCollections<Edition, Magazine>.Time(ref stopwatch, testCollections.strings.Contains(First), "\nFirstElement in List<string>");
            TestCollections<Edition, Magazine>.Time(ref stopwatch, testCollections.strings.Contains(Center), "CenterElement in List<string>");
            TestCollections<Edition, Magazine>.Time(ref stopwatch, testCollections.strings.Contains(Last), "LastElement in List<string>");
            TestCollections<Edition, Magazine>.Time(ref stopwatch, testCollections.strings.Contains(Abscent), "AbscentElement in List<string>");

            TestCollections<Edition, Magazine>.Time(ref stopwatch, testCollections.keyDict.ContainsKey(FirstElement), "\nFirstElement in Dictionary<Edition, Magazine>");
            TestCollections<Edition, Magazine>.Time(ref stopwatch, testCollections.keyDict.ContainsKey(CenterElement), "CenterElement in Dictionary<Edition, Magazine>");
            TestCollections<Edition, Magazine>.Time(ref stopwatch, testCollections.keyDict.ContainsKey(LastElement), "LastElement in Dictionary<Edition, Magazine>");
            TestCollections<Edition, Magazine>.Time(ref stopwatch, testCollections.keyDict.ContainsKey(AbscentElement), "AbscentElement in Dictionary<Edition, Magazine>");

            TestCollections<Edition, Magazine>.Time(ref stopwatch, testCollections.keyDict.ContainsValue(FirstM), "\nFirstElement in Dictionary<Edition, Magazine> (by Value)");
            TestCollections<Edition, Magazine>.Time(ref stopwatch, testCollections.keyDict.ContainsValue(CenterM), "CenterElement in Dictionary<Edition, Magazine> (by Value)");
            TestCollections<Edition, Magazine>.Time(ref stopwatch, testCollections.keyDict.ContainsValue(LastM), "LastElement in Dictionary<Edition, Magazine> (by Value)");
            TestCollections<Edition, Magazine>.Time(ref stopwatch, testCollections.keyDict.ContainsValue(AbscentM), "AbscentElement in Dictionary<Edition, Magazine> (by Value)");

            TestCollections<Edition, Magazine>.Time(ref stopwatch, testCollections.stringDict.ContainsKey(First), "\nFirstElement in Dictionary<string, Magazine>");
            TestCollections<Edition, Magazine>.Time(ref stopwatch, testCollections.stringDict.ContainsKey(Center), "CenterElement in Dictionary<string, Magazine>");
            TestCollections<Edition, Magazine>.Time(ref stopwatch, testCollections.stringDict.ContainsKey(Last), "LastElement in Dictionary<string, Magazine>");
            TestCollections<Edition, Magazine>.Time(ref stopwatch, testCollections.stringDict.ContainsKey(Abscent), "AbscentElement in Dictionary<string, Magazine>");

            TestCollections<Edition, Magazine>.Time(ref stopwatch, testCollections.stringDict.ContainsValue(FirstM), "\nFirstElement in Dictionary<string, Magazine> (by Value)");
            TestCollections<Edition, Magazine>.Time(ref stopwatch, testCollections.stringDict.ContainsValue(CenterM), "CenterElement in Dictionary<string, Magazine> (by Value)");
            TestCollections<Edition, Magazine>.Time(ref stopwatch, testCollections.stringDict.ContainsValue(LastM), "LastElement in Dictionary<string, Magazine> (by Value)");
            TestCollections<Edition, Magazine>.Time(ref stopwatch, testCollections.stringDict.ContainsValue(AbscentM), "AbscentElement in Dictionary<string, Magazine> (by Value)");

            C.ReadKey();
        }
    }
}
