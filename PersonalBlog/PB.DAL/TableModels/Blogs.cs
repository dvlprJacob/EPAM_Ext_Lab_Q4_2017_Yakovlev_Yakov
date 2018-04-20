using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PB.DAL.TableModels
{
    public class Blogs
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

        public bool IsDeleted
        {
            get;
            set;
        }

        public int? CreatorID
        {
            get;
            set;
        }
    }
}
