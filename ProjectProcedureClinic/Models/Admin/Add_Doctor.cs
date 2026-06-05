using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProjectProcedureClinic.Models.Admin
{
    public class Add_Doctor
    {
        [Display(Name="Enter Your name")]
        [Required(ErrorMessage="Please Enter your Name")]
        [StringLength(20,ErrorMessage="Name is out of range")]
        public string Doctor_Name { get; set; }

        [Display(Name="Enter your Qualification")]
        [Required(ErrorMessage="Please Enter your Qualification")]
        public string Qualification { get; set; }

        [Display(Name="Enter your Mobile Number")]
        [Required(ErrorMessage="Please Enter your Mobile Number")]
        public string Mobile_No { get; set; }

        [Display(Name="Enter your Specialization")]
        [Required(ErrorMessage="Please Enter your Specialization")]
        public string Specialist { get; set; }

        [Display(Name="Enter Amount")]
        [Required(ErrorMessage="Please Enter your Amount")]
        public string Amount { get; set; }

        [Display(Name="Enter your timing")]
        [Required(ErrorMessage="Please Enter your Timing")]
        public string Doctor_Timing { get; set; }

        [Display(Name="Please Enter Password")]
        [Required(ErrorMessage="Please Enter your Password")]
        public string Doctor_Password { get; set; }
        
    }
}