using Contracts.Book;

namespace Services.Abstractions
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<BookDto>> GetByNameAsync(string? bookName, CancellationToken cancellationToken = default);
        Task<IEnumerable<BookDto>> GetByAuthorAsync(string? author, CancellationToken cancellationToken = default);
        Task<IEnumerable<BookDto>> GetByGenreAsync(string? genre, CancellationToken cancellationToken = default);
        Task<IEnumerable<BookDto>> GetByPageNumberAsync(int pageNumber, CancellationToken cancellationToken = default);
        Task<IEnumerable<BookDto>> GetByReleaseDateAsync(string? releaseDate, CancellationToken cancellationToken = default);
        Task<BookDto?> GetByIdAsync(int bookId, CancellationToken cancellationToken = default);
        Task<bool> CreateAsync(AddBookDto? bookDto, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(int bookId, UpdateBookDto? bookDto, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(int bookId, CancellationToken cancellationToken = default);
    }
}
