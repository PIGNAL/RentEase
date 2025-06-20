using RentEase.Domain.Common;

namespace RentEase.Application.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        ICarRepository CarRepository { get; }
        IRepositoryBase<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel;
        Task<int> Complete();
    }
}
