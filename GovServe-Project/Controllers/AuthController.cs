using GovServe_Project.Auth;
using GovServe_Project.DTOs;
using GovServe_Project.Models.AdminModels;
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

		[HttpPost("ForgotPassword")]
		public async Task<IActionResult> ForgotPassword(LoginDTO model)
		{
			var result = await _authService.ForgotPassword(model);

			if (!result)
			{
				return BadRequest("Email not found");
			}

			return Ok("Password reset successfully");
		}

		[HttpPost("Logout")]
		public async Task<IActionResult> Logout()
		{
			await _authService.Logout();
			return Ok("Logged out successfully");
		}
	}
}
