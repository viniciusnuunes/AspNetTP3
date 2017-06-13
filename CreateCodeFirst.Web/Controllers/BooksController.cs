using CreateCodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace CreateCodeFirst.Web.Controllers
{
    public class BooksController : Controller
    {
        private HttpClient apiClient;
        private DataContext dbContext = new DataContext();

        public BooksController()
        {
            apiClient = new HttpClient();

            apiClient.BaseAddress = new Uri("http://localhost:64073/");
            apiClient.DefaultRequestHeaders.Accept.Clear();

            var mediaType = new MediaTypeWithQualityHeaderValue("application/json");
            apiClient.DefaultRequestHeaders.Accept.Add(mediaType);
        }

        // GET: api/Books
        public IEnumerable<Book> Index()
        {            
            return dbContext.Books;
        }

        // GET / api/Books
        public ActionResult Create()
        {
            return View();
        }

        // POST: api/Books
        [HttpPost]
        public ActionResult Post()
        {
            var message = apiClient.GetAsync("api/Books").Result;
            if (message.IsSuccessStatusCode)
            {
                var asd = message.Content.ReadAsStringAsync().Result;
                new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Book>(asd);
            }
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && apiClient != null)
            {
                apiClient.Dispose();
                apiClient = null;
            }
            base.Dispose(disposing);
        }
    }
}