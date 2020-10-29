using System.Collections.Generic;
using System.Collections;

namespace l2
{
    public class MagazineEnumerator : IEnumerator
    {
        int position = -1;
        private List<Person> editors { get; set; }
        private List<Article> articles { get; set; }
        public MagazineEnumerator(List<Person> editors, List<Article> articles)
        {
            this.editors = editors;
            this.articles = articles;
        }
        public object Current
        {
            get
            {
                return articles[position];
            }
        }
        public bool MoveNext()
        {
            bool nextToReturn;
            do
            {
                if (position + 1 < articles.Count)
                {
                    nextToReturn = true;
                    for (int i = 0; i < editors.Count; i++)
                    {
                        if (articles[position + 1].Author == editors[i])
                            nextToReturn = false;
                    }
                    position++;
                }
                else return false;
            }
            while (!nextToReturn);
            return true;
        }

        public void Reset()
        {
            position = -1;
        } 
    }
}