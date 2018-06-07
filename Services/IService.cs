using Microsoft.AspNetCore.Mvc;
using Model.Dto;
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
        Task<IEnumerable<Teacher>> GetTeachers();
        Task<bool> UpdateTeacher(Teacher teacher, int teacherId);
        Task<IEnumerable<Lecture>> GetTeachersLecture(int teacherID);
        Task<Teacher> GetTeacher(int teacherID);
        Task<bool> DeleteTeacher(int teacherID);
        Task<int> AddClass(StudyClass studyClass);
        Task<bool> UpdateClass(StudyClass studyClass, int studyClassID);
        Task<bool> DeleteStudyClass(int studyClassID);
        Task<List<StudyClass>> GetClasses();
        Task<int> AddLecture(Lecture lecture);
        Task<bool> UpdateLecture(Lecture lecture, int lectureID);
        Task<bool> DeleteLecture(int lectureID);
        Task<IEnumerable<Lecture>> GetLectures();
        Task<int> AddStudent(Student student);
        Task<IEnumerable<Student>> GetStudents();
        Task<bool> UpdateStudent(Student student, int studentID);
        Task<bool> DeleteStudent(int studentID);
        Task<IEnumerable<Test>> GetTests();
        Task<bool> UpdateUser(User user, int userID);
        Task<int> AddUser(User user);
    }
}
