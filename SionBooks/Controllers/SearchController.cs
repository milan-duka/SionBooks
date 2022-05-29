using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SionBooks.Areas.Admin.Models;
using SionBooks.Data;
using SionBooks.Models;
using SionBooks.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SionBooks.Controllers
{
    public class SearchController : Controller
    {
        private readonly ILogger<SearchController> _logger;
        private readonly SionBooksContext _context;

        public SearchController(ILogger<SearchController> logger, SionBooksContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult SearchBook()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
            return View();
        }

        public async Task<IActionResult> SearchResults([Bind("Title, CategoryId, Isbn")] SearchModels.SearchModel bookModel, int pageNumber = 1)
        {
            var users = _context.User.Where(u => u.email == User.Identity.Name);
            var votes = _context.Vote.Where(v => v.User == users.FirstOrDefault()).Include(v => v.Book);
            var books = from b in _context.Book.Include(b => b.Category)
                        orderby b.Votes.Count() descending
                        select new SearchModels.SearchResultModel
                        {
                            Id = b.Id,
                            Title = b.Title,
                            CategoryId = b.CategoryId,
                            Category = b.Category,
                            Isbn = b.Isbn,
                            Description = b.Description,
                            ImageUrl = b.ImageUrl,
                            Count = b.Votes.Count(),
                            Voted = votes.Where(v => v.Book.Id == b.Id).Any() ? "voted" : ""
                        };

            if (!string.IsNullOrEmpty(bookModel.Title))
            {
                if (bookModel.CategoryId == 0 && string.IsNullOrEmpty(bookModel.Isbn))
                    books = books.Where(j => j.Title.Contains(bookModel.Title));

                else if (bookModel.CategoryId != 0 && string.IsNullOrEmpty(bookModel.Isbn))
                    books = books.Where(j => j.Title.Contains(bookModel.Title) && j.CategoryId.Equals(bookModel.CategoryId));

                else if (bookModel.CategoryId == 0 && !string.IsNullOrEmpty(bookModel.Isbn))
                    books = books.Where(j => j.Title.Contains(bookModel.Title) && j.Isbn.Equals(bookModel.Isbn));

                else
                    books = books.Where(j => j.Title.Contains(bookModel.Title) && j.CategoryId.Equals(bookModel.CategoryId) && j.Isbn.Equals(bookModel.Isbn));
            }
            else
            {
                if (bookModel.CategoryId != 0 && string.IsNullOrEmpty(bookModel.Isbn))
                    books = books.Where(j => j.CategoryId.Equals(bookModel.CategoryId));

                else if (bookModel.CategoryId == 0 && !string.IsNullOrEmpty(bookModel.Isbn))
                    books = books.Where(j => j.Isbn.Equals(bookModel.Isbn));

                else if (bookModel.CategoryId != 0 && !string.IsNullOrEmpty(bookModel.Isbn))
                    books = books.Where(j => j.CategoryId.Equals(bookModel.CategoryId) && j.Isbn.Equals(bookModel.Isbn));
            }
            /* return View(await books.ToListAsync()); */
            return View(await PaginatedList<SearchModels.SearchResultModel>.CreateAsync(books.OrderByDescending(b => b.Count), pageNumber, 5));
        }

        [HttpPost]
        public IActionResult BookDetails(int Id, string Title, int CategoryId, string Isbn, string Description, string ImageUrl, int Count, string Voted)
        {
            var category = _context.Category.Find(CategoryId);
            var book = new SearchModels.SearchResultModel
            {
                Id = Id,
                Title = Title,
                Category = category,
                Isbn = Isbn,
                Description = Description,
                ImageUrl = ImageUrl,
                Count = Count,
                Voted = Voted
            };
            return View(book);
        }

        [HttpPost]
        public int Add(int number1, int number2)
        {
            return number1 + number2;
        }

        public PartialViewResult Comments(int bookId, string email)
        {
            var user = _context.User.Where(u => u.email == email).FirstOrDefault();
            var comments = _context.Comment.Include(c => c.book).Include(c => c.user).Where(c => c.book.Id == bookId && c.user == user);
            return PartialView(comments);
        }

        [HttpPost]
        public int Vote(int bookId, string email)
        {
            var book = _context.Book.Find(bookId);
            var user = _context.User.Where(u => u.email == email).FirstOrDefault();
            var exists = _context.Vote.Where(v => v.Book == book && v.User == user).Count();
            if (exists == 0)
            {
                Vote vote = new()
                {
                    Book = book,
                    User = user
                };
                _context.Add(vote);
                _context.SaveChanges();
                return 1;
            }
            else return 0;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
