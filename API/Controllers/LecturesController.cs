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
        public async Task<IEnumerable<Lecture>> Get()
        {
            return await service.GetLectures();
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
        public async Task<bool> Put(int id, [FromBody]Lecture lecture)
        {
            return await service.UpdateLecture(lecture, id);
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await service.DeleteLecture(id);
        }
    }
}
