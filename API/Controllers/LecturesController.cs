using System.Collections.Generic;
using System.Threading.Tasks;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Model.Repositories;
using Services;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("api/Lectures")]
    public class LecturesController : Controller
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public readonly IService service;

        public LecturesController(IService service)
        {
            this.service = service;
        }

        // GET: api/Lectures
        [HttpGet]
        public async Task<IEnumerable<Lecture>> Get()
        {
            _log.Info("Get all the lectures.");

            return await service.GetLectures();
        }

        // POST: api/Lectures
        [HttpPost]
        public async Task<int> Post([FromBody]Lecture lecture)
        {
            _log.Info("Insert a new lecture: " + lecture.Name);

            return await service.AddLecture(lecture);
        }

        // PUT: api/Lectures/5
        [HttpPut("{id}")]
        public async Task<bool> Put(int id, [FromBody]Lecture lecture)
        {
            _log.Info("Update the lecture: " + lecture.Name);

            return await service.UpdateLecture(lecture, id);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            _log.Info("Delete the lecture. LectureId: " + id);

            return await service.DeleteLecture(id);
        }
    }
}