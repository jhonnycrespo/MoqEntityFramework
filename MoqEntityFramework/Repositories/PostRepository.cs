using MoqEntityFramework.Models;
using System.Collections.Generic;
using System.Linq;

namespace MoqEntityFramework.Repositories
{
    public class PostRepository
    {
        private readonly BlogContext _context;

        public PostRepository(BlogContext context)
        {
            _context = context;
        }

        public List<Post> GetAllPosts()
        {
            IOrderedQueryable<Post> query = from b in _context.Posts
                                            orderby b.Title
                                            select b;

            return query.ToList();
        }

        public Post GetPostById(int id)
        {
            return _context.Posts.SingleOrDefault(p => p.BlogId == id);
        }
    }
}
