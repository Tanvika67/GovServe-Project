using GovServe_Project.DTOs;
using GovServe_Project.DTOs;
using GovServe_Project.Models;
using GovServe_Project.Services.Interfaces;
using GovServe_Project.Services.Service_Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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


		// Get User Profile
		[HttpGet("{id}")]
		public async Task<IActionResult> GetUser(int id)
		{
			var data = await _service.GetUserProfile(id);

			if (data == null)
				return NotFound();

			return Ok(data);
		}

		// Get All Users 

		[HttpGet("all")]
		public async Task<IActionResult> GetAllUsers()
		{
			var data = await _service.GetAllUsers();
			return Ok(data);
		}

		// Update User
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateUser(int id, Users model)
		{
			var result = await _service.UpdateUser(id, model);

			if (!result)
				return NotFound();

			return Ok("User Updated");
		}

		// Delete User
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteUser(int id)
		{
			var result = await _service.DeleteUser(id);

			if (!result)
				return NotFound();

			return Ok("User Deleted");
		}

		//Get Pending Users
		[HttpGet("pending-users")]
		public async Task<IActionResult> GetPendingUsers()
		{
			var users = await _service.GetPendingUsers();
			return Ok(users);
		}

		// Approve User
		[HttpPut("approve/{id}")]
		public async Task<IActionResult> ApproveUser(int id)
		{
			var result = await _service.ApproveUser(id);
			return Ok(result);
		}

		// Reject User
		[HttpPut("reject/{id}")]
		public async Task<IActionResult> RejectUser(int id)
		{
			var result = await _service.RejectUser(id);
			return Ok(result);
		}
	}
}
