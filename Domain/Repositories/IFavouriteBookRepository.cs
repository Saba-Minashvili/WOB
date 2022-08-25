using Domain.Entities;

namespace Domain.Repositories
{
    public interface IFavouriteBookRepository
    {
        Task<List<FavouriteBook>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<List<FavouriteBook>> GetByUserIdAsync(string? userId, CancellationToken cancellationToken = default);
        Task<FavouriteBook?> GetByIdAsync(int favouriteBookId, CancellationToken cancellationToken = default);
        void AddToFavourites(FavouriteBook? favouriteBook);
        void DeleteFromFavourites(FavouriteBook? favouriteBook);
    }
}
