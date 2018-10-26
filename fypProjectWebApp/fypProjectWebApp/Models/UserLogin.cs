using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace fypProjectWebApp.Models
{
    public class UserLogin
    {
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Your email is required")]
        public string email_ID { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Your first name is required")]
        public string user_pass { get; set; }

        [Display(Name = "Remember Me")]
        public bool remember_me { get; set; }
    }
}