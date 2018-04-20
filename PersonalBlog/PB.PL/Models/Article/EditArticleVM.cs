using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PB.PL.Models
{
    public class EditArticleVM
    {
        public int ArticleID
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
    }
}