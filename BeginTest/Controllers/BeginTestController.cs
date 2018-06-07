using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IFileGenerator fileGenerator;

        public BeginTestController(IService service, IBeginTestFacade facade, IFileGenerator fileGenerator)
        {
            this.service = service;
            this.facade = facade;
            this.fileGenerator = fileGenerator;
        }

        [HttpGet]
        public async Task<IEnumerable<Test>> Tests()
        {
            int teacherID = 1;
            var tests = await service.GetTests();
            return tests.Where(test => test.TeacherID == teacherID);
        }

        [HttpGet]
        public async Task<IEnumerable<Lecture>> TeacherLectures()
        {
            int teacherID = 1;
            return await facade.GetTeachersLectures(teacherID);
        }

        [HttpPost]
        public async Task<ContentResult> GenerateFileWithHashCodes([FromBody] int classID)
        {
            int teacherID = 1;
            var students = await service.GetStudents();
            var classStudents = students.Where(student => student.ClassID == classID).ToList<Student>();

            return fileGenerator.GenerateFile<Student>(classStudents);
        }

        [HttpPut]
        public async Task<bool> HashCodesForStudents([FromBody] int classID)
        {
            return await facade.PutHashCodesForStudents(classID);
        }

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
