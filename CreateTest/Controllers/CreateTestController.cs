using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Model.Test;
using Services;

namespace CreateTest.Controllers
{
    [Produces("application/json")]
    [Route("api/CreateTest")]
    public class CreateTestController : Controller
    {
        public readonly IService service;

        public CreateTestController(IService service)
        {
            this.service = service;
        }


        // GET: api/CreateTest
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/CreateTest/5
        [HttpGet("{id}", Name = "Get")]
        public void Get(HttpRequestMessage request, int id)
        {
            //Testare test = new Testare();
            //test.Nume = "Ana";
            //return test;
        }
        
        // POST: api/CreateTest
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Test test)
        {
            test.TeacherID = 1;

            return await service.AddTest(test);
        }
        
        // PUT: api/CreateTest/5
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
