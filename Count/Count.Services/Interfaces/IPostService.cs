using Count.Models;

namespace Count.Services.Interfaces
{
    public interface IPostService
    {
        Task<List<Post>> AllPosts();
        Task<Post> FindPost(int id);
        Task CreatePost(Post model);
        Task EditPost(Post model);
        Task DeletePost(Post model);
    }
}
