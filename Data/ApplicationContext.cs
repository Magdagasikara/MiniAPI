using Microsoft.EntityFrameworkCore;
using MiniAPI.Models;

namespace MiniAPI.Data
{
    public class ApplicationContext : DbContext
    {

        public DbSet<Person> Persons { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<InterestLink> InterestLinks { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

    }
}
