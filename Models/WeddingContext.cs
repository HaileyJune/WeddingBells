using Microsoft.EntityFrameworkCore;
using WeddingBells.Models;
using UpdatedLogReg.Models;

namespace WeddingBells.Models
{
    public class WeddingContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public WeddingContext(DbContextOptions<WeddingContext> options) : base(options) { }
        public DbSet<UserObject> Users { get; set; }

        public DbSet<Wedding> Weddings { get; set; }
        public DbSet<RSVP> RSVPs { get; set; }
    }
}