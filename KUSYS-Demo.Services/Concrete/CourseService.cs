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
    public partial class CourseService : ICourseService
    {
        public async Task<List<Course>> ToListAsync()
        {
            using (var context = new KUSYSContext())
            {
                return await context.Course.ToListAsync();
            }
        }
        public async Task<Course?> Find(string id)
        {
            using (var context = new KUSYSContext())
            {
                return await context.Course.FindAsync(id);
            }
        }

        public async Task Create(Course entity)
        {
            using (var context = new KUSYSContext())
            {
                context.Course.Add(entity);
                await context.SaveChangesAsync();
            }
        }

        public async Task Update(Course entity)
        {
            using (var context = new KUSYSContext())
            {
                context.Course.Update(entity);
                await context.SaveChangesAsync();
            }
        }

        public async Task Delete(string id)
        {
            using (var context = new KUSYSContext())
            {
                var Course = await Find(id);
                context.Course.Remove(Course);
                await context.SaveChangesAsync();
            }
        }

    }
}
