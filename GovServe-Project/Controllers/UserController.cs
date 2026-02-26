using GovServe_Project.DTOs;
using GovServe_Project.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GovServe_Project.DTOs;

namespace GovServe_Project.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserService _service;

		public UserController(IUserService service)
		{
			_service = service;
		}

		// Register

		[HttpPost("register")]
		public async Task<IActionResult> Register(RegisterDTO dto)
		{
			var result = await _service.RegisterAsync(dto);

			return Ok(result);
		}


		// Login

		[HttpPost("login")]
		public async Task<IActionResult> Login(LoginDTO dto)
		{
			var result = await _service.LoginAsync(dto);

			return Ok(result);
		}
	}
}
