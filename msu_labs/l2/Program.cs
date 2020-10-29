using System;

namespace l2
{
    class Program
    {
        static void Main()
        {
            //1
            Console.WriteLine("-1-\n");
            Edition edition1 = new Edition("Igromania", new DateTime(2011, 12, 12), 15000);
            Edition edition2 = new Edition("Igromania", new DateTime(2011, 12, 12), 15000);
            Console.WriteLine("Ref Match:\t" + ReferenceEquals(edition1, edition2));
            Console.WriteLine("Data Match:\t" + (edition1 == edition2) + "\n");
            Console.WriteLine("edition1.HashCode = {0}", edition1.GetHashCode());
            Console.WriteLine("edition2.HashCode = {0}", edition2.GetHashCode());
            
            //2
            Console.WriteLine("\n-2-\n");
            try
            {
                edition1.Circulation = -50;
            }
            catch (ArgumentOutOfRangeException exc)
            {
                Console.WriteLine(exc.Message);
            }
            
            //3
            Console.WriteLine("\n-3-\n");
            Magazine magazine11 = new Magazine();
            magazine11.Edition = edition1;
            magazine11.Frequency = Frequency.Monthly;
            magazine11.AddEditors(
                new Person("Hidetaka", "Miyazaki", new DateTime(1974, 1, 1)),
                new Person("Todd    ", "Howard  ", new DateTime(1971, 4, 25)),
                new Person("Hideo   ", "Kojima  ", new DateTime(1963, 9, 24)));
            Article[] articles = new Article[6];
            articles[0] = new Article(magazine11.Editors[0], "Dark Souls", 100); 
            articles[1] = new Article(new Person(), "Team Fortress 2", 30);
            articles[2] = new Article(new Person("Casey   ", "Hudson  ", DateTime.MinValue), "Mass Effect", 90);
            articles[3] = new Article(magazine11.Editors[1], "TES V: Skyrim", 100);
            articles[4] = new Article();
            articles[5] = new Article(magazine11.Editors[1], "TESO Online", 100);
            magazine11.AddArticles(articles);
            Console.WriteLine(magazine11.ToString());
            
            //4
            Console.WriteLine("-4-\n");
            Console.WriteLine(magazine11.Edition.ToString());

            //5
            Console.WriteLine("\n-5-\n");
            Magazine magazine22 = (Magazine)magazine11.DeepCopy();
            magazine22.Editors.RemoveAt(1);
            magazine22.Articles.RemoveRange(0, 3);
            Console.WriteLine(magazine22.ToString());
            Console.WriteLine(magazine11.ToString());

            //6
            Console.WriteLine("-6-\n");
            foreach (var article in magazine11.GetArticlesWithRatingMoreThan(30))
                Console.WriteLine(article.ToString());

            //7
            Console.WriteLine("\n-7-\n");
            foreach (var article in magazine11.GetArticlesWithTitle("SS"))
                Console.WriteLine(article.ToString());

            //8
            Console.WriteLine("\n-8-\n");
            foreach (var article in magazine11)
                Console.WriteLine(article.ToString());

            //9
            Console.WriteLine("\n-9-\n");
            foreach (var article in magazine11.GetArticlesWrittenByEditors())
                Console.WriteLine(article.ToString());

            //10
            Console.WriteLine("\n-10-\n");
            foreach (var editor in magazine11.GetEditorsWithNoArticles())
                Console.WriteLine(editor.ToString());

            Console.ReadKey();
        }
    }
}
