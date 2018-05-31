using Microsoft.AspNetCore.Mvc;
using Model.Repositories;
using Model.Test;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IService
    {
        Task<ActionResult> AddTest(Test test);
        Task<int> AddTeacher(Teacher teacher);
        Task<int> AddClass(StudyClass studyClass);
        Task<List<StudyClass>> GetClasses();
        Task<int> AddLecture(Lecture lecture);
        Task<int> AddStudent(Student student);
    }
}
