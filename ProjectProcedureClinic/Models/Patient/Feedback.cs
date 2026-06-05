using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectProcedureClinic.Models.Patient
{
    public class Feedback
    {
        public int fid { get; set; }
        public string userid { get; set; }
        public int rate { get; set; }
        public string message { get; set; }
        public string fdate { get; set; }
    }
  }