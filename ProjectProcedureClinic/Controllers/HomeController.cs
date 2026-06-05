using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using ProjectProcedureClinic.Models.Patient;
using ProjectProcedureClinic.Models.Home;
using ProjectProcedureClinic.Models.Admin;
using ProjectProcedureClinic.Models;
using System.Configuration;

namespace ProjectProcedureClinic.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Dashboard()
        {
            ViewBag.show = Convert.ToString(Session["uid"]);
            return View();
        }
        public ActionResult Index()
        {
            View_Doctor ec = null;
            List<View_Doctor> lst = new List<View_Doctor>();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ConnectionString);
            SqlCommand cmd = new SqlCommand("Spa_Doctors_Pictures", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@dmobile", 6787654604);

            SqlDataAdapter sa = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sa.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ec = new View_Doctor();
                    ec.Name = ds.Tables[0].Rows[i]["dname"].ToString();
                    ec.Qualification = ds.Tables[0].Rows[i]["dqualification"].ToString();
                    ec.Mobile_No = ds.Tables[0].Rows[i]["dmobile"].ToString();
                    ec.Specilist = ds.Tables[0].Rows[i]["dspecilist"].ToString();
                    ec.Amount = ds.Tables[0].Rows[i]["damount"].ToString();
                    ec.Time = ds.Tables[0].Rows[i]["dtime"].ToString();
                    ec.Password = ds.Tables[0].Rows[i]["dpass"].ToString();
                    ec.fupic = ds.Tables[0].Rows[i]["pname"].ToString();
                    lst.Add(ec);
                }
            }
            return View(lst);
        }
        public ActionResult AboutUs()
        {
            return View();
        }
       
        public ActionResult AddPatient()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddPatient(AddPatient pp)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ConnectionString);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("Saa_Patients_Appointments", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", pp.Name);
            cmd.Parameters.AddWithValue("@mobile", pp.Mobile);
            cmd.Parameters.AddWithValue("@doctor", pp.DoctorName);
            cmd.Parameters.AddWithValue("@disease", pp.Disease);
            cmd.Parameters.AddWithValue("@age", pp.Age);
            cmd.Parameters.AddWithValue("@Adate", pp.AppointmentDate);
            cmd.Parameters.AddWithValue("@Address", pp.Address);
            cmd.Parameters.AddWithValue("@status", "yes");
            cmd.Parameters.AddWithValue("@Password", pp.Password);
            cmd.Parameters.AddWithValue("@type", "patient");
            cmd.Parameters.AddWithValue("@action", "insert");
           int n = cmd.ExecuteNonQuery();
           SqlCommand cmd2 = new SqlCommand("Saa_Check_login1", con);
           cmd2.CommandType = CommandType.StoredProcedure;
           cmd2.Parameters.AddWithValue("@userid", pp.Mobile);
           cmd2.Parameters.AddWithValue("@passwd", pp.Password);
           cmd2.Parameters.AddWithValue("@status","yes");
           cmd2.Parameters.AddWithValue("@type", "patient");
           cmd2.ExecuteNonQuery();
             if (n > 0)
                 ViewBag.show = "Patient Details Added";
             else
                 ViewBag.show = "Server error";
             return View(pp);
        }
        [HttpGet]
         public ActionResult Login()
        {
            CaptchaManager cg = new CaptchaManager();
            ViewBag.cph = cg.CaptchaCode();
            return View();
        }
        [HttpPost]
         public ActionResult Login(Login lg, string txtcaptcha, string txtcaptchacode)
         {
             CaptchaManager cg = new CaptchaManager();
             ViewBag.cph = cg.CaptchaCode();

             using (SqlConnection con = new SqlConnection(
             ConfigurationManager.ConnectionStrings["mycon"].ConnectionString))
             {
                 SqlDataAdapter da = new SqlDataAdapter("sp_login", con);
                 da.SelectCommand.CommandType = CommandType.StoredProcedure;

                 da.SelectCommand.Parameters.AddWithValue("@userid", lg.UserId);
                 da.SelectCommand.Parameters.AddWithValue("@passwd", lg.Password);
                
                 DataSet ds = new DataSet();
                 da.Fill(ds);

                 if (txtcaptcha == txtcaptchacode)
                 {
                     if (ModelState.IsValid)
                     {
                         if (ds.Tables[0].Rows.Count > 0)
                         {
                             string type = ds.Tables[0].Rows[0]["type"].ToString().ToLower();

                             Session["userid"] = lg.UserId;
                             Session["type"] = type;

                             if (type == "admin")
                             {
                                 return RedirectToAction("Dashboard", "admin");
                             }
                                 
                             else if (type == "doctor")
                             {
                                 return RedirectToAction("Dashboard", "doctor");
                             }

                             else if (type == "patient")
                             {
                                 return RedirectToAction("Dashboard", "patient");
                             }
                             else
                             {
                                 ViewBag.msg = "Invalid type.";
                             }
                       }
                         else
                         {
                             ViewBag.msg = "No record found.";
                         }
                     }
                 }
                 else
                 {
                     ViewBag.msg = "Captcha does not match.";
                 }
             }

             return View();
         }
        [HttpPost]
        
        public JsonResult RefreshCaptcha()
        {
            CaptchaManager cg = new CaptchaManager();
            string msg = cg.CaptchaCode();
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
       
       public JsonResult InsertEnquiry(Contact contact)
        {
            string msg = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ConnectionString);
            if(con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("S_Insert_ccontact", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", contact.Name);
            cmd.Parameters.AddWithValue("@email", contact.Email);
            cmd.Parameters.AddWithValue("@mobile", contact.Mobile);
            cmd.Parameters.AddWithValue("@msg", contact.Message);
            int n = cmd.ExecuteNonQuery();
            if (n > 0)
                msg = "My Enquiry Added";
            else
                msg = "Server Error";
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Contact()
       {
           return View();
       }
        //code for display contact
        public JsonResult DisplayContact()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ConnectionString);
            List<Contact> lst = new List<Contact>();
            DataTable dt = new DataTable();
            SqlDataAdapter sa = new SqlDataAdapter("S_Display_ccontact", con);
            sa.Fill(dt);
            if(dt.Rows.Count > 0)
            {
                for(int i = 0; i < dt.Rows.Count; i++)
                {
                    Contact ad = new Contact();
                    ad.Name = dt.Rows[i]["name"].ToString();
                    ad.Email = dt.Rows[i]["email"].ToString();
                    ad.Mobile = dt.Rows[i]["mobile"].ToString();
                    ad.Message = dt.Rows[i]["msg"].ToString();
                    lst.Add(ad);
                }
            }
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Service()
        {
            return View();
        }
      
        public ActionResult MyContact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult MyContact(MyContact cm)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ConnectionString);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("S_Insert_ccontact", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", cm.Name);
            cmd.Parameters.AddWithValue("@email", cm.Email);
            cmd.Parameters.AddWithValue("@mobile", cm.Mobile);
            cmd.Parameters.AddWithValue("@msg", cm.Message);
            int n = cmd.ExecuteNonQuery();
            if (n > 0)
                ViewBag.show = "Contact Details Added";
            else
                ViewBag.show = "Server error";
            return View();
        }
        
    }
}
