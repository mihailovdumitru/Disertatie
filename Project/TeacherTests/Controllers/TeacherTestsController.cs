using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationLibrary.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Test;
using Services;

namespace TeacherTests.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class TeacherTestsController : Controller
    {
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
                await service.UpdateTest(test);

                return Ok(test);
            }

            return Unauthorized();
        }
    }
}
