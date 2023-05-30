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
    public class FoodRepo : IFoodRepo
    {
        private readonly CountDbContext _dbContext;
        public FoodRepo(CountDbContext dbContext) => _dbContext = dbContext;
        public async Task<List<Food>> AllFoods()
        {
            List<Food> list = await _dbContext.Foods
                .Include(f => f.Meals)
                .Include(f => f.CreatedBy)
                .ToListAsync();
            return list;
        }

        public async Task CreateFood(Food model)
        {
            await _dbContext.Foods.AddAsync(model);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteFood(Food model)
        {
            var food = await FindFood(model.Id);

            _dbContext.Foods.Remove(food);
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditFood(Food model)
        {
            _dbContext.Foods.Update(model);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Food> FindFood(int id)
        {
            var food = await _dbContext.Foods
                .Include(f => f.Meals)
                .Include(f => f.CreatedBy)
                .FirstOrDefaultAsync(f => f.Id == id);
            if (food == null)
            {
                throw new NullReferenceException($"No food with id:{id}");
            }
            return food;
        }

        public async Task<Food> FindFoodByName(string name)
        {
            var food = await _dbContext.Foods
                .Include(f => f.CreatedBy)
                .Include(f => f.Meals)
                .FirstOrDefaultAsync(f => f.Name == name);
            if (food == null)
            {
                throw new NullReferenceException($"No food with name:{name}");
            }
            return food;
        }
    }
}
