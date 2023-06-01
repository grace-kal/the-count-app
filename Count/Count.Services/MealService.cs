using Count.DataAccess;
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
    public class MealService : IMealService
    {
        private readonly IMealRepo _repo;
        private readonly IFoodRepo _foodRepo;
        public MealService(IMealRepo repo, IFoodRepo foodRepo)
        {
            _repo = repo;
            _foodRepo = foodRepo;
        }

        public async Task<Meal> FindMeal(int id)
        {
            return await _repo.FindMeal(id);
        }

        public async Task RemoveFoodFromMeal(int id)
        {
            await _repo.RemoveFoodFromMeal(id);
        }

        public async Task CreateMeal(Meal model)
        {
            await _repo.CreateMeal(model);
        }

        public async Task DeleteMeal(Meal model)
        {
            await _repo.DeleteMeal(model);
        }

        public async Task EditMeal(Meal model)
        {
            await _repo.EditMeal(model);
        }

        public async Task<MealFood> FindMealFoodById(int id)
        {
            return await _repo.FindMealFoodById(id);
        }

        public async Task<List<MealFood>> AllFoodsOfMeal(int id)
        {
            return await _repo.AllFoodsOfMeal(id);
        }

        //adding food from list of foods view
        public async Task AddFoodsToMeal(List<int> listOfMealIds, int foodId, double quantity)
        {
            //to get the calories for the selected quantity:
            //grams of the whole food devided by grams of what is eaten = how many times is the eaten contained in the whole
            //the calories devided of the whole devided by the upper number
            var food = await _foodRepo.FindFood(foodId);

            foreach (var mealId in listOfMealIds)
            {
                var mf = new MealFood();
                
                mf.FoodId = food.Id;
                var meal = await FindMeal(mealId);
                mf.MealId = meal.Id;
                
                mf.Quantity = quantity;

                mf.Calories = food.Calories/(food.Quantity / quantity);
                await _repo.CreateMealFood(mf);
            }
        }
    }
}
