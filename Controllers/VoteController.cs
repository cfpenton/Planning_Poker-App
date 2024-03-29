using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Planning_Poker.Data;
using Planning_Poker.Hubs;
using Planning_Poker.Models;

namespace Planning_Poker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : ControllerBase
    {
        private readonly Planning_PokerContext _context;
        private readonly IHubContext<VotesHub> _signalrHub;

        public VoteController(Planning_PokerContext context, IHubContext<VotesHub> signalrHub)
        {
            _context = context;
            _signalrHub = signalrHub;
        }

        // GET: api/Vote
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vote>>> GetVotes()
        {
            return await _context.Votes.ToListAsync();
        }

        // GET: api/Vote/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vote>> GetVote(int id)
        {
            var vote = await _context.Votes.FindAsync(id);

            if (vote == null)
            {
                return NotFound();
            }

            return vote;
        }

        // PUT: api/Vote/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVote(int id, Vote vote)
        {
            if (id != vote.id)
            {
                return BadRequest();
            }

            _context.Entry(vote).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                await _signalrHub.Clients.All.SendAsync("LoadVotes");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoteExists(id))
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

        // POST: api/Vote
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Vote>> PostVote(Vote vote)
        {
            _context.Votes.Add(vote);
            await _context.SaveChangesAsync();
            await _signalrHub.Clients.All.SendAsync("LoadVotes");

            return CreatedAtAction("GetVote", new { id = vote.id }, vote);
        }

        // DELETE: api/Vote/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Vote>> DeleteVote(int id)
        {
            var vote = await _context.Votes.FindAsync(id);
            if (vote == null)
            {
                return NotFound();
            }

            _context.Votes.Remove(vote);
            await _context.SaveChangesAsync();
            await _signalrHub.Clients.All.SendAsync("LoadVotes");

            return vote;
        }

        private bool VoteExists(int id)
        {
            return _context.Votes.Any(e => e.id == id);
        }
    }
}
