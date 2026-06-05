using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectProcedureClinic.Models.Doctors;
using ProjectProcedureClinic.Models.Admin;
using ProjectProcedureClinic.Models.Patient;
using System.Configuration;
using System.IO;
using System.Data.SqlClient;
using System.Data;

namespace ProjectProcedureClinic.Controllers
{
    public class DoctorController : Controller
    {
        //
        // GET: /Doctor/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Dashboard()
        {
            ViewBag.show = Convert.ToString(Session["did"]);
            return View();
        }
        [HttpGet]
        public ActionResult UploadPic()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadPic(Add_Doctors_Picture add)
        {
            string path = Path.Combine(Server.MapPath("~/Content/images/DoctorsPic/"), add.fupic.FileName);
            add.fupic.SaveAs(path);
            SqlConnection con=new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ConnectionString);
               if(con.State==ConnectionState.Closed)
               {
                   con.Open();
               }
            SqlCommand cmd=new SqlCommand("S_Insert_Doctor_Picture",con);
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pname",add.fupic.FileName);
            cmd.Parameters.AddWithValue("@dmobile",add.DoctorId);
            cmd.Parameters.AddWithValue("@pdate",DateTime.Now.ToString());
            int n = cmd.ExecuteNonQuery();
            if(n>0)
            {
                ViewBag.show="Doctor Picture Details Added";
            }
            else
            {
                ViewBag.show="Server Error";
            }
            return View();
        }
         public ActionResult View_Appointment()
        {
            ViewAppointment ec = null;
            List<ViewAppointment> lst = new List<ViewAppointment>();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ConnectionString);
            SqlCommand cmd = new SqlCommand("Saa_Patients_Appointments", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", "select");
             SqlDataAdapter sa = new SqlDataAdapter(cmd);
             DataSet ds = new DataSet();
            sa.Fill(ds);
            if(ds.Tables[0].Rows.Count > 0)
            {
                for(int i = 0;i < ds.Tables[0].Rows.Count;i++)
                {
                    ec = new ViewAppointment();
                    ec.Name = ds.Tables[0].Rows[i]["name"].ToString();
                    ec.Mobile = ds.Tables[0].Rows[i]["mobile"].ToString();
                    ec.DoctorName = ds.Tables[0].Rows[i]["doctor"].ToString();
                    ec.Disease = ds.Tables[0].Rows[i]["disease"].ToString();
                     ec.Age = ds.Tables[0].Rows[i]["age"].ToString();
                     ec.AppointmentDate = ds.Tables[0].Rows[i]["Adate"].ToString();
                     ec.Address = ds.Tables[0].Rows[i]["Address"].ToString();
                     ViewBag.Status = ds.Tables[0].Rows[i]["status"];
                    lst.Add(ec);
                }
            }
            else
            {
                ViewBag.msg = "no records found";
            }
            return View(lst);
        }
        [HttpGet]
        public ActionResult Update_View_Appointment(string up)
        {
            
            AddPatient ec = null;
            List<AddPatient> lst = new List<AddPatient>();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ConnectionString);
            SqlCommand cmd = new SqlCommand("Saa_Patients_Appointments", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", "getByMobile");
            cmd.Parameters.AddWithValue("@mobile", up);
            SqlDataAdapter sa = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sa.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ec = new AddPatient();
                    ec.Name = dt.Rows[0]["name"].ToString();
                    ec.Mobile = dt.Rows[0]["mobile"].ToString();
                    ec.DoctorName = dt.Rows[0]["doctor"].ToString();
                    ec.Disease = dt.Rows[0]["disease"].ToString();
                    ec.Age = dt.Rows[0]["age"].ToString();
                    ec.AppointmentDate = dt.Rows[0]["Adate"].ToString();
                    ec.Address = dt.Rows[0]["Address"].ToString();
                    
                   
                }
            }
          return View(ec);
        }
        [HttpPost]
        public ActionResult Update_View_Appointment(AddPatient ec)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("Saa_Patients_Appointments", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@action", "update");

                // Important parameters
                cmd.Parameters.AddWithValue("@name", ec.Name);
                cmd.Parameters.AddWithValue("@mobile", ec.Mobile);
                cmd.Parameters.AddWithValue("@age", ec.Age);
                cmd.Parameters.AddWithValue("@Address", ec.Address);
               
                con.Open();
                int n = cmd.ExecuteNonQuery();

                if (n > 0)
                    ViewBag.msg = "Updated Successfully";
                else
                    ViewBag.msg = "Update Failed";
            }

            return View(ec);
        }
        [HttpPost]
        public ActionResult Delete_View_Appointment(AddPatient ec)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("Saa_Patients_Appointments", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@action", "delete");

                // Important parameters
                cmd.Parameters.AddWithValue("@name", ec.Name);
                cmd.Parameters.AddWithValue("@mobile", ec.Mobile);
                cmd.Parameters.AddWithValue("@doctor",ec.DoctorName);
                cmd.Parameters.AddWithValue("@disease",ec.Disease);
                cmd.Parameters.AddWithValue("@age", ec.Age);
                cmd.Parameters.AddWithValue("@Adate", ec.AppointmentDate);
                cmd.Parameters.AddWithValue("@Address", ec.Address);

                con.Open();
                int n = cmd.ExecuteNonQuery();

                if (n > 0)
                    ViewBag.msg = "Deleted Successfully";
                else
                    ViewBag.msg = "Deleted Failed";
            }

            return View(ec);
        }
        public ActionResult LogOut()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogOut(string name)
        {
            string userid = Session["userid"].ToString();
            if (userid != null && userid != "")
            {
                Session.RemoveAll();
                Session.Clear();
                Response.Redirect("/Home/Login");
            }
            else
            {
                Response.Redirect("/Doctor/Logout");
            }

            return View();
        }
        public ActionResult ViewComplain() 
    {
        ViewComplain ec = null;
            List<ViewComplain> lst = new List<ViewComplain>();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_PatientComplain", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "select");
             SqlDataAdapter sa = new SqlDataAdapter(cmd);
             DataSet ds = new DataSet();
            sa.Fill(ds);
            if(ds.Tables[0].Rows.Count > 0)
            {
                for(int i = 0;i < ds.Tables[0].Rows.Count;i++)
                {
                     ec = new ViewComplain();
                    ec.ComplainID =  Convert.ToInt32(ds.Tables[0].Rows[i]["ComplainID"]);
                    ec.PatientName = ds.Tables[0].Rows[i]["PatientName"].ToString();
                    ec.Mobile = ds.Tables[0].Rows[i]["Mobile"].ToString();
                    ec.ComplainText = ds.Tables[0].Rows[i]["ComplainText"].ToString();
                     ec.Date = Convert.ToDateTime(ds.Tables[0].Rows[i]["Date"]);
                    ec.Status = ds.Tables[0].Rows[i]["Status"].ToString();
                     lst.Add(ec);
                }
            }
            else
            {
                ViewBag.msg = "no records found";
            }
            return View(lst);
          }
    }
}
