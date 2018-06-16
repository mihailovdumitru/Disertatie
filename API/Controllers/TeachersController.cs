using System.Collections.Generic;
using System.Threading.Tasks;
using AuthenticationLibrary.Interfaces;
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
        private readonly IAuthService authService;

        public TeachersController(IService service, IUsersFacade usersFacade, IAuthService authService)
        {
            this.service = service;
            this.usersFacade = usersFacade;
            this.authService = authService;
        }

        // GET: api/Teachers
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _log.Info("Get all the teachers.");
            var admin = await authService.ValidateAdmin(Request);
            var teacher = await authService.ValidateTeacher(Request);

            if (admin != null || teacher != null)
            {
                var teachers = await service.GetTeachers();
                return Ok(teachers);
            }

            return Unauthorized();
        }

        // POST: api/Teachers
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Teacher teacher)
        {
            _log.Info("Insert a new teacher: " + teacher.FirstName + " " + teacher.LastName);
            var admin = await authService.ValidateAdmin(Request);
            var teacherUser = await authService.ValidateTeacher(Request);

            if (admin != null || teacherUser != null)
            {
                var teacherID = await usersFacade.AddTeacherUser(teacher);
                return Ok(teacherID);
            }

            return Unauthorized();
        }

        // PUT: api/Teachers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Teacher teacher)
        {
            _log.Info("Update the teacher: " + teacher.FirstName + " " + teacher.LastName);
            var admin = await authService.ValidateAdmin(Request);
            var teacherUser = await authService.ValidateTeacher(Request);

            if (admin != null || teacherUser != null)
            {
                var success = await service.UpdateTeacher(teacher, id);
                return Ok(success);
            }

            return Unauthorized();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _log.Info("Delete the teacher. TeacherId: " + id);
            var admin = await authService.ValidateAdmin(Request);
            var teacherUser = await authService.ValidateTeacher(Request);

            if (admin != null || teacherUser != null)
            {
                var success = await service.DeleteTeacher(id);
                return Ok(success);
            }

            return Unauthorized();
        }
    }
}