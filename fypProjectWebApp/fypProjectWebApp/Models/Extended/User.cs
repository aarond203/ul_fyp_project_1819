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
        public string confirmPass { get; set; }
    }

    public class UserMetadata
    {
        [Display(Name = "First Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Your first name is required")]
        public string firstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Your last name is required")]
        public string lastName { get; set; }

        [Display(Name = "UL Email")]
        [DataType(DataType.EmailAddress)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Your UL email is required")]
        public string emailID { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage ="Password too short. Must be minimum 8 characters")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Your first name is required")]
        public string userPass { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = " Confirm Password too short. Must be minimum 8 characters")]
        [Compare("userPass", ErrorMessage = "Both password do not match. Please try again")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Your first name is required")]
        public string confirmPass { get; set; }
    }
}