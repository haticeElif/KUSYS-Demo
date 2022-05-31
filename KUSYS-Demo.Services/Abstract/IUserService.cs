using KUSYS_Demo.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS_Demo.Services.Abstract
{
    public partial interface IUserService
    {
        Task<List<Users>> ToListAsync();
        Task<Users?> Find(string userName, string password);
        Task Create(Users entity);
        Task Update(Users entity);

    }
}
