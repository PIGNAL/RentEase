using RentEase.Application.Contracts.Persistence;
using RentEase.Domain.Common;
using RentEase.Infrastructure.Persistence;
using System.Collections;

namespace RentEase.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork

    {
        private Hashtable _repositories;
        private readonly RentEaseDbContext _context;
        private ICarRepository _carRepository;

        public ICarRepository CarRepository => _carRepository ??= new CarRepository(_context);

        public UnitOfWork(RentEaseDbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IRepositoryBase<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(RepositoryBase<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
                _repositories.Add(type, repositoryInstance);
            }

            return (IRepositoryBase<TEntity>)_repositories[type];
        }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public RentEaseDbContext RentEaseDbContext => _context;
    }
}
