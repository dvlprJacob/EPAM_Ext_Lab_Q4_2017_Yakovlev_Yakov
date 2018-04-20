using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PB.DAL.Helpers.TableFields
{
    public static class BlogsFields
    {
        public static string BlogID = "BlogID";

        public static string Title = "Title";

        public static string IsDeleted = "IsDeleted";

        public static bool IsArticlesTableFields(IEnumerable<string> fieldNames)
        {
            foreach (var field in fieldNames)
            {
                if (field != BlogID && field != Title && field != IsDeleted)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsArticlesTableField(string field)
        {
            return (field != BlogID && field != Title && field != IsDeleted);
        }
    }
}
