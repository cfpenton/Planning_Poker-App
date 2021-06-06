using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Planning_Poker.Data;
using Planning_Poker.Hubs;
using Planning_Poker.Models;

namespace Planning_Poker.Controllers
{
    public class AllVotesController : Controller
    {
        private readonly Planning_PokerContext _context;
        private readonly IHubContext<VotesHub> _signalrHub;


        public AllVotesController(Planning_PokerContext context, IHubContext<VotesHub> signalrHub)
        {
            _context = context;
            _signalrHub = signalrHub;
        }

        // GET: AllVotes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Votes.ToListAsync());
        }

        // GET: AllVotes/GetVotes
        public IActionResult GetVotes()
        { 
            var v = _context.Votes.ToList();
            return Ok(v);
        }

        // GET: AllVotes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vote = await _context.Votes
                .FirstOrDefaultAsync(m => m.id == id);
            if (vote == null)
            {
                return NotFound();
            }

            return View(vote);
        }

        // GET: AllVotes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AllVotes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,user_id,card_id,story_id")] Vote vote)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vote);
                await _context.SaveChangesAsync();
                await _signalrHub.Clients.All.SendAsync("LoadVotes");
                return RedirectToAction(nameof(Index));
            }
            return View(vote);
        }

        // GET: AllVotes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vote = await _context.Votes.FindAsync(id);
            if (vote == null)
            {
                return NotFound();
            }
            return View(vote);
        }

        // POST: AllVotes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,user_id,card_id,story_id")] Vote vote)
        {
            if (id != vote.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vote);
                    await _context.SaveChangesAsync();
                    await _signalrHub.Clients.All.SendAsync("LoadVotes");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VoteExists(vote.id))
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
            return View(vote);
        }

        // GET: AllVotes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vote = await _context.Votes
                .FirstOrDefaultAsync(m => m.id == id);
            if (vote == null)
            {
                return NotFound();
            }

            return View(vote);
        }

        // POST: AllVotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vote = await _context.Votes.FindAsync(id);
            _context.Votes.Remove(vote);
            await _context.SaveChangesAsync();
            await _signalrHub.Clients.All.SendAsync("LoadVotes");            
            return RedirectToAction(nameof(Index));
        }

        private bool VoteExists(int id)
        {
            return _context.Votes.Any(e => e.id == id);
        }
    }
}
