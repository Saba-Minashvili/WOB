using AutoMapper;
using Contracts.FeedBack;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Services.Abstractions;

namespace Services
{
    internal sealed class FeedBackService : IFeedBackService
    {
        private readonly IUnitOfWork? _unitOfWork;
        private readonly IMapper? _mapper;

        public FeedBackService(IUnitOfWork? unitOfWork, IMapper? mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FeedBackDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var feedBacks = await _unitOfWork.FeedBackRepository.GetAllAsync(cancellationToken);

            if(feedBacks == null)
            {
                throw new NullReferenceException(nameof(feedBacks));
            }

            var feedBacksDto = _mapper.Map<IEnumerable<FeedBackDto>>(feedBacks);

            return feedBacksDto;
        }

        public async Task<IEnumerable<FeedBackDto>> GetByBookIdAsync(int bookId, CancellationToken cancellationToken = default)
        {
            if(bookId == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bookId));
            }

            var feedBacks = await _unitOfWork.FeedBackRepository.GetByBookIdAsync(bookId, cancellationToken);

            if(feedBacks == null)
            {
                throw new NullReferenceException(nameof(feedBacks));
            }

            var feedBacksDto = _mapper.Map<IEnumerable<FeedBackDto>>(feedBacks);

            return feedBacksDto;
        }

        public async Task<FeedBackDto?> GetByIdAsync(int feedBackId, CancellationToken cancellationToken = default)
        {
            if(feedBackId == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(feedBackId));
            }

            var feedBack = await _unitOfWork.FeedBackRepository.GetByIdAsync(feedBackId, cancellationToken);

            if(feedBack == null)
            {
                throw new NullReferenceException(nameof(feedBack));
            }

            var feedBackDto = _mapper.Map<FeedBackDto>(feedBack);

            return feedBackDto;
        }

        public async Task<bool> CreateAsync(AddFeedBackDto? feedBackDto, CancellationToken cancellationToken = default)
        {
            if(feedBackDto == null)
            {
                throw new ArgumentNullException(nameof(feedBackDto));
            }

            var feedBack = _mapper.Map<FeedBack>(feedBackDto);

            feedBack.CommentDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            feedBack.ModifiedAt = "";

            _unitOfWork.FeedBackRepository.CreateAsync(feedBack);

            var result = await _unitOfWork.SaveChangeAsync(cancellationToken);

            return result != 0;
        }

        public async Task<bool> DeleteAsync(int feedBackId, CancellationToken cancellationToken = default)
        {
            if(feedBackId == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(feedBackId));
            }

            var feedBack = await _unitOfWork.FeedBackRepository.GetByIdAsync(feedBackId, cancellationToken);

            if(feedBack == null)
            {
                throw new NullReferenceException(nameof(feedBack));
            }

            _unitOfWork.FeedBackRepository.DeleteAsync(feedBack);

            var result = await _unitOfWork.SaveChangeAsync(cancellationToken);

            return result != 0;
        }

        public async Task<bool> UpdateAsync(int feedBackId, JsonPatchDocument? feedBackPatch, CancellationToken cancellationToken = default)
        {
            if(feedBackId == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(feedBackId));
            }

            var feedBack = await _unitOfWork.FeedBackRepository.GetByIdAsync(feedBackId, cancellationToken);

            feedBack.ModifiedAt = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            _unitOfWork.FeedBackRepository.UpdateAsync(feedBack, feedBackPatch);

            var result = await _unitOfWork.SaveChangeAsync(cancellationToken);

            return result != 0;
        }
    }
}
