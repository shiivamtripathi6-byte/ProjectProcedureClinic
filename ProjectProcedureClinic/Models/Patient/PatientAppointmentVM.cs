using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectProcedureClinic.Models.Patient
{
    public class PatientAppointmentVM
    {
        public DateTime AppointmentDate { get; set; }
        public string DoctorName { get; set; }
        public string Disease { get; set; }
        public string Status { get; set; }
    }
}