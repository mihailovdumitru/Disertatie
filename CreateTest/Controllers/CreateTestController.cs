using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CreateTest.Model;
using Microsoft.AspNetCore.Mvc;

namespace CreateTest.Controllers
{
    [Produces("application/json")]
    [Route("api/CreateTest")]
    public class CreateTestController : Controller
    {
        // GET: api/CreateTest
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/CreateTest/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(HttpRequestMessage request, int id)
        {
            return "";
        }
        
        // POST: api/CreateTest
        [HttpPost]
        public void Post([FromBody]Test testObject)
        {
            Console.WriteLine(testObject.Lecture);
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
