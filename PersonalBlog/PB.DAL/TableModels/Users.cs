using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PB.DAL.TableModels
{
    public class Users
    {
        public int UserID
        {
            get;
            set;
        }

        public string Login
        {
            get;
            set;
        }

        /// <summary>
        /// Password hash sum
        /// </summary>
        public string Password
        {
            get;
            set;
        }

        public string FirstName
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public string Phone
        {
            get;
            set;
        }

        public int RoleID
        {
            get;
            set;
        }

        public int? BlogID
        {
            get;
            set;
        }

        public DateTime RegistrationDate
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
