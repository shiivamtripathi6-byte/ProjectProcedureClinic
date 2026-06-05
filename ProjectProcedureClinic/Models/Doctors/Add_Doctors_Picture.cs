using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectProcedureClinic.Models.Doctors
{
    public class Add_Doctors_Picture
    {
        public string DoctorId { get; set; }
        public HttpPostedFileBase fupic { get; set; }
        
    }
}