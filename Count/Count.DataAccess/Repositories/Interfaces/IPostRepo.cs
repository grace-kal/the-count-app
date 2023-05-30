using Count.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Count.DataAccess.Repositories.Interfaces
{
    public interface IPostRepo
    {
        Task<List<Post>> AllPosts();
        Task<Post> FindPost(int id);
        Task CreatePost(Post model);
        Task EditPost(Post model);
        Task DeletePost(Post model);
    }
}
