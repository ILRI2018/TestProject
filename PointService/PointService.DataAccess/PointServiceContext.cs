using Microsoft.EntityFrameworkCore;
using PointService.DataAccess.Models;

namespace PointService.DataAccess
{
    public class PoinServiceContext : DbContext
    {
        public PoinServiceContext(DbContextOptions<PoinServiceContext> options) : base(options)
        {

        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
