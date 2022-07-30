using Domain.Repositories;

namespace Persistence.Repositories
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext? _dbContext;

        public UnitOfWork(ApplicationDbContext? dbContext)
        {
            _dbContext = dbContext;
            UserRepository = new UserRepository(_dbContext);
        }

        public IUserRepository UserRepository { get; private set; }

        public Task<int> SaveChangeAsync(CancellationToken cancellationToken = default)
        {
            if(_dbContext == null)
            {
                throw new NullReferenceException(nameof(_dbContext));
            }

            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
