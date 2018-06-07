using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Dto;
using Model.Repositories;
using Services;
using Services.Facade.Interfaces;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("api/Teachers")]
    public class TeachersController : Controller
    {
        private readonly IService service;
        private readonly IUsersFacade usersFacade;

        public TeachersController(IService service, IUsersFacade usersFacade)
        {
            this.service = service;
            this.usersFacade = usersFacade;
        }
        // GET: api/Teachers
        [HttpGet]
        public async Task<IEnumerable<Teacher>> Get()
        {
            return await service.GetTeachers();
        }
        
        // POST: api/Teachers
        [HttpPost]
        public async Task<int> Post([FromBody]Teacher teacher)
        {
            return await usersFacade.AddTeacherUser(teacher);
        }
        
        // PUT: api/Teachers/5
        [HttpPut("{id}")]
        public async Task<bool> Put(int id, [FromBody]Teacher teacher)
        {
            return await service.UpdateTeacher(teacher,id);
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await service.DeleteTeacher(id);
        }
        
    }
}
