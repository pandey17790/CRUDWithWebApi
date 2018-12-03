using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUDOperationWithWebApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult GetData()
        {
            System.Data.SqlClient.SqlConnection con;

            con = new System.Data.SqlClient.SqlConnection();
            con.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\db\MyDatabase.mdf;Integrated Security=True;Connect Timeout=30";
            con.Open();

            con.Close();

            return Json(new { name = "Amit" },JsonRequestBehavior.AllowGet);
        }
    }
}
