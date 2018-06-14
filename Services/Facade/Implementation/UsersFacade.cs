using Model.Repositories;
using Services.Facade.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services.Facade.Implementation
{
    public class UsersFacade:IUsersFacade
    {
        private const string TeacherRole = "teacher";
        private const string StudentRole = "student";
        private readonly IService serviceRepo;
        private readonly HashAlgorithm sha;
        private readonly string defaultPassword;
        public UsersFacade(IService serviceRepo)
        {
            this.serviceRepo = serviceRepo;
            sha = new SHA1CryptoServiceProvider();
            defaultPassword = ConfigurationManager.AppSettings.Get("DefaultPassword");
        }

        public async Task<int> AddTeacherUser(Teacher teacher)
        {
            byte[] passwordByteArray = Encoding.UTF8.GetBytes(defaultPassword);
            var hashedPasswordByteArray = sha.ComputeHash(passwordByteArray);
            var password = BitConverter.ToString(hashedPasswordByteArray).Replace("-", string.Empty).ToLower();


            User teacherUser = new User
            {
                Username = teacher.Email,
                Role = TeacherRole,
                Password = password,
                IsActive = true
            };

            teacher.UserID = await serviceRepo.AddUser(teacherUser);

            return await serviceRepo.AddTeacher(teacher);

        }

        public async Task<int> AddStudentUser(Student student)
        {
            byte[] passwordByteArray = Encoding.ASCII.GetBytes(defaultPassword);
            var hashedPasswordByteArray = sha.ComputeHash(passwordByteArray);
            var password = BitConverter.ToString(hashedPasswordByteArray).Replace("-", string.Empty).ToLower();


            User studentUser = new User
            {
                Username = student.Email,
                Role = StudentRole,
                Password = password,
                IsActive = true
            };

            student.UserID = await serviceRepo.AddUser(studentUser);

            return await serviceRepo.AddStudent(student); 
        }
    }
}
