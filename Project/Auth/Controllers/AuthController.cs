using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationLibrary.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class AuthController : Controller
    {
        private readonly  IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }
        // GET: api/Auth
        [HttpGet]
        public async Task<string> Login()
        {
            var request = Request;

            return await authService.GetToken(request);
        }

        // GET: api/Auth/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Auth
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Auth/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
