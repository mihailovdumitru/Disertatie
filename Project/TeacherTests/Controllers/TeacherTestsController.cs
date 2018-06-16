using System.Threading.Tasks;
using AuthenticationLibrary.Interfaces;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Model.Test;
using Services;

namespace TeacherTests.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class TeacherTestsController : Controller
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IAuthService authService;
        private readonly IService service;

        public TeacherTestsController(IAuthService authService, IService service)
        {
            this.authService = authService;
            this.service = service;
        }

        [HttpGet]
        [Route("testID/{testID}")]
        public async Task<IActionResult> GetFullTestByID(int testID)
        {
            var teacher = await authService.ValidateTeacher(Request);

            if (teacher != null)
            {
                _log.Info("Get full test by Id for the teacher: " + teacher.FirstName + " " + teacher.LastName);

                var test = await service.GetFullTestByID(testID);

                if (test != null)
                {
                    return Ok(test);
                }

                return Ok("");
            }

            return Unauthorized();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTest([FromBody]Test test)
        {
            var teacher = await authService.ValidateTeacher(Request);

            if (teacher != null)
            {
                _log.Info("Update test for the teacher: " + teacher.FirstName + " " + teacher.LastName);

                await service.UpdateTest(test);

                return Ok(test);
            }

            return Unauthorized();
        }
    }
}