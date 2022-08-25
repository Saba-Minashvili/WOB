using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    internal sealed class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext? _dbContext;

        public UserRepository(ApplicationDbContext? dbContext) => _dbContext = dbContext;

        public async Task<List<User>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            if(_dbContext == null)
            {
                throw new NullReferenceException(nameof(_dbContext));
            }

            return await _dbContext.Users
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<User?> GetByIdAsync(string? userId, CancellationToken cancellationToken = default)
        {
            if (_dbContext == null)
            {
                throw new NullReferenceException(nameof(_dbContext));
            }

            return await _dbContext.Users
                .AsQueryable()
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

        public void Update(User? user, JsonPatchDocument? userPatch)
        {
            if (_dbContext == null)
            {
                throw new NullReferenceException(nameof(_dbContext));
            }

            if(user == null)
            {
                throw new NullReferenceException(nameof(user));
            }

            if(userPatch == null)
            {
                throw new ArgumentNullException(nameof(userPatch));
            }

            userPatch.ApplyTo(user);
        }
    }
}
