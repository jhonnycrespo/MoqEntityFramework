using MoqEntityFramework.Models;
using MoqEntityFramework.Repositories;
using System.Collections.Generic;

namespace MoqEntityFramework.Services
{
    public class PostService
    {
        private readonly PostRepository _repository;

        public PostService(PostRepository repository)
        {
            _repository = repository;
        }

        public List<Post> ListPosts()
        {
            return _repository.GetAllPosts();
        }

        public Post ShowPost(int id)
        {
            return _repository.GetPostById(id);
        }
    }
}
