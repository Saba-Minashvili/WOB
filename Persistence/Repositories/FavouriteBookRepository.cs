using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    internal sealed class FavouriteBookRepository : IFavouriteBookRepository
    {
        private readonly ApplicationDbContext? _dbContext;

        public FavouriteBookRepository(ApplicationDbContext? dbContext) => _dbContext = dbContext;

        public async Task<List<FavouriteBook>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            if(_dbContext.Favourites == null)
            {
                throw new NullReferenceException(nameof(_dbContext.Favourites));
            }

            return await _dbContext.Favourites
                .Include(o => o.Book)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<List<FavouriteBook>> GetByUserIdAsync(string? userId, CancellationToken cancellationToken = default)
        {
            if (_dbContext.Favourites == null)
            {
                throw new NullReferenceException(nameof(_dbContext.Favourites));
            }

            IQueryable<FavouriteBook> favBooks = _dbContext.Favourites
                .AsNoTracking()
                .Where(o => o.UserId == userId);

            return await favBooks.ToListAsync(cancellationToken);
        }

        public async Task<FavouriteBook?> GetByIdAsync(int favouriteBookId, CancellationToken cancellationToken = default)
        {
            if (_dbContext.Favourites == null)
            {
                throw new NullReferenceException(nameof(_dbContext.Favourites));
            }

            return await _dbContext.Favourites
                .Include(o => o.Book)
                .AsNoTracking()
                .AsQueryable()
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
