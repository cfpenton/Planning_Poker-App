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
    public class UserStoryController : ControllerBase
    {
        private readonly Planning_PokerContext _context;

        public UserStoryController(Planning_PokerContext context)
        {
            _context = context;
        }

        // GET: api/UserStory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserStory>>> GetUserStories()
        {
            return await _context.UserStories.ToListAsync();
        }

        // GET: api/UserStory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserStory>> GetUserStory(int id)
        {
            var userStory = await _context.UserStories.FindAsync(id);

            if (userStory == null)
            {
                return NotFound();
            }

            return userStory;
        }

        // PUT: api/UserStory/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserStory(int id, UserStory userStory)
        {
            if (id != userStory.id)
            {
                return BadRequest();
            }

            _context.Entry(userStory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserStoryExists(id))
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

        // POST: api/UserStory
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<UserStory>> PostUserStory(UserStory userStory)
        {
            _context.UserStories.Add(userStory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserStory", new { id = userStory.id }, userStory);
        }

        // DELETE: api/UserStory/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserStory>> DeleteUserStory(int id)
        {
            var userStory = await _context.UserStories.FindAsync(id);
            if (userStory == null)
            {
                return NotFound();
            }

            _context.UserStories.Remove(userStory);
            await _context.SaveChangesAsync();

            return userStory;
        }

        private bool UserStoryExists(int id)
        {
            return _context.UserStories.Any(e => e.id == id);
        }
    }
}
