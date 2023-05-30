using Count.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Count.Services.Interfaces
{
    public interface IMealService
    {
        Task<Meal> FindMeal(int id);
        Task CreateMeal(Meal model);
        Task EditMeal(Meal model);
        Task DeleteMeal(Meal model);
        Task RemoveFoodFromMeal(int id);
        Task CreateMealFood(MealFood model);
        Task<List<MealFood>> AllFoodsOfMeal(int id);
        Task<MealFood> FindMealFoodById(int id);
    }
}
