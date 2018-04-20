using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PB.PL.Models
{
    public class UserVM
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

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime RegistrationDate
        {
            get;
            set;
        }
    }
}