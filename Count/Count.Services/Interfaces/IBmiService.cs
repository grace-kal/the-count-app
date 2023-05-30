using Count.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Count.Services.Interfaces
{
    public interface IBmiService
    {
        Task<BmiUser> FindBmi(int id);
        Task<List<BmiUser>> AllUserUserBmis(string id);
        Task CreateBmi(BmiUser model);
        Task EditBmi(BmiUser model);
        Task DeleteBmi(BmiUser model);
    }
}
