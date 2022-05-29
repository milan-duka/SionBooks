using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SionBooks.Areas.Admin.Models;
using SionBooks.Data;
using SionBooks.Models;

namespace SionBooks.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookCategoriesController : Controller
    {
        private readonly SionBooksContext _context;

        public BookCategoriesController(SionBooksContext context)
        {
            _context = context;
        }

        [Authorize(AuthenticationSchemes = "Cookies", Roles = "Admin")]
        // GET: Admin/BookCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.Category.ToListAsync());
        }

        [Authorize(AuthenticationSchemes = "Cookies", Roles = "Admin")]
        // GET: Admin/BookCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookCategoryModel = await _context.Category
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookCategoryModel == null)
            {
                return NotFound();
            }

            return View(bookCategoryModel);
        }

        [Authorize(AuthenticationSchemes = "Cookies", Roles = "Admin")]
        // GET: Admin/BookCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(AuthenticationSchemes = "Cookies", Roles = "Admin")]
        // POST: Admin/BookCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] BookCategory bookCategoryModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookCategoryModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bookCategoryModel);
        }

        [Authorize(AuthenticationSchemes = "Cookies", Roles = "Admin")]
        // GET: Admin/BookCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookCategoryModel = await _context.Category.FindAsync(id);
            if (bookCategoryModel == null)
            {
                return NotFound();
            }
            return View(bookCategoryModel);
        }

        [Authorize(AuthenticationSchemes = "Cookies", Roles = "Admin")]
        // POST: Admin/BookCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] BookCategory bookCategoryModel)
        {
            if (id != bookCategoryModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookCategoryModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookCategoryModelExists(bookCategoryModel.Id))
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
            return View(bookCategoryModel);
        }

        [Authorize(AuthenticationSchemes = "Cookies", Roles = "Admin")]
        // GET: Admin/BookCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookCategoryModel = await _context.Category
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookCategoryModel == null)
            {
                return NotFound();
            }

            return View(bookCategoryModel);
        }

        [Authorize(AuthenticationSchemes = "Cookies", Roles = "Admin")]
        // POST: Admin/BookCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookCategoryModel = await _context.Category.FindAsync(id);
            _context.Category.Remove(bookCategoryModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookCategoryModelExists(int id)
        {
            return _context.Category.Any(e => e.Id == id);
        }
    }
}
