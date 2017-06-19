using CreateCodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CreateCodeFirst.Web.Controllers
{
    public class BooksController : Controller
    {
        
        private static HttpClient apiClient = new HttpClient();
        private DataContext dbContext = new DataContext();

        static async Task RunAsync()
        {
            apiClient = new HttpClient();

            apiClient.BaseAddress = new Uri("http://localhost:64073/");
            apiClient.DefaultRequestHeaders.Accept.Clear();            
            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: api/Books
        static async Task GetBooks()
        {
            HttpResponseMessage response = await apiClient.GetAsync("api/produtos/1");
            if (response.IsSuccessStatusCode)
            {
                Book book = await response.Content.ReadAsAsync<Book>();
            }            
        }

        // GET / api/Books
        public ActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        static async Task<Uri> CreateProductAsync(Book book)
        {
            HttpResponseMessage response = await apiClient.PostAsJsonAsync("api/Book", book);
            response.EnsureSuccessStatusCode();

            // Return the URI of the created resource.
            return response.Headers.Location;
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