using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PB.PL.Models.Lists
{
    public class ArticleListVM
    {
        public int ArticleID
        {
            get;
            set;
        }
        public int BlogID
        {
            get;
            set;
        }
        public string Title
        {
            get;
            set;
        }
    }
}