using Count.DataAccess.Repositories.Interfaces;
using Count.Models;
using Count.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Count.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _repo;
        public UserService(IUserRepo repo) => _repo = repo;

        public async Task EditUser(User model)
        {
            await _repo.EditUser(model);
        }
        public async Task DeleteUser(string username)
        {
            await _repo.DeleteUser(username);
        }
        public async Task<User> FindUserById(string id)
        {
            return await _repo.FindUserById(id);
        }
        public async Task<User> FindUserByUsername(string username)
        {
            return await _repo.FindUserByUsername(username);
        }

        public async Task<List<Post>> UserPosts(string id)
        {
            return await _repo.UserPosts(id);
        }
        public async Task<List<BmiUser>> UserBmis(string id)
        {
            return await _repo.UserBmis(id);
        }

        public async Task<List<Day>> UserDays(string id)
        {
            return await _repo.UserDays(id);
        }
    }
}
