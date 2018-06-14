using Microsoft.AspNetCore.Http;
using Model.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationLibrary.Interfaces
{
    public interface IAuthService
    {
        Task<string> GetToken(HttpRequest request);
        Task<Teacher> ValidateTeacher(HttpRequest request);
        Task<Student> ValidateStudent(HttpRequest request);
    }
}
