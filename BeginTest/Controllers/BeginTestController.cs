using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationLibrary.Interfaces;
using log4net;
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
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
                _log.Info("Get tests for the teacher: " + teacher.FirstName + " " + teacher.LastName);

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
                _log.Info("Get lectures for the teacher: " + teacher.FirstName + " " + teacher.LastName);

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
    }
}