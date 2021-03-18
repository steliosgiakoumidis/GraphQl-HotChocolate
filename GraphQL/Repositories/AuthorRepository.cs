using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Repositories
{
    public class AuthorRepository
    {
        private MainContext _mainContext;

        public AuthorRepository(MainContext mainContext)
        {
            _mainContext = mainContext;
        }

        public List<Author> GetAllDAuthorsOnly()
        {
            return _mainContext.Authors.ToList();
        }

        public List<Author> GetAllAuthorsWithBooks()
        {
            return _mainContext.Authors
                .Include(x => x.Books)
                .ToList();
        }

        public async Task<Author> CreateAuthor(Author author)
        {
            await _mainContext.Authors.AddAsync(author);
            await _mainContext.SaveChangesAsync();
            return author;
        }
    }
}