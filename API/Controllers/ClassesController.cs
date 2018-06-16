using System.Collections.Generic;
using System.Threading.Tasks;
using AuthenticationLibrary.Interfaces;
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
        private readonly IAuthService authService;

        public ClassesController(IService service, IAuthService authService)
        {
            this.service = service;
            this.authService = authService;
        }

        // GET: api/Classes
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var admin = await authService.ValidateAdmin(Request);
            var teacher = await authService.ValidateTeacher(Request);

            if (admin != null || teacher != null)
            {
                var classes = await service.GetClasses();
                return Ok(classes);
            }

            return Unauthorized();
        }

        // POST: api/Classes
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]StudyClass studyClass)
        {
            var admin = await authService.ValidateAdmin(Request);
            var teacher = await authService.ValidateTeacher(Request);

            if (admin != null || teacher != null)
            {
                studyClass.IsActive = true;
                var classID = await service.AddClass(studyClass);
                return Ok(classID);
            }

            return Unauthorized();
        }

        // PUT: api/Classes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]StudyClass studyClass)
        {
            var admin = await authService.ValidateAdmin(Request);
            var teacher = await authService.ValidateTeacher(Request);

            if (admin != null || teacher != null)
            {
                studyClass.IsActive = true;
                var updated = await service.UpdateClass(studyClass, id);
                return Ok(updated);
            }

            return Unauthorized();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var admin = await authService.ValidateAdmin(Request);
            var teacher = await authService.ValidateTeacher(Request);

            if (admin != null || teacher != null)
            {
                var deleted = await service.DeleteStudyClass(id);
                return Ok(deleted);
            }

            return Unauthorized();
        }
    }
}