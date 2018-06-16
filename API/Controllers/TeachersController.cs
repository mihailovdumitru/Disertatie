using System.Collections.Generic;
using System.Threading.Tasks;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Model.Repositories;
using Services;
using Services.Facade.Interfaces;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("api/Teachers")]
    public class TeachersController : Controller
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IService service;
        private readonly IUsersFacade usersFacade;

        public TeachersController(IService service, IUsersFacade usersFacade)
        {
            this.service = service;
            this.usersFacade = usersFacade;
        }

        // GET: api/Teachers
        [HttpGet]
        public async Task<IEnumerable<Teacher>> Get()
        {
            _log.Info("Get all the teachers.");

            return await service.GetTeachers();
        }

        // POST: api/Teachers
        [HttpPost]
        public async Task<int> Post([FromBody]Teacher teacher)
        {
            _log.Info("Insert a new teacher: " + teacher.FirstName + " " + teacher.LastName);

            return await usersFacade.AddTeacherUser(teacher);
        }

        // PUT: api/Teachers/5
        [HttpPut("{id}")]
        public async Task<bool> Put(int id, [FromBody]Teacher teacher)
        {
            _log.Info("Update the teacher: " + teacher.FirstName + " " + teacher.LastName);

            return await service.UpdateTeacher(teacher, id);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            _log.Info("Delete the teacher. TeacherId: " + id);

            return await service.DeleteTeacher(id);
        }
    }
}