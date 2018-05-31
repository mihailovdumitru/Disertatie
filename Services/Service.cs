using Model.Test;
using Services.Infrastructure;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using Services.Endpoints;
using Model.Repositories;
using System.Collections.Generic;

namespace Services
{
    public class Service: IService
    {
        private readonly IRestHttpClient restHttpClient;
        private readonly string testApiUrl;
        private readonly string apiUrl;

        public Service(IRestHttpClient restHttpClient)
        {
            this.restHttpClient = restHttpClient;
            testApiUrl = ConfigurationManager.AppSettings.Get("TestApiUrl");
            apiUrl = ConfigurationManager.AppSettings.Get("ApiUrl");
        }

        public async Task<ActionResult> AddTest(Test test)
        {
            return await restHttpClient.Post<Test, ActionResult>(testApiUrl, $"{TestEndpoint.AddTest}", test);
        }

        public async Task<int> AddTeacher(Teacher teacher)
        {
            return await restHttpClient.Post<Teacher, int>(apiUrl, $"{RepositoriesEndpoint.AddTeacher}", teacher);
        }

        public async Task<int> AddClass(StudyClass studyClass)
        {
            return await restHttpClient.Post<StudyClass, int>(apiUrl, $"{RepositoriesEndpoint.AddClass}", studyClass);
        }

        public async Task<List<StudyClass>> GetClasses()
        {
            return await restHttpClient.Get<List<StudyClass>>(apiUrl, $"{RepositoriesEndpoint.GetClasses}");
        }

        public async Task<int> AddLecture(Lecture lecture)
        {
            return await restHttpClient.Post<Lecture, int>(apiUrl, $"{RepositoriesEndpoint.AddLecture}", lecture);
        }

        public async Task<int> AddStudent(Student student)
        {
            return await restHttpClient.Post<Student, int>(apiUrl, $"{RepositoriesEndpoint.AddStudent}", student);
        }
    }
}
