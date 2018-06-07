using Model.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Facade.Interfaces
{
    public interface IBeginTestFacade
    {
        Task<IEnumerable<Lecture>> GetTeachersLectures(int teacherID);
        Task<bool> PutHashCodesForStudents(int classID);
    }
}
