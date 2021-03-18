using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Entities;
using GraphQL.Repositories;

namespace GraphQL
{
    public class AuthorHandler
    {
        private readonly AuthorRepository repository;

        public AuthorHandler(AuthorRepository repository)
        {
            repository = repository;
        }
        
        public List<Author> GetAllAuthors()
        {
            return repository.GetAllDAuthorsOnly();
        }

        public List<Author> GetAllAuthorsWithBooks()
        {
            return repository.GetAllAuthorsWithBooks();
        }

        public async Task<Author> CreateAuthor(Author author)
        {
            return await repository.CreateAuthor(author);
        }
    }
}