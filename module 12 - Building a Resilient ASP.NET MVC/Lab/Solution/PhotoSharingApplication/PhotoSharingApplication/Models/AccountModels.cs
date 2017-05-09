using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PhotoSharingApplication.Models
{

    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("AzureAppServices")
        {

        }
    }

    //A model for logging in
    public class LoginModel
    {
        //UserName
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        //Password
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //Remember Me
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    //A  model for registering
    public class RegisterModel
    {
        //UserName
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        //Password
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        //Confirm Password
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    //This model is for resetting the password
    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
    
}