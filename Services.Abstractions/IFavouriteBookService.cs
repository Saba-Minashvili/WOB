using Contracts.Book;

namespace Services.Abstractions
{
    public interface IFavouriteBookService
    {
        Task<List<FavouriteBookDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<List<FavouriteBookDto>> GetByUserIdAsync(string? userId, CancellationToken cancellationToken = default);
        Task<FavouriteBookDto?> GetByIdAsync(int favouriteBookId, CancellationToken cancellationToken = default);
        Task<bool> AddToFavouritesAsync(AddToFavouritesDto? favouriteBookDto, CancellationToken cancellationToken = default);
        Task<bool> DeleteFromFavouritesAsync(int favouriteBookId, CancellationToken cancellationToken = default);
    }
}
