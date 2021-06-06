using Microsoft.EntityFrameworkCore;
using Planning_Poker.Models;

namespace Planning_Poker.Data
{
public class Planning_PokerContext : DbContext
{
    public Planning_PokerContext(DbContextOptions<Planning_PokerContext> options) :
        base(options)
    {
    }

    public DbSet<Vote> Votes { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<UserStory> UserStories { get; set; }

    public DbSet<Letter> Letters { get; set; }
}
}