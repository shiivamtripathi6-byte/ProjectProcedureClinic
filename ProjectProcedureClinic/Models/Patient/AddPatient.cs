using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProjectProcedureClinic.Models.Patient
{
    public class AddPatient
    {
        [Required(ErrorMessage = "Please enter Name")]
        [Display(Name = "Patient Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter Mobile")]
        [Display(Name = "Patient Mobile")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Please enter Doctor name")]
        [Display(Name = "Enter doctor name")]
        public string DoctorName { get; set; }

        [Required(ErrorMessage = "Please enter Disease")]
        [Display(Name = "Patient Disease")]
        public string Disease { get; set; }

        [Required(ErrorMessage = "Please enter Age")]
        [Display(Name = "Patient Age")]
        public string Age { get; set; }

        [Required(ErrorMessage = "Please enter Appointment Date")]
        [Display(Name = "Enter Appointment Date")]
        public string AppointmentDate { get; set; }

        [Required(ErrorMessage = "Please enter Address")]
        [Display(Name = "Patient Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter Password")]
        [Display(Name = "Patient Password")]
        public string Password{ get; set; }
    }
}