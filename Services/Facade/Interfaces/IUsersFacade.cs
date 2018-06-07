using Model.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Facade.Interfaces
{
    public interface IUsersFacade
    {
        Task<int> AddTeacherUser(Teacher teacher);
        Task<int> AddStudentUser(Student student);
    }
}
