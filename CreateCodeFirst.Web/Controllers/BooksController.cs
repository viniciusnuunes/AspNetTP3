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
        public BooksController()
        {
            var apiClient = new HttpClient();
            
            var mediaType = new MediaTypeWithQualityHeaderValue("application/json");

            apiClient.BaseAddress = new Uri("http://localhost:64073/");
            apiClient.DefaultRequestHeaders.Accept.Add(mediaType);

            var message = apiClient.GetAsync("api/Books").Result;

            if (message.IsSuccessStatusCode)
            {
                Console.WriteLine(message.Content.ReadAsStringAsync().Result);
                Console.ReadKey();
            }            
        }


        // GET: Books
        public ActionResult Index()
        {
            return View();
        }
    }
}