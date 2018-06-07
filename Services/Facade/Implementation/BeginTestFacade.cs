using Model.Repositories;
using Services.Facade.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Facade.Implementation
{
    public class BeginTestFacade:IBeginTestFacade
    {
        private readonly IService service;
        private Random random = new Random();
        private readonly int hashSize;

        public BeginTestFacade(IService service)
        {
            this.service = service;
            hashSize = Convert.ToInt32(ConfigurationManager.AppSettings.Get("HashSize"));
        }

        public async Task<IEnumerable<Lecture>> GetTeachersLectures(int teacherID)
        {
            var teacher = await service.GetTeacher(teacherID);
            var lectures = await service.GetLectures();

            return lectures.Where(lecture => teacher.Lectures.Contains(lecture.LectureID));
        }

        public async Task<bool> PutHashCodesForStudents(int classID)
        {
            var students = await service.GetStudents();
            var classStudents = students.Where(student => student.ClassID == classID).ToList<Student>();
            bool result = false;

            foreach (var student in classStudents)
            {
                //student.Password = RandomString(hashSize);
                result = await service.UpdateStudent(student,student.StudentID);
            }

            return result;
        }



        private string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }


    }
}
