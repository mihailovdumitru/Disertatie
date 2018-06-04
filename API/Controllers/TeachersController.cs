﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Dto;
using Model.Repositories;
using Services;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("api/Teachers")]
    public class TeachersController : Controller
    {
        private readonly IService service;

        public TeachersController(IService service)
        {
            this.service = service;
        }
        // GET: api/Teachers
        [HttpGet]
        public async Task<IEnumerable<Teacher>> Get()
        {
            return await service.GetTeachers();
        }

        // GET: api/Teachers/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "";
        }
        
        // POST: api/Teachers
        [HttpPost]
        public async Task<int> Post([FromBody]Teacher teacher)
        {
            return await service.AddTeacher(teacher);
        }
        
        // PUT: api/Teachers/5
        [HttpPut("{id}")]
        public async Task<bool> Put(int id, [FromBody]Teacher teacher)
        {
            return await service.UpdateTeacher(teacher,id);
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
