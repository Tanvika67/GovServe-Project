using GovServe_Project.Auth;
using GovServe_Project.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GovServe_Project.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login(LoginDTO dto)
		{
			var token = await _authService.Login(dto);

			return Ok(new
			{
				Token = token,
				Message = "Login Successful"
			});
		}
	}
}
