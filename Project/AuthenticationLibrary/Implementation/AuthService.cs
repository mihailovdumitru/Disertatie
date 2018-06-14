using AuthenticationLibrary.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Model.Dto;
using Model.Repositories;
using Newtonsoft.Json;
using Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationLibrary.Implementation
{
    public class AuthService:IAuthService
    {
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
            var username = request.Headers["username"].ToString().Trim(new char[] { '{', '}' });
            var hashedPassword = request.Headers["password"].ToString().Trim(new char[] { '{', '}' });
            var oldPassword = request.Headers["oldpassword"].ToString().Trim(new char[] { '{', '}' });

            var user = await service.GetUserByUsername(username);

            if (user != null)
            {
                if((String.IsNullOrEmpty(oldPassword) == false) && oldPassword.Equals(user.Password) && user.IsActive == true)
                {
                    user.Password = hashedPassword;
                    await service.UpdateUser(user, user.UserID);
                    token = GenerateToken(user);
                }
                else if(String.IsNullOrEmpty(oldPassword) && hashedPassword.Equals(user.Password) && user.IsActive == true)
                {
                    token = GenerateToken(user);
                }
            }

            return token;
        }


        private string GenerateToken(User user)
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

            var tokenString = handler.WriteToken(secToken);


            return tokenString;
        }

        public async Task<Teacher> ValidateTeacher(HttpRequest request)
        {
            var handler = new JwtSecurityTokenHandler();
            string content = String.Empty;
            Token tokenObj = new Token();
            var authorizationHeader = request.Headers["Authorization"].ToString();
            string token = String.Empty;
            Teacher teacher = null;

            if(!String.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith(tokenStartsWith))
            {
                token = authorizationHeader.Replace(tokenStartsWith, String.Empty);

                handler = new JwtSecurityTokenHandler();
                var tokenObject = handler.ReadJwtToken(token);

                content = tokenObject.Payload.First().Value.ToString();

                tokenObj = JsonConvert.DeserializeObject<Token>(content);

                if(tokenObj.ExpiresAt > DateTime.Now && tokenObj.Role.Equals("teacher"))
                {
                    var teachers = await service.GetTeachers();

                     teacher = teachers.FirstOrDefault(t => t.UserID == tokenObj.UserID);
                }
            }

            return teacher;
        }

        public async Task<Student> ValidateStudent(HttpRequest request)
        {
            var handler = new JwtSecurityTokenHandler();
            string content = String.Empty;
            Token tokenObj = new Token();
            var authorizationHeader = request.Headers["Authorization"].ToString();
            string token = String.Empty;
            Student student = null;

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

            return student;
        }

    }
}
