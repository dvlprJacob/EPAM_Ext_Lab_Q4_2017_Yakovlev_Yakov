using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PB.PL.Models.Blog
{
    public class CreateBlogVM
    {
        public string Title
        {
            get;
            set;
        }

        public string UserLogin
        {
            get;
            set;
        }
    }
}