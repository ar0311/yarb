using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using Raven.Client.Embedded;
using Raven.Client;
using Yarb.Web.Blog;

namespace Yarb.Web.Tests
{
    public class SimpleTests : IDisposable
    {
        public EmbeddableDocumentStore docstore;
        public IDocumentSession session;
        public SimpleTests()
        {
            docstore = new EmbeddableDocumentStore
            {
                Configuration = { RunInMemory = true,
                    RunInUnreliableYetFastModeThatIsNotSuitableForProduction = true }
            };

            docstore.Initialize();
        }
        [Fact]
        public void CanSaveAndRetrievePost()
        {
            var post = new Post
                {
                    Title = "This is a test post.",
                    IsPublished = true,
                    Created = DateTime.Now,
                    Modified = DateTime.Now,
                    AllowComments = true,
                    Comments = new List<Comment>
                    {
                        new Comment
                        {
                            Created = DateTime.Now,
                            Content = "This is a test comment."
                        }
                    },
                    Content = new Content
                    {
                        ContentText = "This is the sample content for the post."
                    }
                };

            session = docstore.OpenSession();
            session.Store(post);
            session.SaveChanges();
            session.Dispose();

            session = docstore.OpenSession();
            var newpost = session.Load<Post>("posts/1");

            Assert.Equal(newpost.Title, "This is a test post.");

            session.Dispose();
        }
        public void Dispose()
        {
            docstore.Dispose();
        }
    }
}
