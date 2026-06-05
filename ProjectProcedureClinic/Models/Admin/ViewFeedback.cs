using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectProcedureClinic.Models.Admin
{
    public class ViewFeedback
    {

        public int fid { get; set; }
        public string userid { get; set; }
        public int rate { get; set; }
        public string message { get; set; }
        public string fdate { get; set; }
    }
}