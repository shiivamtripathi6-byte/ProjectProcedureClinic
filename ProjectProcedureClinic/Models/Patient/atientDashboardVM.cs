using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectProcedureClinic.Models.Patient
{
    public class atientDashboardVM
    {
        public string PatientName { get; set; }

        public int TotalAppointments { get; set; }
        public int UpcomingAppointments { get; set; }
        public List<PatientAppointmentVM> Appointments { get; set; }
    }
}