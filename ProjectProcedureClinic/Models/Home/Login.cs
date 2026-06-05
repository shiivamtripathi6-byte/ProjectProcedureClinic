using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ProjectProcedureClinic.Models.Home;


namespace ProjectProcedureClinic.Models.Home
{
    public class Login
    {
        [Display(Name = "Enter Name")]
        [Required(ErrorMessage = "Please Enter Name")]
        public string UserId { get; set; }

        [Display(Name = "Enter Password")]
        [Required(ErrorMessage = "Please Enter Password")]

        public string Password { get; set; }
       

        

        
    }
}