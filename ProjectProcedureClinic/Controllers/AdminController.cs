using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectProcedureClinic.Models.Patient;
using ProjectProcedureClinic.Models.Admin;
using ProjectProcedureClinic.Models.Home;
using ProjectProcedureClinic.Models.Doctors;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ProjectProcedureClinic.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Dashboard()
        {

            int doctors = 0, patients = 0, todayAppointments = 0;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ConnectionString);
            SqlCommand cmd = new SqlCommand("Ssa_GetDashboardCounts", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                doctors = Convert.ToInt32(dr["TotalDoctors"]);
                patients = Convert.ToInt32(dr["TotalPatients"]);
                todayAppointments = Convert.ToInt32(dr["TodayAppointments"]);

            }
            con.Close();

            ViewBag.Doctors = doctors;
            ViewBag.Patients = patients;
            ViewBag.TodayAppointments = todayAppointments;
            ViewBag.show = Convert.ToString(Session["aid"]);
            List<string> months = new List<string>();
            List<int> patientCounts = new List<int>();

                SqlCommand cmd3 = new SqlCommand("Ssa_GetPatientMonthly", con);
                cmd3.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr1 = cmd3.ExecuteReader();
                while (dr.Read())
                {
                    months.Add(dr["MonthName"].ToString());
                    patientCounts.Add(Convert.ToInt32(dr["TotalPatients"]));
                }
                dr1.Close();
            ViewBag.Months = months;
            ViewBag.PatientCounts = patientCounts;
             View_Doctor ec = null;
            List<View_Doctor> lst = new List<View_Doctor>();
            SqlCommand cmd2 = new SqlCommand("Saa_IAddDoctors", con);
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.AddWithValue("@action", "select");
            SqlDataAdapter sa = new SqlDataAdapter(cmd2);
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
        public ActionResult Add_Doctor()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add_Doctor(Add_Doctor ad)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ConnectionString);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("Saa_IAddDoctors", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dname", ad.Doctor_Name);
            cmd.Parameters.AddWithValue("@dqualification", ad.Qualification);
            cmd.Parameters.AddWithValue("@dmobile", ad.Mobile_No);
            cmd.Parameters.AddWithValue("@dspecilist", ad.Specialist);
            cmd.Parameters.AddWithValue("@damount", Convert.ToInt32(ad.Amount));
            cmd.Parameters.AddWithValue("@dtime", ad.Doctor_Timing);
            cmd.Parameters.AddWithValue("@dpass", ad.Doctor_Password);
            cmd.Parameters.AddWithValue("@action", "insert");
            cmd.Parameters.AddWithValue("@type", "doctor");
            cmd.Parameters.AddWithValue("@status", "yes");
            int n = cmd.ExecuteNonQuery();
            SqlCommand cmd2 = new SqlCommand("Saa_Check_login1", con);
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.AddWithValue("@userid", ad.Mobile_No);
            cmd2.Parameters.AddWithValue("@passwd", ad.Doctor_Password);
            cmd2.Parameters.AddWithValue("@status", "yes");
            cmd2.Parameters.AddWithValue("@type", "doctor");
            cmd2.ExecuteNonQuery();
            if (n > 0)
                ViewBag.show = "Doctor Details Added";
            else
                ViewBag.show = "Server error";
            return View();
        }
        public ActionResult View_Doctor()
        {
            View_Doctor ec = null;
            List<View_Doctor> lst = new List<View_Doctor>();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ConnectionString);
            SqlCommand cmd = new SqlCommand("Saa_IAddDoctors", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", "select");
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
        public ActionResult Update_View_Doctor(string up)
        {
            Add_Doctor ec = null;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ConnectionString);
            SqlCommand cmd = new SqlCommand("Saa_IAddDoctors", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", "getByMobile");
            cmd.Parameters.AddWithValue("@dmobile", up);

            SqlDataAdapter sa = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sa.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                ec = new Add_Doctor
                {
                    Doctor_Name = dt.Rows[0]["dname"].ToString(),
                    Qualification = dt.Rows[0]["dqualification"].ToString(),
                    Mobile_No = dt.Rows[0]["dmobile"].ToString(),
                    Specialist = dt.Rows[0]["dspecilist"].ToString(),
                    Amount = dt.Rows[0]["damount"].ToString(),
                    Doctor_Timing = dt.Rows[0]["dtime"].ToString(),
                    Doctor_Password = dt.Rows[0]["dpass"].ToString()
                };
            }
            return View(ec);
        }

        [HttpPost]
        public ActionResult Update_View_Doctor(Add_Doctor ec)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("Saa_IAddDoctors", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@action", "update");

                cmd.Parameters.AddWithValue("@dname", ec.Doctor_Name);
                cmd.Parameters.AddWithValue("@dmobile", ec.Mobile_No);
                cmd.Parameters.AddWithValue("@damount", ec.Amount);
                cmd.Parameters.AddWithValue("@dqualification", ec.Qualification);
                cmd.Parameters.AddWithValue("@dspecilist", ec.Specialist);   // include specialist if required
                cmd.Parameters.AddWithValue("@dtime", ec.Doctor_Timing);     // include timing if required
                cmd.Parameters.AddWithValue("@dpass", ec.Doctor_Password);   // include password if required

                con.Open();
                int n = cmd.ExecuteNonQuery();

                ViewBag.msg = (n > 0) ? "Updated Successfully" : "Update Failed";
            }

            return View(ec);
        }
        [HttpGet]
        public ActionResult Delete_View_Doctor(string Mobile)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("Saa_Patients_Appointments", con);
                cmd.CommandType = CommandType.StoredProcedure;

                // Important parameters
                cmd.Parameters.AddWithValue("@mobile", Mobile);
                cmd.Parameters.AddWithValue("@action", "delete");
                con.Open();
                int n = cmd.ExecuteNonQuery();
                con.Close();
                if (n > 0)
                    ViewBag.msg = "Deleted Successfully";
                else
                    ViewBag.msg = "Deleted Failed";
            }

            return RedirectToAction("View_Doctor");

        }

        public ActionResult View_Appointment()
        {
            View_Appointment ec = null;
            List<View_Appointment> lst = new List<View_Appointment>();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ConnectionString);
            SqlCommand cmd = new SqlCommand("Saa_Patients_Appointments", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", "select");
            SqlDataAdapter sa = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sa.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ec = new View_Appointment();
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
       [HttpGet]
        public ActionResult Delete_View_Appointment(string Mobile)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("Saa_Patients_Appointments", con);
                cmd.CommandType = CommandType.StoredProcedure;

                // Important parameters
                cmd.Parameters.AddWithValue("@mobile", Mobile);
                cmd.Parameters.AddWithValue("@action", "delete");
                con.Open();
                int n = cmd.ExecuteNonQuery();
                con.Close();
                if (n > 0)
                    ViewBag.msg = "Deleted Successfully";
                else
                    ViewBag.msg = "Deleted Failed";
            }

            return RedirectToAction("View_Appointment");

        }

        //code for display contact
        public ActionResult DisplayMyContact()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ConnectionString);
            List<MyContact> lst = new List<MyContact>();
            DataTable dt = new DataTable();
            SqlDataAdapter sa = new SqlDataAdapter("S_Display_ccontact", con);
            sa.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    MyContact ad = new MyContact();
                    ad.Name = dt.Rows[i]["name"].ToString();
                    ad.Email = dt.Rows[i]["email"].ToString();
                    ad.Mobile = dt.Rows[i]["mobile"].ToString();
                    ad.Message = dt.Rows[i]["msg"].ToString();
                    lst.Add(ad);
                }
            }
            return View(lst);
        }
        public ActionResult View_Enquiry()
        {
            return View();
        }
      
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
                Response.Redirect("/Home/Login");
            }
            return View();
        }
        [HttpGet]
        public ActionResult ViewFeedback()
        {
            List<ViewFeedback> lst = new List<ViewFeedback>();

            SqlConnection con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["mycon"].ConnectionString);

            SqlCommand cmd = new SqlCommand("S_Feedback_insert", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", "select");

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ViewFeedback ad = new ViewFeedback();
                    ad.fid = Convert.ToInt32(dt.Rows[i]["fid"]);
                    ad.userid = dt.Rows[i]["userid"].ToString();
                    ad.rate = Convert.ToInt32(dt.Rows[i]["rate"]);
                    ad.message = dt.Rows[i]["message"].ToString();
                    ad.fdate = dt.Rows[i]["fdate"].ToString();
                    lst.Add(ad);
                }
            }
            return View(lst);
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
