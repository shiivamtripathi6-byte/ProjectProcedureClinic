using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProjectProcedureClinic.Models.Admin
{
    public class View_Doctor
    {
        [Display(Name = "Please Enter your name")]
        [Required(ErrorMessage = "Please Enter Name")]
        public string Name { get; set; }

        [Display(Name = "Enter Your Qualification")]
        [Required(ErrorMessage = "Please Enter your Qualification")]
        public string Qualification { get; set; }

        [Display(Name = "Please Enter your Mobile")]
        [Required(ErrorMessage = "Enter Mobile")]
        public string Mobile_No { get; set; }

        [Display(Name = "Please Enter your Specialization")]
        [Required(ErrorMessage = "Please Enter Specialization")]
        public string Specilist { get; set; }

        [Display(Name = "Please Enter your Amount")]
        [Required(ErrorMessage = "Enter Amount")]
        public string Amount { get; set; }

        [Display(Name = "Please Enter Time")]
        [Required(ErrorMessage = "Please Enter Time")]
        public string Time { get; set; }

        [Display(Name = "Please Enter Password")]
        [Required(ErrorMessage = "Please Enter Password")]
        public string Password { get; set; }

        public object fupic { get; set; }
    }
}