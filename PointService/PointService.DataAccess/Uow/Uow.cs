using PointService.DataAccess.Interfaces;
using PointService.DataAccess.Models;
using PointService.DataAccess.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace PointService.DataAccess.Uow
{
    public class Uow : IUow
    {
        private readonly PointServiceContext _context;

        private IGenericRepository<Client> _clientEntity;
        private IGenericRepository<Transaction> _transactionEntity;

        private bool _disposed = false;

        public Uow(PointServiceContext context)
        {
            _context = context;
        }

        public IGenericRepository<Client> ClientEntity => _clientEntity ??= new GenericRepository<Client>(_context);
        public IGenericRepository<Transaction> TransactionEntity => _transactionEntity ??= new GenericRepository<Transaction>(_context);

        public Task SaveAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
                _context?.Dispose();

            _disposed = true;
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            System.GC.SuppressFinalize(this);
        }
    }
}
