using Count.DataAccess.Repositories.Interfaces;
using Count.Models;
using Microsoft.EntityFrameworkCore;

namespace Count.DataAccess.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly CountDbContext _dbContext;
        public UserRepo(CountDbContext dbContext) => _dbContext = dbContext;

        public async Task EditUser(User model)
        {
            var user = await FindUserById(model.Id);
            user.Id = model.Id;
            user.UserName = model.UserName;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Weight = model.Weight;
            user.Height = model.Height;
            user.Country = model.Country;
            user.Age = model.Age;
            user.IsDelete = model.IsDelete;

            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();

        }
        public async Task DeleteUser(string username)
        {
            var user = await FindUserByUsername(username);
            user.IsDelete = true;
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<User> FindUserById(string id)
        {
            var user = await _dbContext.Users
                .Include(u => u.Days)
                .Include(u => u.Posts)
                .Include(u => u.UserBmis)
                .FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                throw new NullReferenceException($"No user with id:{id}");
            }
            else return user;
        }
        public async Task<User> FindUserByUsername(string username)
        {
            var user = await _dbContext.Users
                .Include(u => u.Days)
                .Include(u => u.Posts)
                .Include(u => u.UserBmis)
                .FirstOrDefaultAsync(u => u.UserName == username);
            if (user == null)
            {
                throw new NullReferenceException($"No user with username:{username}");
            }
            else return user;
        }


        public async Task<List<Post>> UserPosts(string id)
        {
            var user = await FindUserById(id);
            List<Post> posts = user.Posts.ToList();

            return posts;
        }
        public async Task<List<BmiUser>> UserBmis(string id)
        {
            var user = await FindUserById(id);
            List<BmiUser> bmis = user.UserBmis.ToList();

            return bmis;
        }

        public async Task<List<Day>> UserDays(string id)
        {
            var user = await FindUserById(id);
            List<Day> days = user.Days.ToList();

            return days;
        }
    }
}
