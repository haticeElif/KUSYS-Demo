using KUSYS_Demo.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUSYS_Demo.Services.Abstract
{
    public partial interface ICourseService
    {
        Task<List<Course>> ToListAsync();
        Task<Course?> Find(string id);
        Task Create(Course entity);
        Task Update(Course entity);
        Task Delete(string id);

    }
}
