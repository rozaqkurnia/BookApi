using BookApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.Services
{
    public class ReviewerRepository : IReviewerRepository
    {
        private readonly BookDbContext _bookDbContext;

        public ReviewerRepository(BookDbContext bookDbContext)
        {
            _bookDbContext = bookDbContext;
        }

        public bool CreateReviewer(Reviewer reviewer)
        {
            _bookDbContext.Add(reviewer);
            return Save();
        }

        public bool DeleteReviewer(Reviewer reviewer)
        {
            _bookDbContext.Remove(reviewer);
            return Save();
        }

        public Reviewer GetReviewer(int reviewerId)
        {
            return _bookDbContext.Reviewers
                .Where(r => r.Id == reviewerId)
                .FirstOrDefault();
        }

        public Reviewer GetReviewerOfAReview(int reviewId)
        {
            var reviewerId = _bookDbContext.Reviews
                .Where(t => t.Id == reviewId)
                .Select(r => r.Reviewer.Id).FirstOrDefault();

            return _bookDbContext.Reviewers
                .Where(r => r.Id == reviewerId)
                .FirstOrDefault();
        }

        public ICollection<Reviewer> GetReviewers()
        {
            return _bookDbContext.Reviewers
                .OrderBy(r => r.LastName).ToList();
        }

        public ICollection<Review> GetReviewsByReviewer(int reviewerId)
        {
            return _bookDbContext.Reviews
                .Where(r => r.Reviewer.Id == reviewerId)
                .ToList();
        }

        public bool ReviewerExists(int reviewerId)
        {
            return _bookDbContext.Reviewers
                .Any(r => r.Id == reviewerId);
        }

        public bool Save()
        {
            var saved = _bookDbContext.SaveChanges();
            return saved >= 0 ? true : false;
        }

        public bool UpdateReviewer(Reviewer reviewer)
        {
            _bookDbContext.Update(reviewer);
            return Save();
        }
    }
}
