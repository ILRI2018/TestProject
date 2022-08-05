using Microsoft.EntityFrameworkCore;
using PointService.DataAccess.Models;

namespace PointService.DataAccess
{
    public class PointServiceContext : DbContext
    {
        public PointServiceContext(DbContextOptions<PointServiceContext> options) : base(options)
        {

        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
