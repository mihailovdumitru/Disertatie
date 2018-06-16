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
    [Route("api/Students")]
    public class StudentsController : Controller
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IService service;
        private readonly IUsersFacade usersFacade;
        private readonly IAuthService authService;

        public StudentsController(IService service, IUsersFacade usersFacade, IAuthService authService)
        {
            this.service = service;
            this.usersFacade = usersFacade;
            this.authService = authService;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _log.Info("Get all the students.");
            var admin = await authService.ValidateAdmin(Request);
            var teacher = await authService.ValidateTeacher(Request);

            if (admin != null || teacher != null)
            {
                var students = await service.GetStudents();
                return Ok(students);
            }

            return Unauthorized();
        }

        // POST: api/Students
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Student student)
        {
            _log.Info("Insert a new student: " + student.FirstName + " " + student.LastName);
            var admin = await authService.ValidateAdmin(Request);
            var teacher = await authService.ValidateTeacher(Request);

            if (admin != null || teacher != null)
            {
                var studentID = await usersFacade.AddStudentUser(student);
                return Ok(studentID);
            }

            return Unauthorized();
        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Student student)
        {
            _log.Info("Update the student: " + student.FirstName + " " + student.LastName);
            var admin = await authService.ValidateAdmin(Request);
            var teacher = await authService.ValidateTeacher(Request);

            if (admin != null || teacher != null)
            {
                var success = await service.UpdateStudent(student, id);
                return Ok(success);
            }

            return Unauthorized();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _log.Info("Delete the student. StudentId: " + id);
            var admin = await authService.ValidateAdmin(Request);
            var teacher = await authService.ValidateTeacher(Request);

            if (admin != null || teacher != null)
            {
                var success = await service.DeleteStudent(id);
                return Ok(success);
            }

            return Unauthorized();
        }
    }
}