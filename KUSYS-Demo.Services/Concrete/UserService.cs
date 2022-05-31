using KUSYS_Demo.Data.Context;
using KUSYS_Demo.Data.Entities;
using KUSYS_Demo.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS_Demo.Services.Concrete
{
    public partial class UserService : IUserService
    {
        public async Task<List<Users>> ToListAsync()
        {
            using (var context = new KUSYSContext())
            {
                return await context.Users.ToListAsync();
            }
        }
        public async Task<Users?> Find(string userName, string password)
        {
            using (var context = new KUSYSContext())
            {
                return await context.Users.Where(x => x.Name == userName && x.Password == password).Include(x=> x.Role).FirstOrDefaultAsync();
            }
        }

        public async Task Create(Users entity)
        {
            using (var context = new KUSYSContext())
            {
                context.Users.Add(entity);
                await context.SaveChangesAsync();
            }
        }

        public async Task Update(Users entity)
        {
            using (var context = new KUSYSContext())
            {
                context.Users.Update(entity);
                await context.SaveChangesAsync();
            }
        }


    }
}
