using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationLibrary.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Test;
using Services;

namespace StudentStart.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class StudentStartTestController : Controller
    {
        private readonly IAuthService authService;
        private readonly IService service;

        public StudentStartTestController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpGet]
        public async Task<Test> Get()
        {
            var student = await authService.ValidateStudent(Request);

            if(student != null)
            {
                var tests = await service.GetTests();
            }

            //return new string[] { "value1", "value2" };
        }




        // GET: api/StudentStartTest/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/StudentStartTest
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/StudentStartTest/5
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
