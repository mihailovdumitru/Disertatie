using Microsoft.AspNetCore.Mvc;
using Model.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Facade.Interfaces
{
    public interface IBeginTestFacade
    {
        Task<IEnumerable<Lecture>> GetTeachersLectures(int teacherID);
        Task<ContentResult> GenerateHashCodes(int classID);
    }
}