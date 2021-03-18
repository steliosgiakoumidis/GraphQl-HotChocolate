using System.Threading;
using System.Threading.Tasks;
using GraphQL.Entities;
using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using HotChocolate.Types;

namespace GraphQL.Repositories
{
    public class Subscription
    {
        [SubscribeAndResolve]
        public async ValueTask<ISourceStream<Author>> OnAuthorCreate([Service] ITopicEventReceiver eventReceiver,
            CancellationToken cancellationToken)
        {
            return await eventReceiver.SubscribeAsync<string, Author>("AuthorCreated", cancellationToken);
        }


        [SubscribeAndResolve]
        public async ValueTask<ISourceStream<Book>> OnBookGet([Service] ITopicEventReceiver eventReceiver,
            CancellationToken cancellationToken)
        {
            return await eventReceiver.SubscribeAsync<string, Book>("ReturnedBook", cancellationToken);
        }
    }
}