using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AuthenticationLibrary.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Repositories;
using Model.Test;
using Services;
using Services.Facade.Interfaces;
using Services.Infrastructure;

namespace BeginTest.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class BeginTestController : Controller
    {
        private readonly IService service;
        private readonly IBeginTestFacade facade;
        private readonly IAuthService auth;
        int teacherID = 21;

        public BeginTestController(IService service, IBeginTestFacade facade, IAuthService auth)
        {
            this.service = service;
            this.facade = facade;
            this.auth = auth;
        }

        [HttpGet]
        public async Task<IActionResult> Tests()
        {
            IEnumerable<Test> teachersTests = new List<Test>();
            var teacher = await auth.ValidateTeacher(Request);

            if (teacher != null)
            {
                var tests = await service.GetTests();
                teachersTests = tests.Where(test => test.TeacherID == teacher.TeacherID);

                return Ok(teachersTests);
            }

            return Unauthorized();
        }

        [HttpGet]
        public async Task<IActionResult> TeacherLectures()
        {
            var teacher = await auth.ValidateTeacher(Request);
            if (teacher != null)
            {
                var teacherLectures = await facade.GetTeachersLectures(teacherID);

                return Ok(teacherLectures);
            }

            return Unauthorized();
        }

        [HttpPost]
        public async Task<IActionResult> GenerateFileWithHashCodes([FromBody] int classID)
        {
            var teacher = await auth.ValidateTeacher(Request);
            if (teacher != null)
            {
                var fileContent = await facade.GenerateHashCodes(classID);
                return Ok(fileContent);
            }

            return Unauthorized();
        }

        [HttpPost]
        public async Task<IActionResult> AddTestParameters([FromBody] TestParameters testParams)
        {
            bool result = false;
            var teacher = await auth.ValidateTeacher(Request);
            if (teacher != null)
            {
                testParams.TeacherID = teacherID;
                testParams.StartTest = DateTime.Now;
                testParams.FinishTest = DateTime.Now.AddMinutes(testParams.Duration);
                result = await service.AddTestParams(testParams);
                return Ok(result);
            }

            return Unauthorized();
        }



        /*[HttpPut]
        public async Task<ContentResult> HashCodesForStudents([FromBody] int classID)
        {

            return await facade.PutHashCodesForStudents(classStudents);
        }*/

        // POST: api/Lectures
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Lectures/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
