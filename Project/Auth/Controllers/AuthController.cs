using System.Threading.Tasks;
using AuthenticationLibrary.Interfaces;
using log4net;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class AuthController : Controller
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        // GET: api/Auth
        [HttpGet]
        public async Task<string> Login()
        {
            var request = Request;
            _log.Info("Started the login...");

            return await authService.GetToken(request);
        }
    }
}