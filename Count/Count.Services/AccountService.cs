using Count.DataAccess;
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
    public class AccountService : IAccountService
    {
        private readonly IAccountRepo _repo;
        public AccountService(IAccountRepo repo)
        {
            _repo= repo;
        }

        public async Task<User> FindUserByUsername(string username)
        {
            return await _repo.FindUserByUsername(username);
        }
    }
}
