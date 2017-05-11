using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OnlineAlbum.Models
{
    public class RegAndLog
    {
        public class LogOnModel
        {
            [Required(ErrorMessage = "Login required!")]
            [Display(Name = "Login")]
            public string Login { get; set; }

            [Required(ErrorMessage = "Password required!")]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public class RegisterModel
        {
            [Required(ErrorMessage = "Login required!")]
            [StringLength(15, MinimumLength = 3, ErrorMessage = "From 3 to 15 symbols required!")]
            [Display(Name = "Login")]
            public string Login { get; set; }

            [Required(ErrorMessage = "Password required!")]
            [StringLength(15, MinimumLength = 3, ErrorMessage = "From 3 to 15 symbols required!")]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Required(ErrorMessage = "Password required!")]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm Password")]
            [Compare("Password", ErrorMessage = "Passwords not match!")]
            public string ConfirmPassword { get; set; }

            [Required(ErrorMessage = "Email required!")]
            [RegularExpression(@"(?i)\b[A-Z0-9._%-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b", ErrorMessage = "Email incorret!")]
            [DataType(DataType.EmailAddress)]
            [Display(Name = "Email")]
            public string Email { get; set; }
        }
    }
}