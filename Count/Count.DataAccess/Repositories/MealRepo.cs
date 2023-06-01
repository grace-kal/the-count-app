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
    public class MealRepo : IMealRepo
    {
        private readonly CountDbContext _dbContext;
        public MealRepo(CountDbContext dbContext) => _dbContext = dbContext;
        public async Task<Meal> FindMeal(int id)
        {
            var meal = await _dbContext.Meals
                .Include(m => m.Foods)
                .Include(m => m.Day)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meal == null)
            {
                throw new NullReferenceException($"No meal with id:{id}");
            }
            return meal;
        }

        public async Task RemoveFoodFromMeal(int id)
        {
            var mealfood = await _dbContext.MealFoods.FirstOrDefaultAsync(mf => mf.Id == id);
            if (mealfood == null)
            {
                throw new NullReferenceException($"No such mealfood obj.");
            }
            _dbContext.MealFoods.Remove(mealfood);
            await _dbContext.SaveChangesAsync();
        }

        public async Task CreateMeal(Meal model)
        {
            await _dbContext.Meals.AddAsync(model);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteMeal(Meal model)
        {
            var meal = await FindMeal(model.Id);
            meal.IsDeleted = true;

            _dbContext.Meals.Update(meal);
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditMeal(Meal model)
        {
            _dbContext.Meals.Update(model);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<MealFood>> AllFoodsOfMeal(int id)
        {
            List<MealFood> allFoods = await _dbContext.MealFoods
                .Include(mf => mf.Food)
                .Include(mf => mf.Meal)
                .Where(mf => mf.MealId == id).ToListAsync();
            return allFoods;
        }

        public async Task CreateMealFood(MealFood model)
        {
            await _dbContext.MealFoods.AddAsync(model);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<MealFood> FindMealFoodById(int id)
        {
            var mealfood = await _dbContext.MealFoods
                .Include(mf => mf.Meal)
                .Include(mf => mf.Food)
                .FirstOrDefaultAsync(mf => mf.Id == id);
            if (mealfood == null)
            {
                throw new NullReferenceException($"No obj with id:{id}");
            }
            return mealfood;
        }

    }
}
