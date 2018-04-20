using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PB.PL.Models.Article
{
    public class CreateCommentVM
    {
        public int ArticleID
        {
            get;
            set;
        }

        [Required]
        public string Content
        {
            get;
            set;
        }

        public int? Parent
        {
            get;
            set;
        }
    }
}