using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Domain.ViewModels;
using SocialNetwork.Service.Interfaces;

namespace SocialNetwork.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) =>
            _authService = authService;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AuthentificationPage()
        {
            return View();
        }

        public IActionResult RegistrationPage()
        {
            return View();
        }

        [Route("/Auth/Authentification")]
        [HttpGet]
        public async Task<IActionResult> Authentification(AuthViewModel model)
        {
            var response = await _authService.AuthentificateUser(model);

            if (response.StatusCode == Domain.Enum.StatusCode.Ok)
            {
                return Ok(new { description = response.Description, data = response.Data });
            }

            return BadRequest(new { description = response.Description });
        }

        [Route("/Auth/Registration")]
        [HttpPost]
        public async Task<IActionResult> Registration([FromBody] RegistrationViewModel model)
        {
            var response = await _authService.RegistrateUser(model);

            if (response.StatusCode == Domain.Enum.StatusCode.Ok)
            {
                return Ok(new { description = response.Description, data = response.Data });
            }

            return BadRequest(new { description = response.Description });
        }
    }
}