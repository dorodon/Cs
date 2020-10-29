namespace l3
{
    class ArticleComparer : System.Collections.Generic.IComparer<Article>
    {
        public int Compare(Article x, Article y)
        {
            if (x.Rating < y.Rating)
                return -1;
            else if (x.Rating > y.Rating)
                return 1;
            else return 0;
        }
    }
}
