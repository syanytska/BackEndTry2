using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEndTry2.Models;

namespace BackEndTry2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CatController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cat>>> GetCats()
        {
            return await _context.Cats.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cat>> GetCat(long id)
        {
            var cat = await _context.Cats.FindAsync(id);
            if (cat == null) return NotFound();
            return cat;
        }

        [HttpPost]
        public async Task<ActionResult<Cat>> CreateCat(Cat cat)
        {
            _context.Cats.Add(cat);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCat), new { id = cat.CatID }, cat);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCat(long id, Cat cat)
        {
            if (id != cat.CatID) return BadRequest();

            _context.Entry(cat).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCat(long id)
        {
            var cat = await _context.Cats.FindAsync(id);
            if (cat == null) return NotFound();

            _context.Cats.Remove(cat);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
