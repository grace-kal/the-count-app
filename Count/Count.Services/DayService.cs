using Count.DataAccess;
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
    public class DayService : IDayService
    {
        private readonly IDayRepo _repo;
        public DayService(IDayRepo repo) => _repo = repo;
        public async Task CreateDay(Day model)
        {
            await _repo.CreateDay(model);
        }

        public async Task DeleteDay(Day model)
        {
            var day = await FindDay(model.Id);
            day.IsDeleted = true;
            await _repo.DeleteDay(day);
        }

        public async Task EditDay(Day model)
        {
            await _repo.EditDay(model);
        }
        public async Task<List<Day>> AllDaysOfUser(string username)
        {
            return await _repo.AllDaysOfUser(username);
        }
        public async Task<List<Meal>> AllMealsOfDay(int id)
        {
            return await _repo.AllMealsOfDay(id);
        }

        public async Task<Day> FindDay(int id)
        {
            return await _repo.FindDay(id);
        }
    }
}
