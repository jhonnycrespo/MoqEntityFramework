using System.Data.Entity;
using MoqEntityFramework.Models;

namespace MoqEntityFramework
{
    public interface IBlogContext
    {
        DbSet<Blog> Blogs { get; set; }
        DbSet<Post> Posts { get; set; }
    }
}