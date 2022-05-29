using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SionBooks.Areas.Admin.Models;
using SionBooks.Areas.Admin.Models.ViewModels;
using SionBooks.Data;
using SionBooks.Models;

namespace SionBooks.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BooksController : Controller
    {
        private readonly SionBooksContext _context;
        private readonly IWebHostEnvironment _environment;
        public BooksController(SionBooksContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Admin/Books
        public async Task<IActionResult> Index()
        {
            var sionBooksContext = _context.Book.Include(b => b.Category);
            return View(await sionBooksContext.ToListAsync());
        }

        // GET: Admin/Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookModel = await _context.Book
                .Include(b => b.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookModel == null)
            {
                return NotFound();
            }

            return View(bookModel);
        }

        [Authorize(AuthenticationSchemes = "Cookies", Roles = "Admin")]
        // GET: Admin/Books/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
            return View();
        }

        [Authorize(AuthenticationSchemes = "Cookies", Roles = "Admin")]
        // POST: Admin/Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Isbn,Description,Image,CategoryId")] BookModels.CreateModel bookModel)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                var file = files.First();
                string fileName = DateTime.UtcNow.Ticks + "." + bookModel.Image.FileName.Split('.')[^1];
                string filePath = Path.Combine(_environment.WebRootPath, "images", fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                Book book = new()
                {
                    Title = bookModel.Title,
                    Isbn = bookModel.Isbn,
                    Description = bookModel.Description,
                    CategoryId = bookModel.CategoryId,
                    ImageUrl = fileName
                };
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", bookModel.CategoryId);
            return View(bookModel);
        }

        [Authorize(AuthenticationSchemes = "Cookies", Roles = "Admin")]
        // GET: Admin/Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            BookModels.EditModel bookModel = new()
            {
                Title = book.Title,
                Isbn = book.Isbn,
                Description = book.Description,
                ImageUrl = book.ImageUrl,
                CategoryId = book.CategoryId
            };

            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", book.CategoryId);
            return View(bookModel);
        }

        [Authorize(AuthenticationSchemes = "Cookies", Roles = "Admin")]
        // POST: Admin/Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Isbn,Description,ImageUrl,Image,CategoryId")] BookModels.EditModel bookModel)
        {
            if (id != bookModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string fileName = "";

                if (files != null && files.Count > 0)
                {
                    var file = files.First();
                    fileName = DateTime.UtcNow.Ticks + "." + bookModel.Image.FileName.Split('.')[^1];
                    string oldFilePath = Path.Combine(_environment.WebRootPath, "images", bookModel.ImageUrl);
                    string newFilePath = Path.Combine(_environment.WebRootPath, "images", fileName);

                    using (var fileStream = new FileStream(newFilePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                        System.IO.File.Delete(oldFilePath);
                    }
                }
                else
                {
                    fileName = bookModel.ImageUrl;
                }

                Book book = new()
                {
                    Id = bookModel.Id,
                    Title = bookModel.Title,
                    Isbn = bookModel.Isbn,
                    Description = bookModel.Description,
                    CategoryId = bookModel.CategoryId,
                    ImageUrl = fileName
                };

                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookModelExists(bookModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", bookModel.CategoryId);
            return View(bookModel);
        }

        [Authorize(AuthenticationSchemes = "Cookies", Roles = "Admin")]
        // GET: Admin/Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookModel = await _context.Book
                .Include(b => b.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookModel == null)
            {
                return NotFound();
            }

            return View(bookModel);
        }

        [Authorize(AuthenticationSchemes = "Cookies", Roles = "Admin")]
        // POST: Admin/Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookModel = await _context.Book.FindAsync(id);
            _context.Book.Remove(bookModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookModelExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }
    }
}
