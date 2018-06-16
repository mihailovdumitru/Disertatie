using System.Collections.Generic;
using System.Threading.Tasks;
using AuthenticationLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Model.Repositories;
using Services;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("api/Lectures")]
    public class LecturesController : Controller
    {
        private readonly IService service;
        private readonly IAuthService authService;

        public LecturesController(IService service, IAuthService authService)
        {
            this.service = service;
            this.authService = authService;
        }

        // GET: api/Lectures
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var admin = await authService.ValidateAdmin(Request);
            var teacher = await authService.ValidateTeacher(Request);

            if (admin != null || teacher != null)
            {
                var lectures = await service.GetLectures();
                return Ok(lectures);
            }

            return Unauthorized();
        }

        // POST: api/Lectures
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Lecture lecture)
        {

            var admin = await authService.ValidateAdmin(Request);
            var teacher = await authService.ValidateTeacher(Request);

            if (admin != null || teacher != null)
            {
                var lectureID = await service.AddLecture(lecture);
                return Ok(lectureID);
            }

            return Unauthorized();
        }

        // PUT: api/Lectures/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Lecture lecture)
        {
            var admin = await authService.ValidateAdmin(Request);
            var teacher = await authService.ValidateTeacher(Request);

            if (admin != null || teacher != null)
            {
                var success = await service.UpdateLecture(lecture, id);
                return Ok(success);
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
                var success = await service.DeleteLecture(id);
                return Ok(success);
            }

            return Unauthorized();
        }
    }
}