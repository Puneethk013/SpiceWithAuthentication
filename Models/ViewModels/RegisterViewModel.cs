using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SpiceWithoutAuthentication.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Username Canot be blank")]

        public string Name { get; set; }
        [Required(ErrorMessage = "Password Canot be blank")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password Canot be blank")]
        [Compare("Password",ErrorMessage ="password and confirm password does not match")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Email Canot be blank")]
        [EmailAddress(ErrorMessage ="Email format is incorrect")]
        public string Email { get; set; }
        public string Mobile { get; set; }
      
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
    }
}