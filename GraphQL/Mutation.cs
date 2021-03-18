using System.Threading.Tasks;
using GraphQL.Entities;
using HotChocolate;
using HotChocolate.Subscriptions;

namespace GraphQL.Repositories
{
    public class Mutation
    {
        public async Task<Author> CreateDepartment([Service] AuthorRepository authorRepository,
            [Service]ITopicEventSender eventSender, string authorName)
        {
            var newAuthor = new Author()
            {
                Name = authorName
            };
            var authorDepartment = await authorRepository.CreateAuthor(newAuthor);

            await eventSender.SendAsync("AuthorCreated", authorDepartment);

            return authorDepartment;            
        }
          
        public async Task<Book> CreateBookWithAuthorId([Service]BookRepository bookRepository, 
            string title, string description, int authorId)
        {
            var newBook = new Book
            {
                Title = title,
                Description = description,
                AuthorId = authorId
            };

            var createdBook = await bookRepository.CreateBook(newBook);
            return createdBook;
        }

        public async Task<Book> CreateBookWithAuthor([Service] BookRepository bookRepository,
            string title, string description, string authorName)
        {
            var newBook = new Book()
            {
                Title = title,
                Description = description,
                Author = new Author(){Name = authorName}
            };

            var createdBook = await bookRepository.CreateBook(newBook);
            return createdBook;
        }
    }
}