using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TreeApplication.DAL.Entities;

namespace TreeApplication.DAL
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions<AppContext> options) : base(options)
        {
        }

        public DbSet<Tree> Trees { get; set; }
        public DbSet<Node> Nodes { get; set; }
        public DbSet<ExceptionJournal> ExceptionJournals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
