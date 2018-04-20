using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PB.PL.Models.Article
{
    public class CreateArticleVM
    {

        public int BlogID
        {
            get;
            set;
        }

        public int ThemeID
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public string Content
        {
            get;
            set;
        }

        public List<string> Tags { get; set; }
    }
}