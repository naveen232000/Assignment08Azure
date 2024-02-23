using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment08Azure.Models;

namespace Assignment08Azure.Controllers
{
    public class BooksController : Controller
    {
        private readonly booksdbContext _context;

        public BooksController(booksdbContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var booksdbContext = _context.Books.Include(b => b.AuthorNavigation).Include(b => b.CategoryNavigation).Include(b => b.PublisherNavigation);
            return View(await booksdbContext.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.AuthorNavigation)
                .Include(b => b.CategoryNavigation)
                .Include(b => b.PublisherNavigation)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["Author"] = new SelectList(_context.Authors, "AuthorId", "AuthorId");
            ViewData["Category"] = new SelectList(_context.BookCategories, "CategoryId", "CategoryId");
            ViewData["Publisher"] = new SelectList(_context.Publishers, "PublisherId", "PublisherId");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookId,Author,Publisher,Category,Price,BookName")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Author"] = new SelectList(_context.Authors, "AuthorId", "AuthorId", book.Author);
            ViewData["Category"] = new SelectList(_context.BookCategories, "CategoryId", "CategoryId", book.Category);
            ViewData["Publisher"] = new SelectList(_context.Publishers, "PublisherId", "PublisherId", book.Publisher);
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["Author"] = new SelectList(_context.Authors, "AuthorId", "AuthorId", book.Author);
            ViewData["Category"] = new SelectList(_context.BookCategories, "CategoryId", "CategoryId", book.Category);
            ViewData["Publisher"] = new SelectList(_context.Publishers, "PublisherId", "PublisherId", book.Publisher);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookId,Author,Publisher,Category,Price,BookName")] Book book)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.BookId))
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
            ViewData["Author"] = new SelectList(_context.Authors, "AuthorId", "AuthorId", book.Author);
            ViewData["Category"] = new SelectList(_context.BookCategories, "CategoryId", "CategoryId", book.Category);
            ViewData["Publisher"] = new SelectList(_context.Publishers, "PublisherId", "PublisherId", book.Publisher);
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.AuthorNavigation)
                .Include(b => b.CategoryNavigation)
                .Include(b => b.PublisherNavigation)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Books == null)
            {
                return Problem("Entity set 'booksdbContext.Books'  is null.");
            }
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
          return (_context.Books?.Any(e => e.BookId == id)).GetValueOrDefault();
        }
    }
}
