using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Repositories;
using Model.Test;
using Services;
using Services.Facade.Interfaces;
using Services.Infrastructure;

namespace BeginTest.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class BeginTestController : Controller
    {
        private readonly IService service;
        private readonly IBeginTestFacade facade;
        int teacherID = 21;

        public BeginTestController(IService service, IBeginTestFacade facade)
        {
            this.service = service;
            this.facade = facade;
        }

        [HttpGet]
        public async Task<IEnumerable<Test>> Tests()
        {
            var tests = await service.GetTests();
            return tests.Where(test => test.TeacherID == teacherID);
        }

        [HttpGet]
        public async Task<IEnumerable<Lecture>> TeacherLectures()
        {
            return await facade.GetTeachersLectures(teacherID);
        }

        [HttpPost]
        public async Task<ContentResult> GenerateFileWithHashCodes([FromBody] int classID)
        {
            return await facade.GenerateHashCodes(classID);
        }

        [HttpPost]
        public async Task<bool> AddTestParameters([FromBody] TestParameters testParams)
        {
            testParams.TeacherID = teacherID;
            testParams.StartTest = DateTime.Now;
            testParams.FinishTest = DateTime.Now.AddMinutes(testParams.Duration);
            return await service.AddTestParams(testParams);
        }

        /*[HttpPut]
        public async Task<ContentResult> HashCodesForStudents([FromBody] int classID)
        {

            return await facade.PutHashCodesForStudents(classStudents);
        }*/

        // POST: api/Lectures
        [HttpPost]
        public void Post([FromBody]string value)
        {
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
