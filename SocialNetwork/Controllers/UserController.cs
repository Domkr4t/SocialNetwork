using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Domain.Filters;
using SocialNetwork.Domain.ViewModels;
using SocialNetwork.Service.Interfaces;

namespace SocialNetwork.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) =>
            _userService = userService;

        public IActionResult Index()
        {
            return View();
        }

        [Route("/User/GetUserAccount")]
        [HttpGet]
        public async Task<IActionResult> GetUserAccount([FromQuery] int userId)
        {
            var response = await _userService.GetUserAccountInformation(userId);

            if (response.StatusCode == Domain.Enum.StatusCode.Ok)
            {
                return Ok(new { description = response.Description, data = response.Data });
            }

            return BadRequest(new { description = response.Description });
        }

        [Route("/User/GetAllMessages")]
        [HttpGet]
        public async Task<IActionResult> GetAllMessages(MessageFilter filter)
        {
            var response = await _userService.GetAllReceivedMessages(filter);

            return Json(new { data = response.Data });
        }

        [Route("/User/GetMessage")]
        [HttpGet]
        public async Task<IActionResult> GetMessage(int messageId)
        {
            var response = await _userService.GetOneMessage(messageId);

            if (response.StatusCode == Domain.Enum.StatusCode.Ok)
            {
                return Ok(new { description = response.Description, data = response.Data });
            }

            return BadRequest(new { description = response.Description });
        }

        [Route("/User/SendMessageToUser")]
        [HttpPost]
        public async Task<IActionResult> SendMessageToUser([FromBody] SendMessageViewModel model)
        {
            var response = await _userService.SendMessage(model);

            if (response.StatusCode == Domain.Enum.StatusCode.Ok)
            {
                return Ok(new { description = response.Description });
            }

            return BadRequest(new { description = response.Description });
        }

        [Route("/User/IsReadMessage")]
        [HttpPatch]
        public async Task<IActionResult> IsReadMessage([FromQuery] int messageId)
        {
            var response = await _userService.SetIsReadMessage(messageId);

            if (response.StatusCode == Domain.Enum.StatusCode.Ok)
            {
                return Ok(new { description = response.Description });
            }

            return BadRequest(new { description = response.Description });
        }
    }
}