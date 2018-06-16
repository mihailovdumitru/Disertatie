using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Model.Repositories;
using Model.Test;
using Services;
using Services.Facade.Interfaces;

namespace BeginTest.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class BeginTestController : Controller
    {
        private readonly IService service;
        private readonly IBeginTestFacade facade;
        private readonly IAuthService auth;

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
                var teacherLectures = await facade.GetTeachersLectures(teacher.TeacherID);

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
                testParams.TeacherID = teacher.TeacherID;
                testParams.StartTest = DateTime.Now;
                testParams.FinishTest = DateTime.Now.AddMinutes(testParams.Duration);
                result = await service.AddTestParams(testParams);
                return Ok(result);
            }

            return Unauthorized();
        }
    }
}