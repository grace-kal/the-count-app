using Count.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Count.DataAccess.Repositories.Interfaces
{
    public interface IDayRepo
    {
        Task<Day> FindDay(int id);

        Task<List<Day>> AllDaysOfUser(string username);
        Task<List<Meal>> AllMealsOfDay(int id);
        Task CreateDay(Day model);
        Task EditDay(Day model);
        Task DeleteDay(Day model);
    }
}
