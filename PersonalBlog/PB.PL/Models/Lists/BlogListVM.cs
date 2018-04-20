using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PB.PL.Models.Lists
{
    public class BlogListVM
    {
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