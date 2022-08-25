using AutoMapper;
using Contracts.Book;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Encoder.Abstraction;
using Services.Abstractions;

namespace Services
{
    internal sealed class FavouriteBookService : IFavouriteBookService
    {
        private readonly IUnitOfWork? _unitOfWork;
        private readonly IEncodeService? _encoder;
        private readonly IMapper? _mapper;

        public FavouriteBookService(IUnitOfWork? unitOfWork, IEncodeService? encoder, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _encoder = encoder;
            _mapper = mapper;
        }
        public async Task<List<FavouriteBookDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var favouriteBooks = await _unitOfWork.FavouriteBookRepository.GetAllAsync(cancellationToken);

            if(favouriteBooks == null)
            {
                throw new NullReferenceException(nameof(favouriteBooks));
            }

            var favouriteBooksDto = _mapper.Map<List<FavouriteBookDto>>(favouriteBooks);

            foreach(var favouriteBook in favouriteBooksDto)
            {
                favouriteBook.Book.Image = _encoder.DecodeFromBase64(favouriteBook.Book.Image);
            }

            return favouriteBooksDto;
        }

        public async Task<List<FavouriteBookDto>> GetByUserIdAsync(string? userId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            var favouriteBooks = await _unitOfWork.FavouriteBookRepository.GetByUserIdAsync(userId, cancellationToken);

            if(favouriteBooks == null)
            {
                throw new NullReferenceException(nameof(favouriteBooks));
            }

            var favouriteBooksDto = _mapper.Map<List<FavouriteBookDto>>(favouriteBooks);

            foreach(var favouriteBook in favouriteBooksDto)
            {
                favouriteBook.Book.Image = _encoder.DecodeFromBase64(favouriteBook.Book.Image);
            }

            return favouriteBooksDto;
        }

        public async Task<FavouriteBookDto?> GetByIdAsync(int favouriteBookId, CancellationToken cancellationToken = default)
        {
            if(favouriteBookId == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(favouriteBookId));
            }

            var favouriteBook = await _unitOfWork.FavouriteBookRepository.GetByIdAsync(favouriteBookId, cancellationToken);

            if(favouriteBook == null)
            {
                throw new NullReferenceException(nameof(favouriteBook));
            }

            var favouriteBookDto = _mapper.Map<FavouriteBookDto>(favouriteBook);
            favouriteBook.Book.Image = _encoder.DecodeFromBase64(favouriteBook.Book.Image);

            return favouriteBookDto;
        }

        public async Task<bool> AddToFavouritesAsync(AddToFavouritesDto? favouriteBookDto, CancellationToken cancellationToken = default)
        {
            if(favouriteBookDto == null)
            {
                throw new ArgumentNullException(nameof(favouriteBookDto));
            }

            var exists = await _unitOfWork.FavouriteBookRepository.GetByIdAsync(favouriteBookDto.BookId, cancellationToken);

            if(exists != null)
            {
                throw new AlreadyExistsException("This book is already added in favourites.");
            }

            var book = await _unitOfWork.BookRepository.GetByIdAsync(favouriteBookDto.BookId, cancellationToken);

            if(book == null)
            {
                throw new NullReferenceException(nameof(book));
            }

            var user = await _unitOfWork.UserRepository.GetByIdAsync(favouriteBookDto.UserId, cancellationToken);

            if(user == null)
            {
                throw new NullReferenceException(nameof(user));
            }

            var favouriteBook = new FavouriteBook { Book = book, UserId = favouriteBookDto.UserId };

            user.FavouriteBooks.Add(favouriteBook);

            _unitOfWork.FavouriteBookRepository.AddToFavourites(favouriteBook);

            var result = await _unitOfWork.SaveChangeAsync(cancellationToken);

            return result != 0;
        }

        public async Task<bool> DeleteFromFavouritesAsync(int favouriteBookId, CancellationToken cancellationToken = default)
        {
            if(favouriteBookId == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(favouriteBookId));
            }

            var favouriteBook = await _unitOfWork.FavouriteBookRepository.GetByIdAsync(favouriteBookId, cancellationToken);

            if(favouriteBook == null)
            {
                throw new NullReferenceException(nameof(favouriteBook));
            }

            _unitOfWork.FavouriteBookRepository.DeleteFromFavourites(favouriteBook);

            var result = await _unitOfWork.SaveChangeAsync(cancellationToken);

            return result != 0;
        }
    }
}
