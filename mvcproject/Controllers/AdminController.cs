using mvcproject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace mvcproject.Controllers
{
    public class AdminController : Controller
    {
        // GET: AdminController1
        public ActionResult Index()
        {
            
            ModelState.Clear();
            return View(GetProduct());
        }

        // GET: AdminController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminController1/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(ArtWork artwork)
        {
            try
            {
                
                if (ModelState.IsValid)
                {

                    if (AddProduct(artwork))
                    {
                        //ViewBag.Message("Product Added Succesfully");
                        
                        ModelState.Clear();
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

       
        public ActionResult Edit(int id)
        {

            return View(GetProduct().Find(artmodel => artmodel.art_id == id));
        }
        [HttpPost]
        public ActionResult Edit(int id, ArtWork artWork)
        {
            try
            {
                UpdateProductDetails(artWork);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController1/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                if (DeleteProduct(id))
                {
                    ViewBag.AlertMessage = "Product Deleted Succesfully";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private SqlConnection con;

        private void connection()
        {
            string constring = "Server=LAPTOP-NMP6MQB2\\MSSQL; Database=ArtGalleryManagement; Integrated Security=True;";
            con = new SqlConnection(constring);
        }

        public bool AddProduct(ArtWork artwork)
        {
            connection();
            SqlCommand cmd = new SqlCommand("AddNewProduct", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@title", artwork.title);
            cmd.Parameters.AddWithValue("@category_id", artwork.category_id);
            cmd.Parameters.AddWithValue("@price", artwork.price);
            cmd.Parameters.AddWithValue("@file_location", artwork.file_location);
            cmd.Parameters.AddWithValue("@user_id", artwork.user_id);
            cmd.Parameters.AddWithValue("@product_explanation", artwork.product_explanation);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }


        public List<ArtWork> GetProduct() {
            connection();
            List<ArtWork> productList = new List<ArtWork>();
            SqlCommand cmd = new SqlCommand("GetUserAndProductDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            sd.Fill(dt);
            con.Close();



            foreach (DataRow dr in dt.Rows)
            {
                productList.Add(
                    new ArtWork
                    {
                        art_id = Convert.ToInt32(dr["art_id"].ToString()),
                        title = dr["title"].ToString(),
                        category_id = Convert.ToInt32(dr["category_id"].ToString()),
                        price = Convert.ToDecimal(dr["price"].ToString()),
                        file_location = dr["file_location"].ToString(),
                        user_id = Convert.ToInt32(dr["user_id"].ToString()),
                        product_explanation = dr["product_explanation"].ToString(),
                        
                    }
                    );
            }
            return productList;
        }

        public List<ArtWork> GetUserAndProduct()
        {
            connection();
            List<ArtWork> productList = new List<ArtWork>();
            SqlCommand cmd = new SqlCommand("GetUserAndProductDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            sd.Fill(dt);
            con.Close();



            foreach (DataRow dr in dt.Rows)
            {
                productList.Add(
                    new ArtWork
                    {
                        art_id = Convert.ToInt32(dr["art_id"].ToString()),
                        title = dr["title"].ToString(),
                        category_id = Convert.ToInt32(dr["category_id"].ToString()),
                        price = Convert.ToDecimal(dr["price"].ToString()),
                        file_location = dr["file_location"].ToString(),
                        user_id = Convert.ToInt32(dr["user_id"].ToString()),
                        product_explanation = dr["product_explanation"].ToString(),
                        Users = new List<User>
                        {
                            new User{
                                name = dr["name"].ToString(),
                                surname = dr["surname"].ToString(),
                                phone_number = dr["phone_number"].ToString(),
                                email = dr["email"].ToString(),
                                age = Convert.ToInt32(dr["age"].ToString()),
                                city = dr["city"].ToString(),
                                country = dr["country"].ToString(),
                                street = dr["street"].ToString()
                            }
                        }
                    }
                    );
            }
            return productList;
        }

        public bool UpdateProductDetails(ArtWork artwork)
        {
            connection();
            SqlCommand cmd = new SqlCommand("UpdateProductDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@art_id", artwork.art_id);
            cmd.Parameters.AddWithValue("@title", artwork.title);
            cmd.Parameters.AddWithValue("@category_id", artwork.category_id);
            cmd.Parameters.AddWithValue("@price", artwork.price);
            cmd.Parameters.AddWithValue("@file_location", artwork.file_location);
            cmd.Parameters.AddWithValue("@user_id", artwork.user_id);
            cmd.Parameters.AddWithValue("@product_explanation", artwork.product_explanation);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        public bool DeleteProduct(int id)
        {
            connection();
            SqlCommand cmd = new SqlCommand("DeleteProduct", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@art_id", id);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
                return true;
            else
                return false;
        }



        public List<ArtWork> GetCategoryAndProduct()
        {
            connection();
            List<ArtWork> productList = new List<ArtWork>();
            SqlCommand cmd = new SqlCommand("GetCategoryAndProductDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            sd.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                productList.Add(
                    new ArtWork
                    {
                        art_id = Convert.ToInt32(dr["art_id"].ToString()),
                        title = dr["title"].ToString(),
                        category_id = Convert.ToInt32(dr["category_id"].ToString()),
                        price = Convert.ToDecimal(dr["price"].ToString()),
                        file_location = dr["file_location"].ToString(),
                        user_id = Convert.ToInt32(dr["user_id"].ToString()),
                        product_explanation = dr["product_explanation"].ToString(),
                        Categories = new List<Category>
                        {
                            new Category{
                                cId = Convert.ToInt32(dr["category_id"].ToString()),
                                cName = dr["name"].ToString()
                            }
                        }
                    }
                    );
            }
            return productList;
        }
    }
}
