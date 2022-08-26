using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using PaintManagement.Models;

namespace PaintManagement.Controllers
{
    public class ImageController : Controller
    {
        public ActionResult Index()
        {
            List<Image> images = GetImages();
            return View(images);
        }

        [HttpPost]
        public ActionResult Index(int imageId)
        {
            List<Image> images = GetImages();
            Image image = images.Find(p => p.ImageID == imageId);
            if (image != null)
            {
                //image.IsSelected = true;
                ViewBag.Base64String = "data:image/png;base64," + Convert.ToBase64String(image.Data, 0, image.Data.Length);
            }
            return View(images);
        }

        private List<Image> GetImages()
        {
            string query = "SELECT * FROM tblFiles";
            List<Image> images = new List<Image>();
            string constr = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            images.Add(new Image
                            {
                                ImageID = Convert.ToInt32(sdr["Id"]),
                                Name = sdr["Name"].ToString(),
                                //ContentType = sdr["ContentType"].ToString(),
                                Data = (byte[])sdr["Data"]
                            });
                        }
                    }
                    con.Close();
                }

                return images;
            }
        }
    }
}