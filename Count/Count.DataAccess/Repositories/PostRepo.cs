using Count.DataAccess.Repositories.Interfaces;
using Count.Models;
using Microsoft.EntityFrameworkCore;

namespace Count.DataAccess.Repositories
{
    public class PostRepo : IPostRepo
    {
        private readonly CountDbContext _dbContext;
        public PostRepo(CountDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreatePost(Post model)
        {
            await _dbContext.Posts.AddAsync(model);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeletePost(Post model)
        {
            _dbContext.Posts.Update(model);
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditPost(Post model)
        {
            _dbContext.Posts.Update(model);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<Post> FindPost(int id)
        {
            var post = await _dbContext.Posts
                .Include(p => p.Author)
                .FirstOrDefaultAsync(p => p.Id == id);
            return post;
        }

        public async Task<List<Post>> AllPosts()
        {
            var list = await _dbContext.Posts
                .Include(p => p.Author)
                .ToListAsync();
            return list;
        }
    }
}
