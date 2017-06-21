using CreateCodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateCodeFirst.ConsoleApp
{
    class Book
    {
        public int BookId { get; set; }
        public string Isbn { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Author> Authors { get; set; }     
    }
}
