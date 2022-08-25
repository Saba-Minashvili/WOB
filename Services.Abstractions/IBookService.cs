using Contracts.Book;

namespace Services.Abstractions
{
    public interface IBookService
    {
        Task<List<BookDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<List<BookDto>> GetByNameAsync(string? bookName, CancellationToken cancellationToken = default);
        Task<List<BookDto>> GetByAuthorAsync(string? author, CancellationToken cancellationToken = default);
        Task<List<BookDto>> GetByGenreAsync(string? genre, CancellationToken cancellationToken = default);
        Task<List<BookDto>> GetByPageNumberAsync(int pageNumber, CancellationToken cancellationToken = default);
        Task<List<BookDto>> GetByReleaseDateAsync(string? releaseDate, CancellationToken cancellationToken = default);
        Task<BookDto?> GetByIdAsync(int bookId, CancellationToken cancellationToken = default);
        Task<bool> CreateAsync(AddBookDto? bookDto, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(int bookId, CancellationToken cancellationToken = default);
    }
}
