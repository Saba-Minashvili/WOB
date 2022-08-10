using AutoMapper;
using Contracts;
using Contracts.Book;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Encoder.Abstraction;
using Services.Abstractions;
using System.Globalization;

namespace Services
{
    internal sealed class BookService : IBookService
    {
        private readonly IUnitOfWork? _unitOfWork;
        private readonly IEncodeService? _encoder;
        private readonly IMapper? _mapper;

        public BookService(IUnitOfWork? unitOfWork, IEncodeService? encoder, IMapper? mapper)
        {
            _unitOfWork = unitOfWork;
            _encoder = encoder;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var books = await _unitOfWork.BookRepository.GetAllAsync(cancellationToken);

            if(books == null)
            {
                throw new NullReferenceException(nameof(books));
            }

            var booksDto = _mapper.Map<IEnumerable<BookDto>>(books);

            foreach(var book in booksDto)
            {
                book.Image = _encoder.DecodeFromBase64(book.Image);
            }

            return booksDto;
        }

        public async Task<IEnumerable<BookDto>> GetByAuthorAsync(string? author, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(author))
            {
                throw new ArgumentNullException(nameof(author));
            }

            var books = await _unitOfWork.BookRepository.GetByAuthorAsync(author, cancellationToken);

            if (books == null)
            {
                throw new NullReferenceException(nameof(books));
            }

            var booksDto = _mapper.Map<IEnumerable<BookDto>>(books);

            foreach(var book in booksDto)
            {
                book.Image = _encoder.DecodeFromBase64(book.Image);
            }

            return booksDto;
        }

        public async Task<IEnumerable<BookDto>> GetByGenreAsync(string? genre, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(genre))
            {
                throw new ArgumentNullException(nameof(genre));
            }

            var books = await _unitOfWork.BookRepository.GetByGenreAsync(genre, cancellationToken);

            if (books == null)
            {
                throw new NullReferenceException(nameof(books));
            }

            var booksDto = _mapper.Map<IEnumerable<BookDto>>(books);

            foreach(var book in booksDto)
            {
                book.Image = _encoder.DecodeFromBase64(book.Image);
            }

            return booksDto;
        }

        public async Task<IEnumerable<BookDto>> GetByNameAsync(string? bookName, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(bookName))
            {
                throw new ArgumentNullException(nameof(bookName));
            }

            var books = await _unitOfWork.BookRepository.GetByNameAsync(bookName, cancellationToken);

            if (books == null)
            {
                throw new NullReferenceException(nameof(books));
            }

            var booksDto = _mapper.Map<IEnumerable<BookDto>>(books);

            foreach(var book in booksDto)
            {
                book.Image = _encoder.DecodeFromBase64(book.Image);
            }

            return booksDto;
        }

        public async Task<IEnumerable<BookDto>> GetByPageNumberAsync(int pageNumber, CancellationToken cancellationToken = default)
        {
            var books = await _unitOfWork.BookRepository.GetByPageNumberAsync(pageNumber, cancellationToken);

            if (books == null)
            {
                throw new NullReferenceException(nameof(books));
            }

            var booksDto = _mapper.Map<IEnumerable<BookDto>>(books);

            foreach (var book in booksDto)
            {
                book.Image = _encoder.DecodeFromBase64(book.Image);
            }

            return booksDto;
        }

        public async Task<IEnumerable<BookDto>> GetByReleaseDateAsync(string? releaseDate, CancellationToken cancellationToken = default)
        {

            if (releaseDate == null)
            {
                throw new ArgumentNullException(nameof(releaseDate));
            }

            var books = await _unitOfWork.BookRepository.GetByReleaseDateAsync(releaseDate, cancellationToken);

            if (books == null)
            {
                throw new NullReferenceException(nameof(books));
            }

            var booksDto = _mapper.Map<IEnumerable<BookDto>>(books);

            foreach (var book in booksDto)
            {
                book.Image = _encoder.DecodeFromBase64(book.Image);
            }

            return booksDto;
        }

        public async Task<BookDto?> GetByIdAsync(int bookId, CancellationToken cancellationToken = default)
        {
            var book = await _unitOfWork.BookRepository.GetByIdAsync(bookId, cancellationToken);

            if(book == null)
            {
                throw new NullReferenceException(nameof(book));
            }

            var bookDto = _mapper.Map<BookDto>(book);
            bookDto.Image = _encoder.DecodeFromBase64(book.Image);

            return bookDto;
        }
        public async Task<bool> CreateAsync(AddBookDto? bookDto, CancellationToken cancellationToken = default)
        {
            if(bookDto == null)
            {
                throw new ArgumentNullException(nameof(bookDto));
            }

            var book = _mapper.Map<Book>(bookDto);

            if (await BookExists(book))
            {
                throw new AlreadyExistsException("The book already exists.");
            }

            book.Image = _encoder.EncodeToBase64(bookDto.Image);
            book.Authors = ValidAuthorsCollection(bookDto.Authors);

            _unitOfWork.BookRepository.Create(book);

            var result = await _unitOfWork.SaveChangeAsync(cancellationToken);

            return result != 0;
        }

        public async Task<bool> UpdateAsync(int bookId, UpdateBookDto? bookDto, CancellationToken cancellationToken = default)
        {
            if(bookId == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bookId));
            }

            if(bookDto == null)
            {
                throw new ArgumentNullException(nameof(bookDto));
            }

            var book = await _unitOfWork.BookRepository.GetByIdAsync(bookId, cancellationToken);

            book.Name = bookDto.Name;
            book.Pages = bookDto.Pages;
            book.ReleaseDate = bookDto.ReleaseDate;
            book.Image = _encoder.EncodeToBase64(bookDto.Image);
            book.Genre = bookDto.Genre;
            book.Authors = ValidAuthorsCollection(bookDto.Authors);

            _unitOfWork.BookRepository.Update(bookId, book);

            var result = await _unitOfWork.SaveChangeAsync(cancellationToken);

            return result != 0;
        }

        public async Task<bool> DeleteAsync(int bookId, CancellationToken cancellationToken = default)
        {
            try
            {
                if (bookId == 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(bookId));
                }

                var book = await _unitOfWork.BookRepository.GetByIdAsync(bookId, cancellationToken);

                _unitOfWork.BookRepository.Delete(book);

                return true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static string? ConvertToValidDateFormat(string? dateTime)
        {
            string[] dateFormats = {"yyyy-MM-dd","dd-MM-yyyy", "MM-dd-yyyy",
                "yyyy.MM.dd", "dd.MM.yyyy", "MM.dd.yyyy",
                "dd/MM/yyyy", "MM/dd/yyyy", "yyyy/MM/dd", "yyyy/dd/MM"};

            DateTime? dt = DateTime.ParseExact(dateTime, dateFormats, CultureInfo.InvariantCulture);

            string result = dt.Value.ToString("dd.MM.yyyy");

            return result;
        }

        private List<Author>? ValidAuthorsCollection(List<AuthorDto>? collection)
        {
            foreach(var author in collection)
            {
                author.DateOfBirth = ConvertToValidDateFormat(author.DateOfBirth);
                if (author.DateOfDeath == null || author.DateOfDeath == "")
                {
                    author.DateOfDeath = "Present";
                }

                return _mapper.Map<List<Author>>(collection);
            }

            return null;
        }

        private async Task<bool> BookExists(Book? book)
        {
            var exists = await _unitOfWork.BookRepository.GetByNameAsync(book.Name);

            if(exists != null)
            {
                // This means that the book already exists in the database.
                return true;
            }

            // This means that the book doesn't exists in the database.
            return false;
        }
    }
}
