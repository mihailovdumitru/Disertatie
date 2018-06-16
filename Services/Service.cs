using Model.Test;
using Services.Infrastructure;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using Services.Endpoints;
using Model.Repositories;
using System.Collections.Generic;
using Model.Dto;
using Model.StudentTest;

namespace Services
{
    public class Service: IService
    {
        private readonly IRestHttpClient restHttpClient;
        private readonly string testApiUrl;
        private readonly string apiUrl;
        private readonly string beginTestUrl;
        private readonly string authUrl;
        private readonly string studentTestUrl;

        public Service(IRestHttpClient restHttpClient)
        {
            this.restHttpClient = restHttpClient;
            testApiUrl = ConfigurationManager.AppSettings.Get("TestApiUrl");
            apiUrl = ConfigurationManager.AppSettings.Get("ApiUrl");
            beginTestUrl = ConfigurationManager.AppSettings.Get("BeginTestUrl");
            authUrl = ConfigurationManager.AppSettings.Get("AuthUrl");
            studentTestUrl = ConfigurationManager.AppSettings.Get("StudentTest");
        }

        public async Task<ActionResult> AddTest(Test test)
        {
            return await restHttpClient.Post<Test, ActionResult>(testApiUrl, $"{TestEndpoint.AddTest}", test);
        }

        public async Task<int> AddTeacher(Teacher teacher)
        {
            return await restHttpClient.Post<Teacher, int>(apiUrl, $"{RepositoriesEndpoint.AddTeacher}", teacher);
        }

        public async Task<IEnumerable<Teacher>> GetTeachers()
        {
            return await restHttpClient.Get<IEnumerable<Teacher>>(apiUrl, $"{RepositoriesEndpoint.GetTeachers}");
        }

        public async Task<Teacher> GetTeacher(int teacherID)
        {
            return await restHttpClient.Get<Teacher>(apiUrl, string.Format(RepositoriesEndpoint.GetTeacher, teacherID));
        }

        public async Task<bool> UpdateTeacher(Teacher teacher, int teacherId)
        {
            return await restHttpClient.Put<Teacher>(apiUrl, string.Format(RepositoriesEndpoint.UpdateTeacher, teacherId), teacher);
        }

        public async Task<int> AddClass(StudyClass studyClass)
        {
            return await restHttpClient.Post<StudyClass, int>(apiUrl, $"{RepositoriesEndpoint.AddClass}", studyClass);
        }

        public async Task<List<StudyClass>> GetClasses()
        {
            return await restHttpClient.Get<List<StudyClass>>(apiUrl, $"{RepositoriesEndpoint.GetClasses}");
        }

        public async Task<bool> UpdateClass(StudyClass studyClass, int studyClassID)
        {
            return await restHttpClient.Put<StudyClass>(apiUrl, string.Format(RepositoriesEndpoint.UpdateClass, studyClassID), studyClass);
        }

        public async Task<bool> DeleteStudyClass(int studyClassID)
        {
            return await restHttpClient.Delete(apiUrl, string.Format(RepositoriesEndpoint.DeleteClass, studyClassID));
        }

        public async Task<int> AddLecture(Lecture lecture)
        {
            return await restHttpClient.Post<Lecture, int>(apiUrl, $"{RepositoriesEndpoint.AddLecture}", lecture);
        }

        public async Task<bool> UpdateLecture(Lecture lecture, int lectureID)
        {
            return await restHttpClient.Put<Lecture>(apiUrl, string.Format(RepositoriesEndpoint.UpdateLecture, lectureID), lecture);
        }

        public async Task<IEnumerable<Lecture>> GetLectures()
        {
            return await restHttpClient.Get<IEnumerable<Lecture>>(apiUrl, $"{RepositoriesEndpoint.GetLectures}");
        }

        public async Task<bool> DeleteLecture(int lectureID)
        {
            return await restHttpClient.Delete(apiUrl, string.Format(RepositoriesEndpoint.DeleteLecture, lectureID));
        }

        public async Task<IEnumerable<Lecture>> GetTeachersLecture(int teacherID)
        {
            return await restHttpClient.Get<IEnumerable<Lecture>>(apiUrl, string.Format(RepositoriesEndpoint.GetTeachersLecture, teacherID));
        }

        public async Task<bool> DeleteTeacher(int teacherID)
        {
            return await restHttpClient.Delete(apiUrl, string.Format(RepositoriesEndpoint.DeleteTeacher, teacherID));
        }

        public async Task<int> AddStudent(Student student)
        {
            return await restHttpClient.Post<Student, int>(apiUrl, $"{RepositoriesEndpoint.AddStudent}", student);
        }

        public async Task<IEnumerable<Student>> GetStudents()
        {
            return await restHttpClient.Get<IEnumerable<Student>>(apiUrl, $"{RepositoriesEndpoint.GetStudents}");
        }

        public async Task<bool> UpdateStudent(Student student, int studentID)
        {
            return await restHttpClient.Put<Student>(apiUrl, string.Format(RepositoriesEndpoint.UpdateStudent, studentID), student);
        }

        public async Task<bool> DeleteStudent(int studentID)
        {
            return await restHttpClient.Delete(apiUrl, string.Format(RepositoriesEndpoint.DeleteStudent, studentID));
        }

        public async Task<IEnumerable<Test>> GetTests()
        {
            return await restHttpClient.Get<IEnumerable<Test>>(beginTestUrl, RepositoriesEndpoint.GetTests);
        }

        public async Task<Teacher> GetTeacherAuth()
        {
            return await restHttpClient.Get<Teacher>(authUrl, RepositoriesEndpoint.GetTeacherAuthObj);
        }

        public async Task<int> AddUser(User user)
        {
            return await restHttpClient.Post<User, int>(apiUrl, $"{RepositoriesEndpoint.AddUser}", user);
        }

        public async Task<bool> UpdateUser(User user, int userID)
        {
            return await restHttpClient.Put<User>(apiUrl, string.Format(RepositoriesEndpoint.UpdateUser, userID), user);
        }

        public async Task<bool> AddTestParams(TestParameters testParams)
        {
            return await restHttpClient.Post<TestParameters,bool>(beginTestUrl, $"{RepositoriesEndpoint.AddTestParams}", testParams);
        }

        public async Task<List<TestParameters>> GetTestsParams()
        {
            return await restHttpClient.Get<List<TestParameters>>(studentTestUrl, RepositoriesEndpoint.GetTestsParams);
        }


        public async Task<User> GetUserByUsername(string username)
        {
            return await restHttpClient.Get<User>(apiUrl, string.Format(RepositoriesEndpoint.GetUserByUsername, username));
        }

        public async Task<StudentTest> GetTestByID(int testID)
        {
            return await restHttpClient.Get<StudentTest>(studentTestUrl, string.Format(RepositoriesEndpoint.GetTest, testID));
        }

        public async Task<Test> GetFullTestByID(int testID)
        {
            return await restHttpClient.Get<Test>(studentTestUrl, string.Format(RepositoriesEndpoint.GetTest, testID));
        }

        public async Task<bool> AddTestResults(TestResults testResults)
        {
            return await restHttpClient.Post<TestResults, bool>(studentTestUrl, $"{RepositoriesEndpoint.AddTestResults}", testResults);
        }

        public async Task<List<TestResults>> GetTestsResults()
        {
            return await restHttpClient.Get<List<TestResults>>(studentTestUrl, RepositoriesEndpoint.GetTestResults);
        }

        public async Task<bool> UpdateTest(Test test)
        {
            return await restHttpClient.Put<Test>(testApiUrl, string.Format(RepositoriesEndpoint.UpdateTest), test);
        }
    }
}
