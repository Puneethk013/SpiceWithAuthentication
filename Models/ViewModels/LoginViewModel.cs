using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SpiceWithoutAuthentication.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is must")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Password is must")]
        public string Password { get; set; }
    }
}