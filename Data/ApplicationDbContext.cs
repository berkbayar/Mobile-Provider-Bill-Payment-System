using Microsoft.EntityFrameworkCore;
using MobileProviderAPI.Models;

namespace MobileProviderAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<Bill> Bills { get; set; }
    }
}
