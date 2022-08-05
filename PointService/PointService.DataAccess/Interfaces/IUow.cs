using PointService.DataAccess.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PointService.DataAccess.Interfaces
{
    public interface IUow : IDisposable
    {
        public IGenericRepository<Client> ClientEntity { get; }
        public IGenericRepository<Transaction> TransactionEntity { get; }
        public Task SaveAsync(CancellationToken cancellationToken = default);
    }
}
