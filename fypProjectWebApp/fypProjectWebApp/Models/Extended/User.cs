using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace fypProjectWebApp.Models
{
    [MetadataType(typeof(UserMetadata))]
    public partial class User
    {
        public string confirm_pass { get; set; }
    }

    public class UserMetadata
    {
        [Display(Name = "First Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Your first name is required")]
        public string first_name { get; set; }

        [Display(Name = "Last Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Your last name is required")]
        public string last_name { get; set; }

        [Display(Name = "UL Email")]
        [DataType(DataType.EmailAddress)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Your UL email is required")]
        public int email_ID { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage ="Password too short. Must be minimum 8 characters")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Your first name is required")]
        public string user_pass { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = " Confirm Password too short. Must be minimum 8 characters")]
        [Compare("user_pass", ErrorMessage = "Both password do not match. Please try again")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Your first name is required")]
        public string confirm_pass { get; set; }
    }
}