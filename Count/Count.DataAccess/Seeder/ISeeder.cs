using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Count.DataAccess.Seeder
{
    public interface ISeeder
    {
        Task SeedAsync(CountDbContext dbContext, IServiceProvider serviceProvider);
    }
}
