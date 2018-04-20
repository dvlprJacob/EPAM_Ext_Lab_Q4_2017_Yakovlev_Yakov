using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PB.PL.Models
{
    public class EditUserVM
    {
        [Required, StringLength(50, MinimumLength = 5, ErrorMessage = "Login length must be between 6 and 50 symbols")]
        public string Login { get; set; }
        [Required, StringLength(50, MinimumLength = 2, ErrorMessage = "Name length must be between 3 and 50 symbols")]
        public string FirstName { get; set; }
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name length must be between 3 and 50 symbols")]
        public string LastName { get; set; }
        [Required, RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Must be a valid email")]
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}