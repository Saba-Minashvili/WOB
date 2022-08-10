using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    internal sealed class FavouriteBookRepository : IFavouriteBookRepository
    {
        private readonly ApplicationDbContext? _dbContext;

        public FavouriteBookRepository(ApplicationDbContext? dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<FavouriteBook>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            if(_dbContext.Favourites == null)
            {
                throw new NullReferenceException(nameof(_dbContext.Favourites));
            }

            return await _dbContext.Favourites
                .Include(o => o.Book.Authors)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<FavouriteBook>> GetByUserIdAsync(string? userId, CancellationToken cancellationToken = default)
        {
            if (_dbContext.Favourites == null)
            {
                throw new NullReferenceException(nameof(_dbContext.Favourites));
            }

            return await _dbContext.Favourites
                .Include(o => o.Book.Authors)
                .Where(o => o.UserId == userId)
                .ToListAsync(cancellationToken);
        }

        public async Task<FavouriteBook?> GetByIdAsync(int favouriteBookId, CancellationToken cancellationToken = default)
        {
            if (_dbContext.Favourites == null)
            {
                throw new NullReferenceException(nameof(_dbContext.Favourites));
            }

            return await _dbContext.Favourites
                .Include(o => o.Book.Authors)
                .FirstOrDefaultAsync(o => o.Id == favouriteBookId, cancellationToken);
        }

        public void AddToFavourites(FavouriteBook? favouriteBook)
        {
            if (_dbContext.Favourites == null)
            {
                throw new NullReferenceException(nameof(_dbContext.Favourites));
            }

            if(favouriteBook == null)
            {
                throw new ArgumentNullException(nameof(favouriteBook));
            }

            _dbContext.Favourites.Add(favouriteBook);
        }

        public void DeleteFromFavourites(FavouriteBook? favouriteBook)
        {
            if (_dbContext.Favourites == null)
            {
                throw new NullReferenceException(nameof(_dbContext.Favourites));
            }

            if (favouriteBook == null)
            {
                throw new ArgumentNullException(nameof(favouriteBook));
            }

            _dbContext.Favourites.Remove(favouriteBook);
        }
    }
}
