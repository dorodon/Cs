using System;
using System.Diagnostics;

namespace l1
{
    class Program
    {
        static void Main()
        {
            //1
            Console.WriteLine("-1-\n");
            Magazine magazine = new Magazine("Igromania", Frequency.Monthly, DateTime.Now, 15000);
            Console.WriteLine(magazine.ToShortString());

            //2
            Console.WriteLine("-2-\n");
            Console.WriteLine(magazine[Frequency.Weekly].ToString());
            Console.WriteLine(magazine[Frequency.Monthly].ToString());
            Console.WriteLine(magazine[Frequency.Yearly].ToString() + "\n");

            //3
            Console.WriteLine("-3-\n");
            magazine.Articles = new Article[2];
            int i;
            for (i = 0; i < magazine.Articles.Length; i++)
                magazine.Articles[i] = new Article();
            magazine.Articles[0].Rating = 40;
            Article article1 = magazine.Articles[1];
            article1.Author = new Person("Ivan", "Dmitrov  ", new DateTime(1992, 1, 11));
            article1.Title = "Mass Effect";
            article1.Rating = 70;
            Console.WriteLine(magazine.ToString());

            //4
            Console.WriteLine("-4-\n");
            Article article2 = new Article(new Person(), "Team Fortress 2", 30);
            Article article3 = new Article(new Person("Boris", "Spassky  ", new DateTime(1937, 1, 30)), "Chess online", 100);
            magazine.AddArticles(article2, article3);
            Console.WriteLine(magazine.ToString());

            //5
            Console.WriteLine("-5-\n");
            char[] separator = { ' ', 'x', 'х', '-', '-' };
            Console.Write("Enter nrow & ncol (' ', 'x', '-' might be used as separators): ");
            string input = Console.ReadLine();
            string[] words = input.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            int nrow = int.Parse(words[0]);
            int ncol = int.Parse(words[1]);
            int size = nrow * ncol;

            Article[] array = new Article[size];
            for (i = 0; i < size; i++)
                array[i] = new Article();

            int j;
            Article[,] matrix = new Article[nrow, ncol];
            for (i = 0; i < nrow; i++)
                for (j = 0; j < ncol; j++)
                    matrix[i, j] = new Article();

            Random random = new Random();
            int ost = size;
            int[] col = new int[size];
            i = 0;
            while (ost > 0)
            {
                col[i] = random.Next(1, ost);
                ost -= col[i];
                i++;
            }
            int toothedNrow = i;
            Article[][] toothed = new Article[i][];
            for (i -= 1; i >= 0; i--)
            {
                toothed[i] = new Article[col[i]];
                for (j = 0; j < col[i]; j++)
                    toothed[i][j] = new Article();
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (i = 0; i < size; i++)
                array[i].Rating = 100;
            stopwatch.Stop();
            Console.WriteLine("Processing for array:\t{0}", stopwatch.ElapsedMilliseconds);

            stopwatch.Start();
            for (i = 0; i < nrow; i++)
                for (j = 0; j < ncol; j++)
                    matrix[i, j].Rating = 100;
            stopwatch.Stop();
            Console.WriteLine("Processing for matrix:\t{0}", stopwatch.ElapsedMilliseconds);

            stopwatch.Start();
            for (i = 0; i < toothedNrow; i++)
                for (j = 0; j < col[i]; j++)
                    toothed[i][j].Rating = 100;
            stopwatch.Stop();
            Console.WriteLine("Processing for toothed:\t{0}", stopwatch.ElapsedMilliseconds);

            Console.ReadKey();
        }
    }
}