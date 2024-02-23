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
    public class BookAuthorsController : Controller
    {
        private readonly booksdbContext _context;

        public BookAuthorsController(booksdbContext context)
        {
            _context = context;
        }

        // GET: BookAuthors
        public async Task<IActionResult> Index()
        {
            var booksdbContext = _context.BookAuthors.Include(b => b.AuthorNavigation).Include(b => b.Book);
            return View(await booksdbContext.ToListAsync());
        }

        // GET: BookAuthors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BookAuthors == null)
            {
                return NotFound();
            }

            var bookAuthor = await _context.BookAuthors
                .Include(b => b.AuthorNavigation)
                .Include(b => b.Book)
                .FirstOrDefaultAsync(m => m.BookAuthorId == id);
            if (bookAuthor == null)
            {
                return NotFound();
            }

            return View(bookAuthor);
        }

        // GET: BookAuthors/Create
        public IActionResult Create()
        {
            ViewData["Author"] = new SelectList(_context.Authors, "AuthorId", "AuthorId");
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookId");
            return View();
        }

        // POST: BookAuthors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookAuthorId,BookId,Author")] BookAuthor bookAuthor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookAuthor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Author"] = new SelectList(_context.Authors, "AuthorId", "AuthorId", bookAuthor.Author);
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookId", bookAuthor.BookId);
            return View(bookAuthor);
        }

        // GET: BookAuthors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BookAuthors == null)
            {
                return NotFound();
            }

            var bookAuthor = await _context.BookAuthors.FindAsync(id);
            if (bookAuthor == null)
            {
                return NotFound();
            }
            ViewData["Author"] = new SelectList(_context.Authors, "AuthorId", "AuthorId", bookAuthor.Author);
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookId", bookAuthor.BookId);
            return View(bookAuthor);
        }

        // POST: BookAuthors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookAuthorId,BookId,Author")] BookAuthor bookAuthor)
        {
            if (id != bookAuthor.BookAuthorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookAuthor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookAuthorExists(bookAuthor.BookAuthorId))
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
            ViewData["Author"] = new SelectList(_context.Authors, "AuthorId", "AuthorId", bookAuthor.Author);
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookId", bookAuthor.BookId);
            return View(bookAuthor);
        }

        // GET: BookAuthors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BookAuthors == null)
            {
                return NotFound();
            }

            var bookAuthor = await _context.BookAuthors
                .Include(b => b.AuthorNavigation)
                .Include(b => b.Book)
                .FirstOrDefaultAsync(m => m.BookAuthorId == id);
            if (bookAuthor == null)
            {
                return NotFound();
            }

            return View(bookAuthor);
        }

        // POST: BookAuthors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BookAuthors == null)
            {
                return Problem("Entity set 'booksdbContext.BookAuthors'  is null.");
            }
            var bookAuthor = await _context.BookAuthors.FindAsync(id);
            if (bookAuthor != null)
            {
                _context.BookAuthors.Remove(bookAuthor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookAuthorExists(int id)
        {
          return (_context.BookAuthors?.Any(e => e.BookAuthorId == id)).GetValueOrDefault();
        }
    }
}
