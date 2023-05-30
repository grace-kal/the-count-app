using AutoMapper;
using Count.DataAccess.Repositories;
using Count.DataAccess.Repositories.Interfaces;
using Count.Models;
using Count.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Count.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepo _repo;
        public PostService(IPostRepo repo) => _repo = repo;

        public async Task<List<Post>> AllPosts()
        {
            return await _repo.AllPosts();
        }

        public async Task CreatePost(Post model)
        {
            await _repo.CreatePost(model);
        }

        public async Task DeletePost(Post model)
        {
            var post = await FindPost(model.Id);
            post.IsDelete = true;

            await _repo.DeletePost(post);
        }

        public async Task EditPost(Post model)
        {
           await _repo.EditPost(model);
        }

        public async Task<Post> FindPost(int id)
        {
            return await _repo.FindPost(id);
        }
    }
}
