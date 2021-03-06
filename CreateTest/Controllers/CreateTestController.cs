﻿using System.Net.Http;
using System.Threading.Tasks;
using AuthenticationLibrary.Interfaces;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Model.Test;
using Services;

namespace CreateTest.Controllers
{
    [Produces("application/json")]
    [Route("api/CreateTest")]
    public class CreateTestController : Controller
    {
        private readonly IService service;
        private readonly IAuthService auth;
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public CreateTestController(IService service,IAuthService auth)
        {
            this.service = service;
            this.auth = auth;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Test test)
        {
            _log.Info("Insert a new test: " + test.Naming);
            var teacher = await auth.ValidateTeacher(Request);
            if (teacher != null)
            {    
                test.TeacherID = teacher.TeacherID;
                ActionResult result = await service.AddTest(test);
                return Ok(true);
            }

            return Unauthorized();
        }
    }
}