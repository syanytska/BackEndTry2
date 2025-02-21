using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEndTry2.Models;

namespace BackEndTry2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DogController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DogController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dog>>> GetDogs()
        {
            return await _context.Dogs.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Dog>> GetDog(long id)
        {
            var dog = await _context.Dogs.FindAsync(id);
            if (dog == null) return NotFound();
            return dog;
        }

        [HttpPost]
        public async Task<ActionResult<Dog>> CreateDog(Dog dog)
        {
            _context.Dogs.Add(dog);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDog), new { id = dog.DogID }, dog);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDog(long id, Dog dog)
        {
            if (id != dog.DogID) return BadRequest();

            _context.Entry(dog).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDog(long id)
        {
            var dog = await _context.Dogs.FindAsync(id);
            if (dog == null) return NotFound();

            _context.Dogs.Remove(dog);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
