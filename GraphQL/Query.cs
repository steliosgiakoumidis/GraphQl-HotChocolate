using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQL.Entities;
using GraphQL.Repositories;
using HotChocolate;
using HotChocolate.Subscriptions;

namespace GraphQL
{
    public class Query
    {
        public async Task<List<Book>> AllBooksOnly([Service] BookRepository bookRepository) =>
            await bookRepository.GetBooks();

        public List<Book> AllBooksWithAuthors([Service] BookRepository bookRepository) =>
            bookRepository.GetBooksWithAuthors();

        public async Task<Book> GetEmployeeById([Service] BookRepository bookRepository, 
            [Service]ITopicEventSender eventSender, int id)
        {
            var gottenBook = bookRepository.GetBookById(id);
            await eventSender.SendAsync("ReturnedBook", gottenBook);
            return gottenBook;
        }           

        public List<Author> AllAuthorsOnly([Service] AuthorHandler authorHandler) =>
            authorHandler.GetAllAuthors();

        public List<Author> AllAuthorsWithBooks([Service] AuthorHandler authorHandler) =>
            authorHandler.GetAllAuthorsWithBooks();
    }
}