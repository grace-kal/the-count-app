using Count.DataAccess;
using Count.DataAccess.Repositories.Interfaces;
using Count.Models;
using Count.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Count.Services
{
    public class FoodService : IFoodService
    {
        private readonly IFoodRepo _repo;
        public FoodService(IFoodRepo repo) => _repo = repo;
        public async Task<List<Food>> AllFoods()
        {
            return await _repo.AllFoods();
        }

        public async Task CreateFood(Food model)
        {
            await _repo.CreateFood(model);
        }

        public async Task DeleteFood(Food model)
        {
            await _repo.DeleteFood(model);
        }

        public async Task EditFood(Food model)
        {
            await _repo.EditFood(model);
        }

        public async Task<Food> FindFood(int id)
        {
            return await _repo.FindFood(id);
        }

        public async Task<Food> FindFoodByName(string name)
        {
            return await _repo.FindFoodByName(name);
        }
    }
}
