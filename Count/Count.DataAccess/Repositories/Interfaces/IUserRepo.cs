using Count.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Count.DataAccess.Repositories.Interfaces
{
    public interface IUserRepo
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
