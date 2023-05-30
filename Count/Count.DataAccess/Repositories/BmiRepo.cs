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
    public class BmiRepo : IBmiRepo
    {
        private readonly CountDbContext _dbContext;
        public BmiRepo(CountDbContext dbContext) => _dbContext = dbContext;
        public async Task CreateBmi(BmiUser model)
        {
            await _dbContext.BmisUsers.AddAsync(model);
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditBmi(BmiUser model)
        {
            _dbContext.BmisUsers.Update(model);
            await _dbContext.SaveChangesAsync();

        }
        public async Task DeleteBmi(BmiUser model)
        {
            _dbContext.BmisUsers.Update(model);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<BmiUser> FindBmi(int id)
        {
            var bmiUser = await _dbContext.BmisUsers
                .Include(bu => bu.User)
                .FirstOrDefaultAsync(bu => bu.Id == id);
            if (bmiUser == null)
            {
                throw new NullReferenceException($"No BMI with id:{id}.");
            }
            return bmiUser;
        }

        public async Task<List<BmiUser>> AllUserUserBmis(string id)
        {
            List<BmiUser> list = await _dbContext.BmisUsers
                .Where(bu => bu.UserId == id)
                .Include(bu => bu.User)
                .ToListAsync();
            return list;

        }
    }
}
