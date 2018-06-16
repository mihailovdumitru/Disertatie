using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Model.Repositories;
using Model.StudentTest;
using Newtonsoft.Json;
using Services;

namespace StudentTest.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class StudentTest : Controller
    {
        private readonly IAuthService authService;
        private readonly IService service;

        public StudentTest(IAuthService authService, IService service)
        {
            this.authService = authService;
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetTest()
        {
            var student = await authService.ValidateStudent(Request);
            Model.StudentTest.StudentTest test = null;
            var currentTime = DateTime.Now;

            if (student != null)
            {
                var testParams = await service.GetTestsParams();
                var studentTestParams = testParams.FirstOrDefault(x => x.ClassID == student.ClassID &&
                                        (x.StartTest < currentTime && x.FinishTest > currentTime));

                if (studentTestParams != null)
                {
                    test = await service.GetTestByID(studentTestParams.TestID);
                }

                if (test != null)
                {
                    return Ok(test);
                }

                return Ok("");
            }

            return Unauthorized();
        }

        [HttpGet]
        public async Task<IActionResult> GetTestParams()
        {
            var student = await authService.ValidateStudent(Request);
            var currentTime = DateTime.Now;

            if (student != null)
            {
                var testParams = await service.GetTestsParams();
                var studentTestParams = testParams.FirstOrDefault(x => x.ClassID == student.ClassID &&
                                        (x.StartTest < currentTime && x.FinishTest > currentTime));

                if (studentTestParams != null)
                {
                    return Ok(studentTestParams);
                }

                return Ok("");
            }

            return Unauthorized();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTestParamsByTestID(int id)
        {
            var student = await authService.ValidateStudent(Request);
            var currentTime = DateTime.Now;

            if (student != null)
            {
                var testParams = await service.GetTestsParams();
                var studentTestParams = testParams.FirstOrDefault(x => x.TestID == id);

                if (studentTestParams != null)
                {
                    return Ok(studentTestParams);
                }

                return Ok("");
            }

            return Unauthorized();
        }

        [HttpPost]
        public async Task<IActionResult> PostTestsResults([FromBody]TestResults testResults)
        {
            var student = await authService.ValidateStudent(Request);
            testResults.TestResultDate = DateTime.Now;
            testResults.StudentID = student.StudentID;

            if (student != null)
            {
                float numberOfPoints = 0;
                var testParams = await service.GetTestsParams();
                var studentTestParams = testParams.FirstOrDefault(x => x.TestID == testResults.TestID);
                var testResultsList = JsonConvert.DeserializeObject<List<ResultWithAnswers>>(testResults.AnswersResult);
                var test = await service.GetFullTestByID(testResults.TestID);
                var penalty = studentTestParams.Penalty;
                bool answerIsCorrect = false;
                int nrOfCorrectAnswers = 0;
                int nrOfWrongAnswers = 0;
                int nrOfUnfilledAnswers = 0;
                ResultWithStats response = null;
                var testTotalNumberOfPoints = test.Questions.Sum(x => x.Question.Points);

                foreach (var elem in testResultsList)
                {
                    var question = test.Questions.FirstOrDefault(x => x.Question.QuestionID == elem.QuestionID);
                    var correctAns = question.Answers.Where(x => (x.Correct == true)).Select(i => i.AnswerID).ToList<int>();

                    if (elem.Answers.Count != 0)
                    {
                        answerIsCorrect = elem.Answers.ToList<int>().Intersect(correctAns).Count() == elem.Answers.Count();
                    }

                    if (answerIsCorrect && elem.Answers.Count != 0)
                    {
                        numberOfPoints += question.Question.Points;
                        nrOfCorrectAnswers++;
                    }
                    else if (penalty != 0 && elem.Answers.Count != 0)
                    {
                        numberOfPoints -= question.Question.Points * penalty / 100;
                        nrOfWrongAnswers++;
                    }
                    else if (elem.Answers.Count == 0)
                    {
                        nrOfUnfilledAnswers++;
                    }
                }

                testResults.Points = numberOfPoints;
                testResults.Mark = Convert.ToSingle(Math.Round((numberOfPoints / testTotalNumberOfPoints) * 10, 2));

                testResults.NrOfCorrectAnswers = nrOfCorrectAnswers;
                testResults.NrOfWrongAnswers = nrOfWrongAnswers;
                testResults.NrOfUnfilledAnswers = nrOfUnfilledAnswers;

                response = new ResultWithStats
                {
                    Mark = testResults.Mark,
                    NumberOfCorrectAnswers = nrOfCorrectAnswers,
                    NumberOfUnfilledAnswers = nrOfUnfilledAnswers,
                    NumberOfWrongAnswers = nrOfWrongAnswers,
                    Points = numberOfPoints
                };

                bool result = await service.AddTestResults(testResults);

                return Ok(response);
            }

            return Unauthorized();
        }

        [HttpGet]
        public async Task<IActionResult> GetTestsResults()
        {
            var student = await authService.ValidateStudent(Request);

            if (student != null)
            {
                var testParams = await service.GetTestsParams();
                var classTestParams = testParams.Where(x => x.ClassID == student.ClassID).OrderByDescending(t => t.FinishTest).First();
                var testsResults = await service.GetTestsResults();
                TestResults studentTestResults = null;

                if (testsResults.Count != 0)
                {
                    studentTestResults = testsResults.FirstOrDefault(x => x.StudentID == student.StudentID && x.TestID == classTestParams.TestID);
                    if(studentTestResults != null)
                        return Ok(studentTestResults);
                }

                return Ok("");
            }

            return Unauthorized();
        }

        [HttpGet]
        public async Task<IActionResult> GetFullTest()
        {
            var student = await authService.ValidateStudent(Request);

            if (student != null)
            {
                var testParams = await service.GetTestsParams();
                var classTestParams = testParams.Where(x => x.ClassID == student.ClassID).OrderByDescending(t => t.FinishTest).First();
                var test = await service.GetFullTestByID(classTestParams.TestID);

                if (test != null)
                {
                    return Ok(test);
                }

                return Ok("");
            }

            return Unauthorized();
        }
    }
}