using Count.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Count.Services.Interfaces
{
    public interface IFoodService
    {
        Task<Food> FindFood(int id);
        Task<Food> FindFoodByName(string name);
        Task<List<Food>> AllFoods();
        Task CreateFood(Food model);
        Task EditFood(Food model);
        Task DeleteFood(Food model);
    }
}
