using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace ProjectProcedureClinic.Models.Doctors
{
    public class ViewAppointment
    {
        [Display(Name = "Enter your Name")]
        [Required(ErrorMessage = "Please Enter Your Name")]
        public string Name { get; set; }

        [Display(Name = "Enter your Mobile")]
        [Required(ErrorMessage = "Plesae Enter Mobile")]
        public string Mobile { get; set; }

        [Display(Name = "Enter your Doctor Name")]
        [Required(ErrorMessage = "Plesae Enter Doctor Name")]
        public string DoctorName { get; set; }

        [Display(Name = "Enter your Disease")]
        [Required(ErrorMessage = "Please Enter your Disease")]
        public string Disease { get; set; }

        [Display(Name = "Enter your Age")]
        [Required(ErrorMessage = "Plesae Enter Age")]
        public string Age { get; set; }

        [Display(Name = "Enter your Appointment Date")]
        [Required(ErrorMessage = "Plesae Enter Appointment Date")]
        public string AppointmentDate { get; set; }

        [Display(Name = "Enter your Address")]
        [Required(ErrorMessage = "Plesae Enter Address")]
        public string Address { get; set; }

        [Display(Name = "Enter your Password")]
        [Required(ErrorMessage = "Plesae Enter Password")]
        public string Password { get; set; }
    }
}