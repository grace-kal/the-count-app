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
    public class DayRepo : IDayRepo
    {
        private readonly CountDbContext _dbContext;
        public DayRepo(CountDbContext dbContext) => _dbContext = dbContext;
        public async Task CreateDay(Day model)
        {
            _dbContext.Days.Add(model);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteDay(Day model)
        {
            _dbContext.Update(model);
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditDay(Day model)
        {
            _dbContext.Update(model);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<List<Day>> AllDaysOfUser(string username)
        {
            List<Day> list = await _dbContext.Days
                .Include(d => d.Meals)
                .Include(d => d.User)
                .Where(d => d.User.UserName == username)
                .ToListAsync();
            return list;
        }
        public async Task<List<Meal>> AllMealsOfDay(int id)
        {
            List<Meal> list = await _dbContext.Meals
                .Include(m => m.Day)
                .Include(m => m.Foods)
                .Where(m => m.DayId == id)
                .ToListAsync();

            foreach (var meal in list)
            {
                double caloriesOfMeal = 0;
                List<MealFood> mealFoods = await _dbContext.MealFoods.Where(mf => mf.MealId == meal.Id).ToListAsync();
                if (mealFoods != null)
                {
                    foreach (var mf in mealFoods)
                    {
                        caloriesOfMeal += mf.Calories;
                    }
                }
                meal.AllCalories = caloriesOfMeal;
                meal.CountOfFoodsForMeal = mealFoods.Count;
            }
            return list;
        }

        public async Task<Day> FindDay(int id)
        {
            var day = await _dbContext.Days
                .Include(d => d.Meals)
                .Include(d => d.User)
                .FirstOrDefaultAsync(d => d.Id == id);
            if (day == null)
            {
                throw new NullReferenceException($"No day with id:{id}.");
            }
            return day;
        }
    }
}
