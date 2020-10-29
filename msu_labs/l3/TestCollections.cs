using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace l3
{
    class TestCollections<TKey, TValue> 
        where TKey : Edition
        where TValue : Magazine
    {
        internal List<Edition> keys;
        internal List<string> strings;
        GenerateElement<Edition, Magazine> generateElement;
        internal Dictionary<Edition, Magazine> keyDict;
        internal Dictionary<string, Magazine> stringDict;

        internal TestCollections(int count, GenerateElement<Edition, Magazine> GenerateKeyValuePair)
        {
            keys = new List<Edition>(count);
            strings = new List<string>(count);
            generateElement = GenerateKeyValuePair;
            keyDict = new Dictionary<Edition, Magazine>(count);
            stringDict = new Dictionary<string, Magazine>(count);

            KeyValuePair<Edition, Magazine> pair;
            for (int i = 0; i < count; i++)
            {
                keys.Add(new Edition(i.ToString(), DateTime.MinValue, 0));
                strings.Add(i.ToString());
                pair = generateElement(i);
                keyDict.Add(pair.Key, pair.Value);
                stringDict.Add(i.ToString(), pair.Value);
            }
        }
        
        internal static KeyValuePair<Edition, Magazine> GenerateKeyValuePair(int par)
        {
            string parStr = par.ToString();
            DateTime minDateTime = DateTime.MinValue;
            return new KeyValuePair<Edition, Magazine>(new Edition(parStr, minDateTime, 0), new Magazine(parStr, Frequency.Weekly, minDateTime, 0));
        }

        internal static void Time(ref Stopwatch stopwatch, bool cond, string message)
        {
            stopwatch.Start();
            if (cond) stopwatch.Stop();
            Console.WriteLine(message + ": " + stopwatch.Elapsed.TotalMilliseconds * 1000 + "us");
        }
    }
}
