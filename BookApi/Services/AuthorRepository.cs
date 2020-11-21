using BookApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.Services
{
    public class AuthorRepository : IAuthorRepository
    {
        private BookDbContext _bookDbContext;

        public AuthorRepository(BookDbContext bookDbContext)
        {
            _bookDbContext = bookDbContext;
        }

        public bool AuthorExists(int authorId)
        {
            return _bookDbContext.Authors
                .Any(a => a.Id == authorId);
        }

        public bool CreateAuthor(Author author)
        {
            _bookDbContext.Add(author);
            return Save();
        }

        public bool DeleteAuthor(Author author)
        {
            _bookDbContext.Remove(author);
            return Save();
        }

        public Author GetAuthor(int authorId)
        {
            return _bookDbContext.Authors
                .Where(a => a.Id == authorId)
                .FirstOrDefault();
        }

        public ICollection<Author> GetAuthors()
        {
            return _bookDbContext.Authors
                .OrderBy(a => a.LastName)
                .ToList();
        }

        public ICollection<Author> GetAuthorsOfABook(int bookId)
        {
            return _bookDbContext.BookAuthors
                .Where(b => b.Book.Id == bookId)
                .Select(a => a.Author)
                .ToList();
        }

        public ICollection<Book> GetBooksByAuthor(int authorId)
        {
            return _bookDbContext.BookAuthors
                .Where(a => a.Author.Id == authorId)
                .Select(b => b.Book)
                .ToList();
        }

        public bool Save()
        {
            var saved = _bookDbContext.SaveChanges();
            return saved >= 0 ? true : false;
        }

        public bool UpdateAuthor(Author author)
        {
            _bookDbContext.Update(author);
            return Save();
        }
    }
}
