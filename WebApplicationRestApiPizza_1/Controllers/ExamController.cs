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
using MongoDB.Bson;
using MongoDB.Driver;

namespace WebApplicationRestApiPizza_1.Controllers
{
    public class ExamController : Controller
    {

        // GET: Exam/Listing/matiere
        public ActionResult Listing2(String matiere)
        {
            /*
            var client = new HttpClient();
            var mat = matiere.Replace("_", ".");
           var url = "https://qcmapi.herokuapp.com/questionsExam/"+mat+"/20";
            //var url = "https://qcmapi.herokuapp.com/questionsExam/D51.1/20";
            var response = client.GetAsync(url).Result;

            var retour = response.Content.ReadAsStringAsync().Result;


            JObject json = JObject.Parse(retour);
            var dataQuestions = json["questions"];



            ViewBag.dataQuestions = dataQuestions;
            */
            ViewBag.matiere = matiere;
            return View();

        }
        // GET: Exam
        public ActionResult Index()
        {
            var id = HttpContext.Session["id"];

            var client = new HttpClient();
            var url = "https://qcmapi.herokuapp.com/listExams/" + id;
            var response = client.GetAsync(url).Result;

            var retour = response.Content.ReadAsStringAsync().Result;


            JObject json = JObject.Parse(retour);
            var dataEpreuves = json["epreuves"];



            ViewBag.epreuves = dataEpreuves;
            
            return View();

        }


        
        // GET: Client/Details/5
        public ActionResult Listing(string id)
        {
            var client = new HttpClient();
            var mat = id.Replace("_", ".");
            var url = "https://qcmapi.herokuapp.com/questionsExam/" + mat + "/20";
            //var url = "https://qcmapi.herokuapp.com/questionsExam/D51.1/20";
            var response = client.GetAsync(url).Result;

            var retour = response.Content.ReadAsStringAsync().Result;


            JObject json = JObject.Parse(retour);
            var dataQuestions = json["questions"];



            ViewBag.dataQuestions = dataQuestions;
            
            return View();
        }

        // GET: Client/Create
        public ActionResult Create()
        {
            return View();
        }
        
        // POST: Exam/Corriger
        [HttpPost]
        public ActionResult Corriger()

        {
            var note = 0;
            Dictionary<string, string> responseDict = new Dictionary<string, string>();

            string[] keys = Request.Form.AllKeys;
            var client = new HttpClient();
            for (int i = 0; i < keys.Length; i++)
            {
               
                var url = "https://qcmapi.herokuapp.com/correction/D51.1/" + keys[i];
                var response = client.GetAsync(url).Result;

                var retour = response.Content.ReadAsStringAsync().Result;
                JObject json = JObject.Parse(retour);
                var correctOption = json["correctOption"];
                var responseUser = Request.Form[keys[i]];
                if (correctOption.ToString() == Request.Form[keys[i]]) { note++; }
                responseDict.Add(keys[i], Request.Form[keys[i]]);

               // Response.Write(keys[i] + ": " + Request.Form[keys[i]] + "<br>");
            }
            var cl = new MongoClient("mongodb://localhost:27017");
            var database = cl.GetDatabase("QMQUIZ");
            //var collection = database.GetCollection<Leader>("leaders");
            var collection = database.GetCollection<BsonDocument>("user");
            var filter = new BsonDocument("_id", Session["id"].ToString());
            var update = Builders<BsonDocument>.Update.Set("notes", "["+note.ToString()+"]");

            var result = collection.FindOneAndUpdate(filter, update);

            
            ViewBag.note = note.ToString();
            //Session.Abandon();
            return View("Result");
        }

        // POST: Client/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Client/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Client/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Client/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Client/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
