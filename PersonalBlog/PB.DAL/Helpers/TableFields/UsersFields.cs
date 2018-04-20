using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PB.DAL.Helpers.TableFields
{
    public static class UsersFields
    {
        public static string UserID = "UserID";

        public static string Login = "Login";

        public static string Password = "Password";

        public static string FirstName = "FirstName";

        public static string LastName = "LastName";

        public static string Email = "Email";

        public static string Phone = "Phone";

        public static string RoleID = "RoleID";

        public static string BlogID = "BlogID";

        public static string RegistrationDate = "RegistrationDate";

        public static string IsDeleted = "IsDeleted";

        public static bool IsUsersTableFields(IEnumerable<string> fieldNames)
        {
            foreach (var field in fieldNames)
            {
                if (field != UserID && field != Login && field != Password && field != FirstName && field != LastName && field != Email &&
                    field != Phone && field != RoleID && field != BlogID && field != RegistrationDate && field != IsDeleted)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsUsersTableField(string field)
        {
            return (field != UserID && field != Login && field != Password && field != FirstName && field != LastName && field != Email &&
                    field != Phone && field != RoleID && field != BlogID && field != RegistrationDate && field != IsDeleted) ? false : true;
        }
    }
}
