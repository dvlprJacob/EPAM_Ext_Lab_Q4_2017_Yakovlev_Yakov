using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PB.PL.Models
{
    public class LogonVM
    {
        [Required, StringLength(50, MinimumLength = 5, ErrorMessage = "Login length must be between 6 and 50 symbols")]
        public string Login { get; set; }
        [Required,StringLength(50, MinimumLength = 5, ErrorMessage = "Login length must be between 6 and 50 symbols")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, Compare("Password", ErrorMessage = "Password confarmation not equal input password")]
        [DataType(DataType.Password)]
        public string PasswordConfirmation { get; set; }
    }
}