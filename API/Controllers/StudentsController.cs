using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model.Repositories;
using Services;
using Services.Facade.Interfaces;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("api/Students")]
    public class StudentsController : Controller
    {
        private readonly IService service;
        private readonly IUsersFacade usersFacade;

        public StudentsController(IService service, IUsersFacade usersFacade)
        {
            this.service = service;
            this.usersFacade = usersFacade;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<IEnumerable<Student>> Get()
        {
            return await service.GetStudents();
        }

        // POST: api/Students
        [HttpPost]
        public async Task<int> Post([FromBody]Student student)
        {
            return await usersFacade.AddStudentUser(student);
        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        public async Task<bool> Put(int id, [FromBody]Student student)
        {
            return await service.UpdateStudent(student, id);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await service.DeleteStudent(id);
        }
    }
}