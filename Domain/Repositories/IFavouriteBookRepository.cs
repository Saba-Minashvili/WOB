using Domain.Entities;

namespace Domain.Repositories
{
    public interface IFavouriteBookRepository
    {
        Task<IEnumerable<FavouriteBook>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<FavouriteBook>> GetByUserIdAsync(string? userId, CancellationToken cancellationToken = default);
        Task<FavouriteBook?> GetByIdAsync(int favouriteBookId, CancellationToken cancellationToken = default);
        void AddToFavourites(FavouriteBook? favouriteBook);
        void DeleteFromFavourites(FavouriteBook? favouriteBook);
    }
}
