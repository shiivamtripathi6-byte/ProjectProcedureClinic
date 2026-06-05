using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using ProjectProcedureClinic.Models.Patient;
using System.Configuration;
using ProjectProcedureClinic.Models;



namespace ProjectProcedureClinic.Controllers
{
    public class PatientController : Controller
    {
        //
        // GET: /Patient/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Dashboard()
        {
            int mobile = Convert.ToInt32(Session["mobile"]);

            atientDashboardVM vm = new atientDashboardVM();
            vm.Appointments = new List<PatientAppointmentVM>();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("Sd_PatientDashboard", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mobile", mobile);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                // Summary
                if (dr.Read())
                {
                    vm.TotalAppointments = dr["TotalAppointments"] != DBNull.Value ? Convert.ToInt32(dr["TotalAppointments"]) : 0;
                    vm.UpcomingAppointments = dr["UpcomingAppointments"] != DBNull.Value ? Convert.ToInt32(dr["UpcomingAppointments"]) : 0;
                }

                // Appointments
                if (dr.NextResult())
                {
                    while (dr.Read())
                    {
                        vm.Appointments.Add(new PatientAppointmentVM
                        {
                            AppointmentDate = dr["Adate"] != DBNull.Value ? Convert.ToDateTime(dr["Adate"]) : DateTime.MinValue,
                            DoctorName = dr["doctor"].ToString(),
                            Disease = dr["disease"].ToString(),
                            Status = dr["status"].ToString(),
                        });
                    }
                }
                con.Close();
            }

            return View(vm);
        }


        public ActionResult Feedback()
        {
            return View();
        }
        [HttpPost]
        public JsonResult InsertFeedback(string Total, string Msg)
        {
            string userid = "";
            //attach session id like userid
           if (Session["userid"] != null)
           {
               userid = Session["userid"].ToString();
           }
           else
           {
               return Json("User not logged in!", JsonRequestBehavior.AllowGet);
           }
            string msg = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ConnectionString);
            if(con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("S_Feedback_insert", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@userid", userid);
            cmd.Parameters.AddWithValue("@rate", Total);
            cmd.Parameters.AddWithValue("@message", Msg);
            cmd.Parameters.AddWithValue("@fdate", DateTime.Now.ToString());
            cmd.Parameters.AddWithValue("@action", "insert");   
            int n = cmd.ExecuteNonQuery();
            if (n > 0)
                msg = "My Feedback Successfully Added";
            else
                msg = "Server Error";
            return Json(msg, JsonRequestBehavior.AllowGet);

        }
      public ActionResult LogOut()
        {
            return View();
        }
        [HttpPost]
      public ActionResult LogOut(string name)
      {
          string pid = Session["pid"].ToString();
            if (pid != null && pid != "")
            {
                Session.RemoveAll();
                Session.Clear();
                Response.Redirect("/Home/Login");
            }
            else
            {
                Response.Redirect("/Home/Login");
            }
          return View();
      }
        [HttpPost]
        public ActionResult Index(string txtenc)
        {
            Encryption_Decryption enc = new Encryption_Decryption();
            string en = enc.Encryption(txtenc);
            ViewBag.enc = en;
            string dc = enc.Decryption(en);
            ViewBag.desc = dc;
            return View();
        }
        [HttpGet]
        public ActionResult Change_Password()
        {
            return View();
        }
        [HttpPost]
     public ActionResult Change_Password(string oldpass, string newpass, string confirmpass)
     {
         if (Session["userid"] == null)
             return RedirectToAction("Login");
    if (newpass != confirmpass)
    {
        ViewBag.msg = "New password and confirm password do not match";
        return View();
    }

    long userid = Convert.ToInt64(Session["userid"]); 

    using (SqlConnection con = new SqlConnection(
           ConfigurationManager.ConnectionStrings["mycon"].ConnectionString))
    {
        SqlCommand cmd = new SqlCommand("SP_ChangePasswords", con);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@userid", userid);
        cmd.Parameters.AddWithValue("@oldpass", oldpass);
        cmd.Parameters.AddWithValue("@newpass", newpass);

        SqlParameter output = new SqlParameter("@result", SqlDbType.Int);
        output.Direction = ParameterDirection.Output;
        cmd.Parameters.Add(output);
        con.Open();

        cmd.ExecuteNonQuery();
        int res = Convert.ToInt32(cmd.Parameters["@result"].Value);
            if (res == 1)
                ViewBag.msg = "Password change successfully";
            else
                ViewBag.msg = "unable to update";
         }
            return View();
      }
        [HttpGet]
        public ActionResult AddComplain()
        {
            return View();
        }

    [HttpPost]
    public ActionResult AddComplain(Complain complain)
    {
        if (Session["PatientName"] != null)
            complain.PatientName = Session["PatientName"].ToString();

        if (Session["PatientMobile"] != null)
            complain.Mobile = Session["PatientMobile"].ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ConnectionString);
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
            SqlCommand cmd = new SqlCommand("SP_PatientComplain", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PatientName", complain.PatientName);
            cmd.Parameters.AddWithValue("@Mobile", complain.Mobile);
            cmd.Parameters.AddWithValue("@ComplainText", complain.ComplainText);
            cmd.Parameters.AddWithValue("@Action", "insert");
            cmd.Parameters.AddWithValue("@status", "yes");
            int n = cmd.ExecuteNonQuery();
            con.Close();
         if (n > 0)
                 ViewBag.show = "Patient Details Added";
             else
                 ViewBag.show = "Server error";
         return View(complain);
        }
        
    }
   
            
            
           
         
            
             
   
}

