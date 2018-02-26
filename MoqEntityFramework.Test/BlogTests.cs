using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MoqEntityFramework.Models;
using MoqEntityFramework.Services;
using System.Collections.Generic;
using System.Data.Entity;

// https://msdn.microsoft.com/en-us/library/dn314429(v=vs.113).aspx#Testing query scenarios
namespace MoqEntityFramework.Test
{
    [TestClass]
    public class BlogTests
    {
        //private BlogService _service;
        private Mock<DbSet<Blog>> _mockBlogs;

        // This attribute is needed when we want to run a function before execution of a test.
        // For example we want to run the same test 5 times and want to set some property value
        // before running each time.
        [TestInitialize]
        public void TestInitialize()
        {
            _mockBlogs = new Mock<DbSet<Blog>>();

            //var mockContext = new Mock<UniversityContext>();
            //mockContext.Setup(c => c.Blogs).Returns(_mockBlogs.Object);
            //_service = new BlogService(mockContext.Object);
        }

        // Testing Query Scenarios
        [TestMethod]
        public void GetAllBlogs_orders_by_name()
        {
            var data = new List<Blog>
            {
                new Blog { Name = "BBB" },
                new Blog { Name = "ZZZ" },
                new Blog { Name = "AAA" },
            };

            _mockBlogs.SetSource(data);

            var mockContext = new Mock<BlogContext>();
            mockContext.Setup(c => c.Blogs).Returns(_mockBlogs.Object);
            var service = new BlogService(mockContext.Object);

            var blogs = service.GetAllBlogs();

            Assert.AreEqual(3, blogs.Count);
            Assert.AreEqual("AAA", blogs[0].Name);
            Assert.AreEqual("BBB", blogs[1].Name);
            Assert.AreEqual("ZZZ", blogs[2].Name);
        }

        // Testing Non-Query Scenarios
        [TestMethod]
        public void CreateBlog_saves_a_blog_via_context()
        {
            var mockSet = new Mock<DbSet<Blog>>();

            var mockContext = new Mock<BlogContext>();
            mockContext.Setup(m => m.Blogs).Returns(mockSet.Object);

            var service = new BlogService(mockContext.Object);
            service.AddBlog("ADO.NET Blog", "http://blogs.msdn.com/adonet");

            mockSet.Verify(m => m.Add(It.IsAny<Blog>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}
