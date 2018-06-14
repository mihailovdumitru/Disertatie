using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationLibrary.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Repositories;
using Services;

namespace TeacherViewResults.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class TeacherViewResultsController : Controller
    {
        private readonly IService service;
        private readonly IAuthService authService;
        public TeacherViewResultsController(IService service, IAuthService authService)
        {
            this.service = service;
            this.authService = authService;
        }



        [HttpGet]
        [Route("testID/{testID}/classID/{classID}")]
        public async Task<IActionResult> GetTestsResults(int testID, int classID)
        {
            var teacher = await authService.ValidateTeacher(Request);

            if (teacher != null)
            {
                var tests = await service.GetTests();
                var test = tests.Where(x => x.TestID == testID);

                var students = await service.GetStudents();
                var classStudents = students.Where(x => x.ClassID == classID);

                var testsResults = await service.GetTestsResults();
                var testResultForTest = testsResults.Where(x => x.TestID == testID && 
                                                           classStudents.Select(s=>s.StudentID).Contains(x.StudentID))
                                                            .Join(classStudents, 
                                                                  testElem => testElem.StudentID,
                                                                  student => student.StudentID,(
                                                                  testElem, student) => 
                                                            new {
                                                                student.FirstName,
                                                                student.LastName,
                                                                student.Email,
                                                                testElem.NrOfCorrectAnswers,
                                                                testElem.NrOfUnfilledAnswers,
                                                                testElem.NrOfWrongAnswers,
                                                                testElem.Points,
                                                                Mark = Math.Round(testElem.Mark,2)
                                                            }).ToList();


                if (testResultForTest.Count != 0)
                {     
                    return Ok(testResultForTest);
                }

                return Ok("");
            }

            return Unauthorized();
        }





        //// GET: api/TeacherViewResults
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/TeacherViewResults/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}
        
        //// POST: api/TeacherViewResults
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}
        
        //// PUT: api/TeacherViewResults/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}
        
        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
