using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SpellChecker
{
    class Program
    {
        static void Main()
        {
            StringBuilder input = new StringBuilder();
            do
            {
                input.AppendLine(Console.ReadLine());
            }
            while (Console.KeyAvailable);

            const string pattern = @"^\s*===\s*$";
            const byte maxSplitIndex = 3;
            try
            {
                string[] splitInput = Regex.Split(input.ToString(), pattern, RegexOptions.Multiline);
                if (splitInput.Length != maxSplitIndex)
                    throw new Exception($"Incorrect input: line '===' must be used exactly {maxSplitIndex - 1} times");
                if (splitInput[maxSplitIndex - 1].Trim() != string.Empty)
                    throw new Exception("Incorrect input: text after closing '==='");

                const string delimiter = @"\s+";
                const byte maxWordLength = 50;

                var dictionary = Regex.Split(splitInput[0], delimiter, RegexOptions.Compiled | RegexOptions.Multiline).Where(s => s != string.Empty);
                foreach (var item in dictionary)
                {
                    if (!(Regex.IsMatch(item, @"^[a-zA-Z]+$")))
                        throw new Exception($"Incorrect dictionary input: {item} contains non-letters");
                    if (item.Length > maxWordLength)
                        throw new Exception($"Incorrect dictionary input: {item} contains more than {maxWordLength} letters");
                }

                var text = Regex.Split(splitInput[1], delimiter, RegexOptions.Compiled | RegexOptions.Multiline).Where(s => s != string.Empty);
                foreach (var item in text)
                {
                    if (!(Regex.IsMatch(item, @"^[a-zA-Z]+$")))
                        throw new Exception($"Incorrect text input: {item} contains non-letters");
                    if (item.Length > maxWordLength)
                        throw new Exception($"Incorrect text input: {item} contains more than {maxWordLength} letters");
                }
                StringBuilder output = new StringBuilder(splitInput[1]);

                var wordLengthGroups = dictionary
                    .OrderBy(w => w.Length)
                    .GroupBy(w => w.Length)
                    .Select(g => new
                    {
                        WordsLength = g.FirstOrDefault().Length,
                        Words = g.Select(w => w) 
                    });

                foreach (var word in text)
                {
                    int len = word.Length;
                    List<string> editions = new List<string>();

                    //match
                    foreach (var group in wordLengthGroups)
                        foreach (var item in group.Words)
                            if (item.Equals(word, StringComparison.OrdinalIgnoreCase))
                            {
                                editions.Add(item);
                                goto AFTERCHECK;
                            }

                    bool noOneLetterEdition = true;

                    //deleted 1 letter
                    Task taskDeletedOneLetter = Task.Run( () =>
                    {
                        foreach (var group in wordLengthGroups.Where(g => g.WordsLength == len + 1))
                            foreach (var item in group.Words)
                                for (int i = 0; i < item.Length; i++)
                                    if (item.Remove(i, 1).Equals(word, StringComparison.OrdinalIgnoreCase))
                                    {
                                        editions.Add(item);
                                        if (noOneLetterEdition) noOneLetterEdition = false;
                                        break;
                                    }
                    });

                    //inserted 1 letter
                    foreach (var group in wordLengthGroups.Where(g => g.WordsLength == len - 1))
                        foreach (var item in group.Words)
                            for (int i = 0; i < len; i++) 
                                if (word.Remove(i, 1).Equals(item, StringComparison.OrdinalIgnoreCase))
                                {
                                    editions.Add(item);
                                    if (noOneLetterEdition) noOneLetterEdition = false;
                                    break;
                                }

                    taskDeletedOneLetter.Wait();

                    if (noOneLetterEdition)
                    {
                        bool noDictionaryWordFound;

                        //deleted 2 non-adjacent letters
                        Task taskDeletedTwoNonadjacentLetters = Task.Run( () =>
                        {
                            int j;
                            foreach (var group in wordLengthGroups.Where(g => g.WordsLength == len + 2))
                                foreach (var item in group.Words)
                                {
                                    noDictionaryWordFound = true;
                                    for (int i = 0; noDictionaryWordFound && i < item.Length; i++)
                                    {
                                        j = i + 2;
                                        while (j > i + 1 && j < item.Length)
                                        {
                                            if (item.Remove(i, 1).Remove(j - 1, 1).Equals(word, StringComparison.OrdinalIgnoreCase))
                                            {
                                                editions.Add(item);
                                                noDictionaryWordFound = false;
                                                break;
                                            }
                                            j++;
                                        }
                                    }
                                }
                        });

                        //inserted 2 non-adjacent letters
                        Task taskInsertedTwoNonadjacentLetters = Task.Run( () =>
                        {
                            int j;
                            foreach (var group in wordLengthGroups.Where(g => g.WordsLength == len - 2))
                                foreach (var item in group.Words)
                                {
                                    noDictionaryWordFound = true;
                                    for (int i = 0; noDictionaryWordFound && i < len; i++)
                                    {
                                        j = i + 2;
                                        while (j > i + 1 && j < len)
                                        {
                                            if (word.Remove(i, 1).Remove(j - 1, 1).Equals(item, StringComparison.OrdinalIgnoreCase))
                                            {
                                                editions.Add(item);
                                                noDictionaryWordFound = false;
                                                break;
                                            }
                                            j++;
                                        }
                                    }
                                }
                        });

                        //deleted 1 letter and inserted 1 letter
                        foreach (var group in wordLengthGroups.Where(g => g.WordsLength == len))
                            foreach (var item in group.Words)
                            {
                                noDictionaryWordFound = true;
                                for (int i = 0; noDictionaryWordFound && i < len; i++)
                                    for (int j = 0; j < item.Length; j++)
                                        if (word.Remove(i, 1).Equals(item.Remove(j, 1), StringComparison.OrdinalIgnoreCase))
                                        {
                                            editions.Add(item);
                                            noDictionaryWordFound = false;
                                            break;
                                        }
                            }

                        taskDeletedTwoNonadjacentLetters.Wait();
                        taskInsertedTwoNonadjacentLetters.Wait();
                    }

                    StringBuilder editionsStr = new StringBuilder();
                    var match = Regex.Match(output.ToString(), $@"\b{word}\b");

                    switch (editions.Count)
                    {
                        case 0:
                            output.Replace(word, $"{{{word}?}}", match.Index, len);
                            break;
                        case 1:
                            output.Replace(word, editions[0], match.Index, len);
                            break;
                        default:
                            if (editions.Count > 1)
                            {
                                //print the set of editions in the order they appear in the dictionary
                                foreach (var item in dictionary.Where(w => editions.Contains(w)))
                                    editionsStr.Append(item + " ");
                                output.Replace(word, $"{{{editionsStr.ToString().TrimEnd()}}}", match.Index, len);
                            }
                            break;
                    }

                AFTERCHECK:
                    editions.Clear(); 
                }

                Console.Write(output);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }
    }
}

/*
FOR TESTING:

rain spain plain plaint Pain main mainly
the in on fall Falls his was
===
hte rame in    pain fells
mainy oon teh lain
was hints pliant
===

705
oooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo
===
===
d
===

brain Spain  pain an Ann a
===
    sain pain ann Cobain anti    RAIN ain  rAi sain
===

*/