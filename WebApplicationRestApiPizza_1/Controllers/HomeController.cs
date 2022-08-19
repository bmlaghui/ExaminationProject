using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebApplicationRestApiPizza_1.Models;
using System.Web.Security;

namespace WebApplicationRestApiPizza_1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        // POST: Home/Login
        [HttpPost]
        public ActionResult Login()

        {

            string[] keys = Request.Form.AllKeys;
            var client = new HttpClient();
            string hashedPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(Request.Form[keys[1]], "MD5").ToLower();
            var uri = "https://qcmapi.herokuapp.com/loginVerif/" + Request.Form[keys[0]] + "/" + hashedPwd;
            var resp = client.GetAsync(uri).Result;
            var ret = resp.Content.ReadAsStringAsync().Result.ToString();
            dynamic json = JsonConvert.DeserializeObject(ret);
            if (json.ToString()== "No record found.") { return RedirectToAction("Index", "Home"); }
            else {
                this.Session["id"] = json["_id"];
                this.Session["nom"] = json["nom"];
                this.Session["prenom"] = json["prenom"];
                return RedirectToAction("Index", "Exam");
            }


        }

    }
}