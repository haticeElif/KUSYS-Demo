using KUSYS_Demo.Data.Context;
using KUSYS_Demo.Data.Entities;
using KUSYS_Demo.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS_Demo.Services.Concrete
{
    public partial class StudentService : IStudentService
    {
        public async Task<List<Student>> ToListAsync()
        {
            using (var context = new KUSYSContext())
            {
                return await context.Student.Include(i=>i.Course).ToListAsync();
            }
        }
        
        public async Task<Student?> Find(int id)
        {
            using (var context = new KUSYSContext())
            {
                return await context.Student.Where(x=>x.StudentId == id).Include(x=>x.Course).FirstOrDefaultAsync();
            }
        }

        public async Task Create(Student entity)
        {
            using (var context = new KUSYSContext())
            {
                context.Student.Add(entity);
                await context.SaveChangesAsync();
            }
        }

        public async Task Update(Student entity)
        {
            using (var context = new KUSYSContext())
            {
                context.Student.Update(entity);
                await context.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            using (var context = new KUSYSContext())
            {
                var student = await Find(id);
                context.Student.Remove(student);
                await context.SaveChangesAsync();
            }
        }
        public async Task<List<Student>> ToListNoCourseAsync()
        {
            using (var context = new KUSYSContext())
            {
                return await context.Student.Where(i => i.CourseId == null).ToListAsync();
            }
        }

    }
}
