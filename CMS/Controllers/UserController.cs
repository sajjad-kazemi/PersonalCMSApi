using Microsoft.AspNetCore.Mvc;
using Models.User;
using Services;

namespace CMS.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;
		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpPost]
		public async Task<IActionResult> GetCurrentUser()
		{
			var response = await _userService.GetCurrentUser();
			return Ok(response);
		}

		[HttpPost]
		public async Task<IActionResult> GetUser(GetUserRequest request)
		{
			var response = await _userService.GetUser(request);
			return Ok(response);
		}
	}
}
