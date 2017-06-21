using CreateCodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CreateCodeFirst.ConsoleApp
{
    class Program
    {
        static HttpClient apiClient = new HttpClient();

        static void Main(string[] args)
        {
            RunAsync().Wait();
        }

        static void ShowBook(Book book)
        {
            Console.WriteLine($"ID: {book.BookId}\tTitulo: {book.Title}\tIsbn: {book.Isbn}");
        }

        static async Task<Book> GetBook(string path)
        {
            Book book = null;
            HttpResponseMessage response = await apiClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                book = await response.Content.ReadAsAsync<Book>();
            }
            return book;
        }

        static async Task<Uri> CreateBook(Book book)
        {
            HttpResponseMessage response = await apiClient.PostAsJsonAsync("api/books", book);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        static async Task<Book> UpdateBook(Book book)
        {
            HttpResponseMessage response = await apiClient.PutAsJsonAsync($"api/books/{book.BookId}, {book.Title}, {book.Isbn}", book);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            book = await response.Content.ReadAsAsync<Book>();
            return book;
        }

        static async Task<HttpStatusCode> DeleteBook(int bookId)
        {
            HttpResponseMessage response = await apiClient.DeleteAsync($"api/books/{bookId}");
            return response.StatusCode;
        }


        static async Task RunAsync()
        {
            using (apiClient)
            {
                apiClient.BaseAddress = new Uri("http://localhost:64073/");
                apiClient.DefaultRequestHeaders.Accept.Clear();
                apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    // Create a new product
                    Book book = new Book { BookId = 10, Title = "Teste de console", Isbn = "123456789" };

                    var url = await CreateBook(book);
                    Console.WriteLine($"Created at {url}");

                    // Get the product
                    book = await GetBook(url.PathAndQuery);
                    ShowBook(book);

                    // Update the product
                    Console.WriteLine("Updating price...");
                    book.BookId = 80;
                    await UpdateBook(book);

                    // Get the updated product
                    book = await GetBook(url.PathAndQuery);
                    ShowBook(book);

                    // Delete the product
                    var statusCode = await DeleteBook(book.BookId);
                    Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                Console.ReadLine();
            }
        }
    }
}
