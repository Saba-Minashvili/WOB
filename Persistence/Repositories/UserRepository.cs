using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    internal sealed class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext? _dbContext;

        public UserRepository(ApplicationDbContext? dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            if(_dbContext == null)
            {
                throw new NullReferenceException(nameof(_dbContext));
            }

            return await _dbContext.Users
                .Include(o => o.FavouriteBooks)
                .ToListAsync(cancellationToken);
        }

        public async Task<User?> GetByIdAsync(string? userId, CancellationToken cancellationToken = default)
        {
            if (_dbContext == null)
            {
                throw new NullReferenceException(nameof(_dbContext));
            }

            return await _dbContext.Users
                .Include(o => o.FavouriteBooks)
                .FirstOrDefaultAsync(o => o.Id == userId, cancellationToken);
        }

        public void Create(User user)
        {
            if (_dbContext == null)
            {
                throw new NullReferenceException(nameof(_dbContext));
            }

            _dbContext.Users.Add(user);
        }

        public void Update(string? userId, User user)
        {
            if (_dbContext == null)
            {
                throw new NullReferenceException(nameof(_dbContext));
            }

            user.Id = userId;
            _dbContext.Users.Update(user);
        }
    }
}
