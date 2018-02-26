using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MoqEntityFramework.Models;
using MoqEntityFramework.Repositories;
using MoqEntityFramework.Services;
using System.Collections.Generic;
using System.Data.Entity;

namespace MoqEntityFramework.Test
{
    [TestClass]
    public class PostTests
    {
        private Mock<DbSet<Post>> _mockPosts;
        private PostService _service;

        [TestInitialize]
        public void TestInitialize()
        {
            var data = new List<Post>
            {
                new Post { BlogId = 1, Title = "BBB" },
                new Post { BlogId = 2, Title = "ZZZ" },
                new Post { BlogId = 3, Title = "AAA" },
            };

            _mockPosts = new Mock<DbSet<Post>>();

            _mockPosts.SetSource(data);

            var mockContext = new Mock<BlogContext>();
            mockContext.Setup(c => c.Posts).Returns(_mockPosts.Object);
            PostRepository repository = new PostRepository(mockContext.Object);

            _service = new PostService(repository);
        }


        [TestMethod]
        public void ListPostsTest()
        {
            var posts = _service.ListPosts();

            Assert.AreEqual(3, posts.Count);
            Assert.AreEqual("AAA", posts[0].Title);
            Assert.AreEqual("BBB", posts[1].Title);
            Assert.AreEqual("ZZZ", posts[2].Title);
        }

        [TestMethod]
        public void GetPostTest()
        {
            var post = _service.ShowPost(1);

            Assert.AreEqual("BBB", post.Title);
        }
    }
}
