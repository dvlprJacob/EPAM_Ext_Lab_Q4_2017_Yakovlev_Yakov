using PB.DAL.TableModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PB.DAL.Helpers
{
    public class CommentsTree
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

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime CreateDate
        {
            get;
            set;
        }

        /// <summary>
        ///   Article
        ///         |
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

        public List<Comments> ChildNodes
        {
            get;
            set;
        }
    }
}
