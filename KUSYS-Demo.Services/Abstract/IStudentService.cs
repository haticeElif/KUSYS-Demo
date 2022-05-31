using KUSYS_Demo.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS_Demo.Services.Abstract
{
    public partial interface IStudentService
    {
        Task<List<Student>> ToListAsync();
        Task<Student?> Find(int id);
        Task Create(Student entity);
        Task Update(Student entity);
        Task Delete(int id);
        Task<List<Student>> ToListNoCourseAsync();


    }
}
