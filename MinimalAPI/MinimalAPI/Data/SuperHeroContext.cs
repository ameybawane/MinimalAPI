using Microsoft.EntityFrameworkCore;
using MinimalAPI.Model;

namespace MinimalAPI.Data
{
    public class SuperHeroContext : DbContext
    {
        public SuperHeroContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<SuperHero> SuperHeros { get; set; }
    }
}
