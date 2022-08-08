using Calefy.DataAccesLayer;
using Microsoft.EntityFrameworkCore;
using PointService.DataAccess.Models;

namespace PointService.DataAccess
{
    public class PointServiceContext : DbContext
    {
        public PointServiceContext(DbContextOptions<PointServiceContext> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ModelBuilderExtension.Seed(modelBuilder);
        }
    }
}
