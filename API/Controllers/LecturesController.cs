using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Repositories;
using Services;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("api/Lectures")]
    public class LecturesController : Controller
    {
        public readonly IService service;

        public LecturesController(IService service)
        {
            this.service = service;
        }

        // GET: api/Lectures
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Lectures/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Lectures
        [HttpPost]
        public async Task<int> Post([FromBody]Lecture lecture)
        {
            return await service.AddLecture(lecture);
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
