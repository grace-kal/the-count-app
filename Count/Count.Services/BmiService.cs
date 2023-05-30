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
    public class BmiService : IBmiService
    {
        private readonly IBmiRepo _repo;
        public BmiService(IBmiRepo repo) => _repo = repo;

        public async Task CreateBmi(BmiUser model)
        {
            model.Date = DateTime.Now;
            var calculatedBmi = model.Weight / (model.Height * model.Height);
            calculatedBmi = calculatedBmi * 10000;
            model.CalculatedBmi = Math.Round(calculatedBmi, 2);
            if (calculatedBmi > 25.0)
            {
                model.Bmi = Bmi.Overweight;
            }
            else if (calculatedBmi >= 18.5 && calculatedBmi <= 24.9)
            {
                model.Bmi = Bmi.Normal;
            }
            else
            {
                model.Bmi = Bmi.Underweight;
            }

            await _repo.CreateBmi(model);
        }

        public async Task EditBmi(BmiUser model)
        {
            var calculatedBmi = model.Weight / (model.Height * model.Height);
            calculatedBmi = calculatedBmi * 10000;
            model.CalculatedBmi = Math.Round(calculatedBmi, 2); ;
            if (calculatedBmi > 25.0)
            {
                model.Bmi = Bmi.Overweight;
            }
            else if (calculatedBmi >= 18.5 && calculatedBmi <= 24.9)
            {
                model.Bmi = Bmi.Normal;
            }
            else
            {
                model.Bmi = Bmi.Underweight;
            }

            await _repo.EditBmi(model);
        }
        public async Task DeleteBmi(BmiUser model)
        {
            var bmi = await FindBmi(model.Id);
            bmi.IsDeleted = true;
            await _repo.DeleteBmi(bmi);
        }

        public async Task<BmiUser> FindBmi(int id)
        {
            return await _repo.FindBmi(id);
        }

        public async Task<List<BmiUser>> AllUserUserBmis(string id)
        {
            return await _repo.AllUserUserBmis(id);
        }
    }
}
