using MoqEntityFramework.Models;

namespace MoqEntityFramework
{
    using System.Data.Entity;

    public class BlogContext : DbContext, IBlogContext
    {
        // Your context has been configured to use a 'BlogContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'MoqEntityFramework.BlogContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'BlogContext' 
        // connection string in the application configuration file.
        public BlogContext()
            : base("name=BlogContext")
        {
        }

        // Note that the DbSet properties on the context are marked as virtual. This will
        // allow the mocking framework to derive from our context and overriding these
        // properties with a mocked implementation.
        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
    }
}