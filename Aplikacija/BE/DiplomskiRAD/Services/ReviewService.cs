using DiplomskiRAD.Models;
using DiplomskiRAD.Repository;
using static DiplomskiRAD.DTOs.ReviewDTO;
using static DiplomskiRAD.DTOs.ServiceDTO;

namespace DiplomskiRAD.Services
{
    public class ReviewService
    {
        private readonly ReviewRepository _reviewRepository;
        private readonly ServiceRepository _serviceRepository;
        private readonly UserRepository _userRepository;

        public ReviewService(ReviewRepository reviewRepository, ServiceRepository serviceRepository, UserRepository userRepository)
        {
            _reviewRepository = reviewRepository;
            _serviceRepository = serviceRepository;
            _userRepository = userRepository;
        }

        public async Task<ReviewInfo> CreateReview(Guid serviceId, Guid userId, CreateReviewDto dto)
        {
            var service = await _serviceRepository.GetServiceById(serviceId);

            if (service == null)
            {
                throw new ArgumentException("Service doesn't exist");
            }

            var user = await _userRepository.GetUserById(userId);

            if (user == null)
            {
                throw new ArgumentException("User doesn't exist");
            }

            if(service.UserId != userId)
            {
                throw new ArgumentException("This is not service from this user");
            }

            var review = new Review
            {
                Id = Guid.NewGuid(),
                Comment = dto.Comment,
                Grade = dto.Grade,
                ServiceId = serviceId,
                UserId = userId,
            };

            _reviewRepository.CreateReview(review);


            var reviewInfo = new ReviewInfo
            {
                Comment = review.Comment,
                Grade = review.Grade,
                
            };

            return reviewInfo;

        }

    }
}
