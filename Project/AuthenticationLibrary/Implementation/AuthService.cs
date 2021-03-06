﻿using AuthenticationLibrary.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Model.Dto;
using Model.Repositories;
using Newtonsoft.Json;
using Services;
using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace AuthenticationLibrary.Implementation
{
    public class AuthService : IAuthService
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IMapper mapper;
        private readonly IService service;
        private readonly string key;
        private readonly int tokenValabilityMins;
        private const string tokenStartsWith = "Basic ";

        public AuthService(IService service, IMapper mapper)
        {
            this.mapper = mapper;
            this.service = service;
            key = ConfigurationManager.AppSettings.Get("SecretKey");
            tokenValabilityMins = Convert.ToInt32(ConfigurationManager.AppSettings.Get("TokenValability"));
        }

        public async Task<string> GetToken(HttpRequest request)
        {
            string token = " ";

            try
            {
                var username = request.Headers["username"].ToString().Trim(new char[] { '{', '}' });
                var hashedPassword = request.Headers["password"].ToString().Trim(new char[] { '{', '}' });
                var oldPassword = request.Headers["oldpassword"].ToString().Trim(new char[] { '{', '}' });

                _log.Info("Get the token for the user: " + username);

                var user = await service.GetUserByUsername(username);

                if (user != null)
                {
                    if ((String.IsNullOrEmpty(oldPassword) == false) && oldPassword.Equals(user.Password) && user.IsActive == true)
                    {
                        user.Password = hashedPassword;
                        await service.UpdateUser(user, user.UserID);
                        token = GenerateToken(user);
                    }
                    else if (String.IsNullOrEmpty(oldPassword) && hashedPassword.Equals(user.Password) && user.IsActive == true)
                    {
                        token = GenerateToken(user);
                    }
                }
            }
            catch (Exception e)
            {
                _log.Error("GetToken error: ", e);
            }

            return token;
        }

        private string GenerateToken(User user)
        {
            var tokenString = string.Empty;

            try
            {
                Token token = mapper.Map<User, Token>(user);
                token.ExpiresAt = DateTime.Now.AddMinutes(tokenValabilityMins);
                string jsonToken = JsonConvert.SerializeObject(token);

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

                var header = new JwtHeader(credentials);

                var payload = new JwtPayload
                {
                    {"content", token }
                };

                var secToken = new JwtSecurityToken(header, payload);

                var handler = new JwtSecurityTokenHandler();

                tokenString = handler.WriteToken(secToken);
            }
            catch (Exception e)
            {
                _log.Error("GenerateToken error: ", e);
            }

            return tokenString;
        }

        public async Task<Teacher> ValidateTeacher(HttpRequest request)
        {
            Teacher teacher = null;

            try
            {
                var handler = new JwtSecurityTokenHandler();
                string content = String.Empty;
                Token tokenObj = new Token();
                var authorizationHeader = request.Headers["Authorization"].ToString();
                string token = String.Empty;

                if (!String.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith(tokenStartsWith))
                {
                    token = authorizationHeader.Replace(tokenStartsWith, String.Empty);

                    handler = new JwtSecurityTokenHandler();
                    var tokenObject = handler.ReadJwtToken(token);

                    content = tokenObject.Payload.First().Value.ToString();

                    tokenObj = JsonConvert.DeserializeObject<Token>(content);

                    if (tokenObj.ExpiresAt > DateTime.Now && tokenObj.Role.Equals("teacher"))
                    {
                        var teachers = await service.GetTeachers();

                        teacher = teachers.FirstOrDefault(t => t.UserID == tokenObj.UserID);
                    }
                }

                _log.Info("Validate the teacher: " + teacher.FirstName + " " + teacher.LastName);
            }
            catch (Exception e)
            {
                _log.Error("ValidateTeacher error: ", e);
            }

            return teacher;
        }

        public async Task<Student> ValidateStudent(HttpRequest request)
        {
            Student student = null;

            try
            {
                var handler = new JwtSecurityTokenHandler();
                string content = String.Empty;
                Token tokenObj = new Token();
                var authorizationHeader = request.Headers["Authorization"].ToString();
                string token = String.Empty;

                if (!String.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith(tokenStartsWith))
                {
                    token = authorizationHeader.Replace(tokenStartsWith, String.Empty);

                    handler = new JwtSecurityTokenHandler();
                    var tokenObject = handler.ReadJwtToken(token);

                    content = tokenObject.Payload.FirstOrDefault().Value.ToString();

                    tokenObj = JsonConvert.DeserializeObject<Token>(content);

                    if (tokenObj.ExpiresAt > DateTime.Now && tokenObj.Role.Equals("student"))
                    {
                        var students = await service.GetStudents();

                        student = students.First(t => t.UserID == tokenObj.UserID);
                    }
                }

                _log.Info("Validate the student: " + student.FirstName + " " + student.LastName);
            }
            catch (Exception e)
            {
                _log.Error("ValidateStudent error: ", e);
            }

            return student;
        }

        public async Task<User> ValidateAdmin(HttpRequest request)
        {
            var handler = new JwtSecurityTokenHandler();
            string content = String.Empty;
            Token tokenObj = new Token();
            var authorizationHeader = request.Headers["Authorization"].ToString();
            string token = String.Empty;
            User admin = null;

            if (!String.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith(tokenStartsWith))
            {
                token = authorizationHeader.Replace(tokenStartsWith, String.Empty);

                handler = new JwtSecurityTokenHandler();
                var tokenObject = handler.ReadJwtToken(token);

                content = tokenObject.Payload.FirstOrDefault().Value.ToString();

                tokenObj = JsonConvert.DeserializeObject<Token>(content);

                if (tokenObj.ExpiresAt > DateTime.Now && tokenObj.Role.Equals("admin"))
                {
                    admin = await service.GetUserByUsername(tokenObj.Username);
                }
            }

            return admin;
        }
    }
}