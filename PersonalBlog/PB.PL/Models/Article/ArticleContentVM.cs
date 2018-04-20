using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PB.PL.Models
{
    public class ArticleContentVM
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

        [Display(Name = "Update")]
        [DisplayFormat(DataFormatString = "{0:d}",NullDisplayText ="")]
        public DateTime CreateDate
        {
            get;
            set;
        }

        [DisplayFormat(DataFormatString = "{0:d}", NullDisplayText = "")]
        public DateTime? UpdateDate
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