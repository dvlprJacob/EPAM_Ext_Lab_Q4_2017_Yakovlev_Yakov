using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PB.DAL.Helpers.TableFields
{
    public static class ArticlesFields
    {
        public static string ArticleID = "ArticleID";

        public static string BlogID = "BlogID";

        public static string ThemeID = "ThemeID";

        public static string Title = "Title";

        public static string CreateDate = "CreateDate";

        public static string UpdateDate = "UpdateDate";

        public static string Content = "Content";

        public static string IsDeleted = "IsDeleted";

        public static bool IsArticlesTableFields(IEnumerable<string> fieldNames)
        {
            foreach (var field in fieldNames)
            {
                if (field != ArticleID && field != BlogID && field != ThemeID && field != Title && field != CreateDate && field != UpdateDate &&
                    field != Content && field != IsDeleted)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsArticlesTableField(string field)
        {
            return (field != ArticleID && field != BlogID && field != ThemeID && field != Title && field != CreateDate && field != UpdateDate &&
                    field != Content && field != IsDeleted) ;
        }
    }
}
