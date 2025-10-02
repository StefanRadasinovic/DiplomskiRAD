using DiplomskiRAD.Data;
using DiplomskiRAD.Models;

namespace DiplomskiRAD.Repository
{
    public class ReviewRepository
    {
        private readonly AppDbContext _context;

        public ReviewRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateReview(Review review)
        {
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
        }



    }
}
