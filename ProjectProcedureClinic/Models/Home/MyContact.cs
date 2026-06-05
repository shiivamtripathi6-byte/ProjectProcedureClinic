using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProjectProcedureClinic.Models.Home
{
    public class MyContact
    {
        [Display(Name = "Enter Name")]
        [Required(ErrorMessage = "Please Enter Name")]
        public string Name { get; set; }

        [Display(Name = "Enter Email")]
        [Required(ErrorMessage = "Please Enter Email")]
        public string Email { get; set; }

        [Display(Name = "Enter Mobile")]
        [Required(ErrorMessage = "Please Enter Mobile")]
        public string Mobile { get; set; }

        [Display(Name = "Enter Message")]
        [Required(ErrorMessage = "Please Enter Message")]
        public string Message { get; set; }
       
    }
}