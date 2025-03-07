using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class PersonController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Person.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonId,FullName,Address")] Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id) || _context.Person == null)
            {
                return NotFound();
            }

            if (!int.TryParse(id, out int personId)) // Chuyển từ string sang int
            {
                return BadRequest("Invalid Person ID");
            }

            var person = await _context.Person.FirstOrDefaultAsync(p => p.PersonId == personId);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PersonId,FullName,Address")] Person person)
        {
            if (!int.TryParse(id, out int personId)) // Chuyển từ string sang int
            {
                return BadRequest("Invalid Person ID");
            }

            if (personId != person.PersonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(personId))
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
            return View(person);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id) || _context.Person == null)
            {
                return NotFound();
            }

            if (!int.TryParse(id, out int personId)) // Chuyển từ string sang int
            {
                return BadRequest("Invalid Person ID");
            }

            var person = await _context.Person.FirstOrDefaultAsync(m => m.PersonId == personId);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Person == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Person' is null.");
            }

            if (!int.TryParse(id, out int personId)) // Chuyển từ string sang int
            {
                return BadRequest("Invalid Person ID");
            }

            var person = await _context.Person.FindAsync(personId);
            if (person != null)
            {
                _context.Person.Remove(person);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id) // Chuyển kiểu dữ liệu về int
        {
            return _context.Person?.Any(e => e.PersonId == id) ?? false;
        }
    }
}
