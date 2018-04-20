using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PB.DAL.TableModels
{
    public class Comments
    {
        public int CommentID
        {
            get;
            set;
        }

        public int ArticleID
        {
            get;
            set;
        }

        public int UserID
        {
            get;
            set;
        }

        public string Content
        {
            get;
            set;
        }

        public DateTime CreateDate
        {
            get;
            set;
        }

        /// <summary>
        ///   Article
        ///         |--
        ///         |Comment
        ///         |      |
        ///         |      |Comment
        ///         |      |
        ///         |      |Comment
        ///         |
        ///         |Comment
        /// </summary>
        public int? Parent
        {
            get;
            set;
        }

        public bool IdDeleted
        {
            get;
            set;
        }
    }
}
