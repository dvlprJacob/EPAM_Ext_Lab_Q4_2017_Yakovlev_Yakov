using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PB.DAL.TableModels
{
    public class Articles
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

        public DateTime CreateDate
        {
            get;
            set;
        }

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

        public bool IsDeleted
        {
            get;
            set;
        }
    }
}
