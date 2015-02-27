using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TyepAheadWebAPIExample.Models;

namespace TyepAheadWebAPIExample.Controllers
{
    public class BooksController : ApiController
    {
        // GET api/values/GetAllBooks
        /// <summary>
        /// This method is going to be used for the prefetching of all data used for the Twitter typeahead
        /// </summary>
        /// <returns>A list of books that will be serialized to JSON and used by the Twitter typeahead</returns>
        public IEnumerable<Book> GetAllBooks()
        {
            List<Book> books = GetBooks();

            return books;
        }

        // GET api/values/SearchBooks?searchTerm={%QUERY}
        /// <summary>
        /// This method will be used to handle searching for books
        /// </summary>
        /// <param name="searchTerm">The term being searched for. In this case, either an author or a book title.</param>
        /// <returns>A list of books matching the searched term.</returns>
        [HttpGet]
        public IEnumerable<Book> SearchBooks(string searchTerm)
        {
            searchTerm = searchTerm.ToUpper();
            IEnumerable<Book> books = GetAllBooks().Where(x=>x.Title.ToUpper().Contains(searchTerm) || x.Author.ToUpper().Contains(searchTerm));

            return books;
        }
        /// <summary>
        /// This will cache and populate a list of book objects. If the book objects have not been cached then it will cache, otherwise it will retrieve the book objects from the existing application cache.
        /// </summary>
        /// <returns>A list of book object.</returns>
        private List<Book> GetBooks()
        {
            List<Book> books = new List<Book>();

            if (HttpContext.Current.Cache["BooksList"] != null)
            {
                books = (List<Book>)HttpContext.Current.Cache["BooksList"];
            }
            else
            {
                books.Add(new Book()
                {
                    Author = "Bret Easton Ellis",
                    Title = "American Psycho"
                });

                books.Add(new Book()
                {
                    Author = "Bret Easton Ellis",
                    Title = "Less Than Zero"
                });

                books.Add(new Book()
                {
                    Title = "The Winner",
                    Author = "David Baldacci"
                });

                books.Add(new Book()
                {
                    Title = "Private Vegas",
                    Author = "James Patterson"
                });
            }

            return books;
        }
    }
}
