using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Planning_Poker.Data;
using Planning_Poker.Models;

namespace Planning_Poker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LetterController : ControllerBase
    {
        private readonly Planning_PokerContext _context;

        public LetterController(Planning_PokerContext context)
        {
            _context = context;
        }

        // GET: api/Letter
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Letter>>> GetLetters()
        {
            return await _context.Letters.ToListAsync();
        }

        // GET: api/Letter/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Letter>> GetLetter(int id)
        {
            var letter = await _context.Letters.FindAsync(id);

            if (letter == null)
            {
                return NotFound();
            }

            return letter;
        }

        // PUT: api/Letter/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLetter(int id, Letter letter)
        {
            if (id != letter.id)
            {
                return BadRequest();
            }

            _context.Entry(letter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LetterExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Letter
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Letter>> PostLetter(Letter letter)
        {
            _context.Letters.Add(letter);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLetter", new { id = letter.id }, letter);
        }

        // DELETE: api/Letter/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Letter>> DeleteLetter(int id)
        {
            var letter = await _context.Letters.FindAsync(id);
            if (letter == null)
            {
                return NotFound();
            }

            _context.Letters.Remove(letter);
            await _context.SaveChangesAsync();

            return letter;
        }

        private bool LetterExists(int id)
        {
            return _context.Letters.Any(e => e.id == id);
        }
    }
}
