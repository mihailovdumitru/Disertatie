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
    [Route("api/Students")]
    public class StudentsController : Controller
    {
        private readonly IService service;

        public StudentsController(IService service)
        {
            this.service = service;
        }
        // GET: api/Students
        [HttpGet]
        public async Task<IEnumerable<Student>> Get()
        {
            return await service.GetStudents();
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Students
        [HttpPost]
        public async Task<int> Post([FromBody]Student student)
        {
            return await service.AddStudent(student);
        }
        
        // PUT: api/Students/5
        [HttpPut("{id}")]
        public async Task<bool> Put(int id, [FromBody]Student student)
        {
            return await service.UpdateStudent(student, id);
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
