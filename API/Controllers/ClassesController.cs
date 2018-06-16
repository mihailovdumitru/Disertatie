using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model.Repositories;
using Services;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("api/Classes")]
    public class ClassesController : Controller
    {
        private readonly IService service;

        public ClassesController(IService service)
        {
            this.service = service;
        }

        // GET: api/Classes
        [HttpGet]
        public async Task<List<StudyClass>> Get()
        {
            return await service.GetClasses();
        }

        // POST: api/Classes
        [HttpPost]
        public async Task<int> Post([FromBody]StudyClass studyClass)
        {
            studyClass.IsActive = true;

            return await service.AddClass(studyClass);
        }

        // PUT: api/Classes/5
        [HttpPut("{id}")]
        public async Task<bool> Put(int id, [FromBody]StudyClass studyClass)
        {
            studyClass.IsActive = true;

            return await service.UpdateClass(studyClass, id);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await service.DeleteStudyClass(id);
        }
    }
}