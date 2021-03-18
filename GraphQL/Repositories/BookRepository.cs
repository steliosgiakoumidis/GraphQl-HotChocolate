using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Repositories
{
    public class BookRepository
    {
        private readonly MainContext _context;

        public BookRepository(MainContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetBooks()
        {
            return await _context.Books.ToListAsync();
        }

        public Book GetBookById(int id)
        {
            return _context.Books
                .Include(x => x.Author)
                .FirstOrDefault(x => x.Id == id);
        }

        public List<Book> GetBooksWithAuthors()
        {
            return _context.Books.Include(x => x.Author).ToList();
        }

        public async Task<Book> CreateBook(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();

            return book;
        }
    }
}