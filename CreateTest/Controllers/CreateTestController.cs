using System.Net.Http;
using System.Threading.Tasks;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Model.Test;
using Services;

namespace CreateTest.Controllers
{
    [Produces("application/json")]
    [Route("api/CreateTest")]
    public class CreateTestController : Controller
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public readonly IService service;

        public CreateTestController(IService service)
        {
            this.service = service;
        }

        // GET: api/CreateTest/5
        [HttpGet("{id}", Name = "Get")]
        public void Get(HttpRequestMessage request, int id)
        {
            //Testare test = new Testare();
            //test.Nume = "Ana";
            //return test;
        }

        // POST: api/CreateTest
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Test test)
        {
            test.TeacherID = 1;
            _log.Info("Insert a new test: " + test.Naming);

            return await service.AddTest(test);
        }
    }
}