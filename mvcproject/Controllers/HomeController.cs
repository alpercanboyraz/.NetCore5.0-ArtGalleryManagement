using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using mvcproject.Models;

namespace mvcproject.Controllers
{
    public class HomeController : Controller
    {
        private SqlConnection con;

        private void connection()
        {
            string constring = "Server=LAPTOP-NMP6MQB2\\MSSQL; Database=ArtGalleryManagement; Integrated Security=True;";
            con = new SqlConnection(constring);
        }
        private readonly IConfiguration configuration;

        public object FormsAuthentication { get; private set; }

        public HomeController(IConfiguration config)
        {
            this.configuration = config;
        }

        [HttpPost]
        public ActionResult showuserinput(string input1)
        {
            return Redirect("/Home/Index/" + input1);
        } 


        public ActionResult showPub()
        {
            List<Models.User> list = new List<Models.User>();
            try
            {
                // runs stored procedure and returns data to main page
                using (SqlConnection con = new SqlConnection())
                {
                    String sql = @"select * from [User]";
                    con.ConnectionString = "Server=LAPTOP-NMP6MQB2\\MSSQL; Database=ArtGalleryManagement; Integrated Security=True;";
                    

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = new SqlCommand(sql, con);

                    da.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        var pub = new Models.User();
                        pub.name = row["name"].ToString();
                        pub.surname = row["surname"].ToString();

                        list.Add(pub);
                    }
                }
                return View(list);
            }
            catch
            {
                return View("Error");
            }
        }

        public ActionResult Index(string input1)
        {
            return View();
        }
        
        public IActionResult Painting()
        {
            List<Models.ArtWork> list = new List<Models.ArtWork>();
            try
            {
                // runs stored procedure and returns data to main page
                using (SqlConnection con = new SqlConnection())
                {
                    String sql = @"select * from [ArtWork] where category_id = 1";
                    con.ConnectionString = "Server=LAPTOP-NMP6MQB2\\MSSQL; Database=ArtGalleryManagement; Integrated Security=True;";


                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = new SqlCommand(sql, con);

                    da.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        var pub = new Models.ArtWork();
                        pub.art_id = int.Parse(row["art_id"].ToString());
                        pub.title = row["title"].ToString();
                        pub.price = decimal.Parse(row["price"].ToString());
                        pub.file_location = row["file_location"].ToString();
                        pub.category_id = int.Parse(row["category_id"].ToString());
                        
                        list.Add(pub);
                        
                        
                    }
                }
                ViewBag.Alper = "Alper";
                return View(list);
            }
            catch
            {
                return View("Error");
            }

        }
    
        public IActionResult Sculpture()
        {
            List<Models.ArtWork> list = new List<Models.ArtWork>();
            try
            {
                // runs stored procedure and returns data to main page
                using (SqlConnection con = new SqlConnection())
                {
                    String sql = @"select * from [ArtWork] WHERE category_id = 2";
                    con.ConnectionString = "Server=LAPTOP-NMP6MQB2\\MSSQL; Database=ArtGalleryManagement; Integrated Security=True;";


                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = new SqlCommand(sql, con);

                    da.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        var pub = new Models.ArtWork();
                        pub.art_id = int.Parse(row["art_id"].ToString());
                        pub.title = row["title"].ToString();
                        pub.price = decimal.Parse(row["price"].ToString());
                        pub.file_location = row["file_location"].ToString();
                        pub.category_id = int.Parse(row["category_id"].ToString());
                       
                        list.Add(pub);
                        
                        
                    }
                }
                
                return View(list);
            }
            catch
            {
                return View("Error");
            }
        }
   
        public IActionResult Music()
        {
            List<Models.ArtWork> list = new List<Models.ArtWork>();
            try
            {
                // runs stored procedure and returns data to main page
                using (SqlConnection con = new SqlConnection())
                {
                    String sql = @"select * from [ArtWork]";
                    con.ConnectionString = "Server=LAPTOP-NMP6MQB2\\MSSQL; Database=ArtGalleryManagement; Integrated Security=True;";


                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = new SqlCommand(sql, con);

                    da.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        var pub = new Models.ArtWork();
                        pub.art_id = int.Parse(row["art_id"].ToString());
                        pub.title = row["title"].ToString();
                        pub.price = decimal.Parse(row["price"].ToString());
                        pub.file_location = row["file_location"].ToString();
                        pub.category_id = int.Parse(row["category_id"].ToString());
                        if (pub.category_id == 2)
                        {
                            list.Add(pub);
                        }

                    }
                }

                return View(list);
            }
            catch
            {
                return View("Error");
            }
        }
        [HttpGet]
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult login(User user)
        {
            if (loginisvalid(user.name, user.password))
            {
                
                return RedirectToAction("Index", "Admin");

            }
            else
            {

                ModelState.AddModelError("", "Your Email and password is incorrect");
                
            }
            return View(user);

        }
        public bool loginisvalid(string name,string password)
        {


            connection();
            bool IsValid = false;
            string query = "select * from [User] where name=@name AND password=@password";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@password", password);
                /*
                SqlParameter unitsParam = cmd.Parameters.AddWithValue("@name", name);
                if (name == null)
                {
                    unitsParam.Value = DBNull.Value;
                }
                SqlParameter unitsParam2 = cmd.Parameters.AddWithValue("@password", password);
                if (password == null)
                {
                    unitsParam2.Value = DBNull.Value;
                }
                */
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    IsValid = true;
                }
            }
            return IsValid;
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult review(int id)
        {
            return View(new AdminController().GetUserAndProduct().Find(artmodel => artmodel.art_id == id));
        }
    }
}
