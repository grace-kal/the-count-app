using Count.Models;

namespace Count.Services.Interfaces
{
    public interface IUserService
    {
        Task EditUser(User model);
        Task DeleteUser(string username);
        Task<User> FindUserById(string id);
        Task<User> FindUserByUsername(string username);
        Task<List<BmiUser>> UserBmis(string id);
        Task<List<Post>> UserPosts(string id);
        Task<List<Day>> UserDays(string id);
    }
}
