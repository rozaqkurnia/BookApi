using BookApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.Services
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly BookDbContext _bookDbContext;

        public ReviewRepository(BookDbContext bookDbContext)
        {
            _bookDbContext = bookDbContext;
        }

        public bool CreateReview(Review review)
        {
            _bookDbContext.Add(review);
            return Save();
        }

        public bool DeleteReview(Review review)
        {
            _bookDbContext.Remove(review);
            return Save();
        }

        public bool DeleteReviews(List<Review> reviews)
        {
            _bookDbContext.RemoveRange(reviews);
            return Save();
        }

        public Book GetBookOfAReview(int reviewId)
        {
            return _bookDbContext.Reviews
                .Where(r => r.Id == reviewId)
                .Select(b => b.Book)
                .FirstOrDefault();
        }

        public Review GetReview(int reviewId)
        {
            return _bookDbContext.Reviews
                .Where(r => r.Id == reviewId)
                .FirstOrDefault();
        }

        public ICollection<Review> GetReviews()
        {
            return _bookDbContext.Reviews
                .OrderByDescending(r => r.Rating)
                .ToList();
        }

        public ICollection<Review> GetReviewsOfABook(int bookId)
        {
            return _bookDbContext.Reviews
                .Where(r => r.Book.Id == bookId)
                .ToList();
        }

        public bool ReviewExists(int reviewId)
        {
            return _bookDbContext.Reviews
                .Any(r => r.Id == reviewId);
        }

        public bool Save()
        {
            var saved = _bookDbContext.SaveChanges();
            return saved >= 0 ? true : false;
        }

        public bool UpdateReview(Review review)
        {
            _bookDbContext.Update(review);
            return Save();
        }
    }
}
