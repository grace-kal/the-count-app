using Count.DataAccess.Repositories.Interfaces;
using Count.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Count.DataAccess.Repositories
{
    public class AccountRepo : IAccountRepo
    {
        private readonly CountDbContext _dbContext;
        public AccountRepo(CountDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> FindUserByUsername(string username)
        {
            var user = await _dbContext.Users
                .Include(u => u.Country)
                .FirstOrDefaultAsync(u => u.UserName == username);
            return user;
        }
    }
}
