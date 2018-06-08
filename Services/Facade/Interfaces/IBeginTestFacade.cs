using Microsoft.AspNetCore.Mvc;
using Model.Repositories;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Services.Facade.Interfaces
{
    public interface IBeginTestFacade
    {
        Task<IEnumerable<Lecture>> GetTeachersLectures(int teacherID);
        Task<ContentResult> GenerateHashCodes(int classID);
    }
}
