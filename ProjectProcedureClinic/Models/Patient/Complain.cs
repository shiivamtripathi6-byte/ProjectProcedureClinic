using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace ProjectProcedureClinic.Models.Patient
{
    public class Complain
    {
        [Key]
        public int ComplainID { get; set; }

        [Required]
        [StringLength(50)]
        public string PatientName { get; set; }

        [Required]
        [StringLength(50)]
        public string Mobile { get; set; }

        [Required]
        [StringLength(200)]
        public string ComplainText { get; set; }

        public DateTime Date { get; set; }

        [StringLength(50)]
        public string Status { get; set; }
        public Complain()
        {
            Date = DateTime.Now;
            Status = "Pending";
        }
    }
}