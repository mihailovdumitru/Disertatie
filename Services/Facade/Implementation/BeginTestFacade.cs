using log4net;
using Microsoft.AspNetCore.Mvc;
using Model.Dto;
using Model.Repositories;
using Services.Facade.Interfaces;
using Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services.Facade.Implementation
{
    public class BeginTestFacade : IBeginTestFacade
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IService service;
        private readonly IFileGenerator fileGenerator;
        private Random random = new Random();
        private readonly int passwordSize;
        private readonly HashAlgorithm sha;
        private const string TeacherRole = "teacher";
        private const string StudentRole = "student";

        public BeginTestFacade(IService service, IFileGenerator fileGenerator)
        {
            this.service = service;
            this.fileGenerator = fileGenerator;
            passwordSize = Convert.ToInt32(ConfigurationManager.AppSettings.Get("HashSize"));
            sha = new SHA1CryptoServiceProvider();
        }

        public async Task<IEnumerable<Lecture>> GetTeachersLectures(int teacherID)
        {
            var teacher = await service.GetTeacher(teacherID);
            var lectures = await service.GetLectures();

            return lectures.Where(lecture => teacher.Lectures.Contains(lecture.LectureID));
        }

        public async Task<ContentResult> GenerateHashCodes(int classID)
        {
            var students = await service.GetStudents();
            var classStudents = students.Where(student => student.ClassID == classID).ToList<Student>();
            User user = null;
            List<User> users = new List<User>();
            List<StudentDataForFile> studentsCredentialsForFile = new List<StudentDataForFile>();
            List<User> hashedList = null;

            if (classStudents != null)
            {
                foreach (var student in classStudents)
                {
                    user = new User
                    {
                        UserID = student.UserID,
                        Username = student.Email,
                        IsActive = true,
                        Role = StudentRole
                    };
                    users.Add(user);
                }
            }

            GeneratePasswordsForUsersList(users);

            for (int i = 0; i < users.Count; i++)
            {
                studentsCredentialsForFile.Add(new StudentDataForFile
                {
                    FirstName = classStudents[i].FirstName,
                    LastName = classStudents[i].LastName,
                    Email = classStudents[i].Email,
                    Password = users[i].Password
                });
            }

            var fileContent = fileGenerator.GenerateFile<StudentDataForFile>(studentsCredentialsForFile);

            hashedList = new List<User>(users);
            GenerateHashForPasswords(hashedList);

            foreach (var userWithHashedPwd in hashedList)
            {
                await service.UpdateUser(userWithHashedPwd, userWithHashedPwd.UserID);
            }

            return fileContent;
        }

        public List<User> GeneratePasswordsForUsersList(List<User> users)
        {
            try
            {
                if (users != null)
                {
                    for (int i = 0; i < users.Count; i++)
                    {
                        users[i].Password = RandomString(passwordSize);
                    }
                }
            }
            catch (Exception e)
            {
                _log.Error("GeneratePasswordsForUsersList error: ", e);
            }

            return users;
        }

        public List<User> GenerateHashForPasswords(List<User> users)
        {
            try
            {
                byte[] passwordByteArray = null;
                byte[] hashedPasswordByteArray = null;

                if (users != null)
                {
                    for (int i = 0; i < users.Count; i++)
                    {
                        passwordByteArray = Encoding.ASCII.GetBytes(users[i].Password);
                        hashedPasswordByteArray = sha.ComputeHash(passwordByteArray);
                        users[i].Password = BitConverter.ToString(hashedPasswordByteArray).Replace("-", string.Empty).ToLower();
                    }
                }
            }
            catch (Exception e)
            {
                _log.Error("GenerateHashForPasswords error: ", e);
            }

            return users;
        }

        private string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}