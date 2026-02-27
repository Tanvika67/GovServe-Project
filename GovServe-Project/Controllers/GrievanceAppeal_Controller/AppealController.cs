using System.Threading;
using System.Threading.Tasks;
using GovServe.Models;
using GovServe_Project.DTOs;
using GovServe_Project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GovServe_Project.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AppealController : ControllerBase
	{
		private readonly IAppealService _service;

		public AppealController(IAppealService service)
		{
			_service = service;
		}

		// Citizen files appeal
		[HttpPost]
		public async Task<IActionResult> Create(AppealCreateDTO dto)
		{
			await _service.CreateAppealAsync(dto);
			return Ok("Appeal Filed Successfully");
		}

		// Citizen dashboard
		[HttpGet("my/{citizenId}")]
		public async Task<IActionResult> My(int citizenId)
		{
			return Ok(await _service.GetMyAppealsAsync(citizenId));
		}

		// Admin list
		[HttpGet("all")]
		public async Task<IActionResult> All()
		{
			return Ok(await _service.GetAllAppealsAsync());
		}

		[HttpGet("active-count")]
		public async Task<IActionResult> Active()
		{
			return Ok(await _service.GetActiveCountAsync());
		}

		[HttpGet("inactive-count")]
		public async Task<IActionResult> Inactive()
		{
			return Ok(await _service.GetInactiveCountAsync());
		}

		[HttpPut("{id}/approve")]
		public async Task<IActionResult> Approve(int id, AppealActionDTO dto)
		{
			await _service.ApproveAsync(id, dto.Remarks);
			return Ok("Appeal Approved");
		}

		[HttpPut("{id}/reject")]
		public async Task<IActionResult> Reject(int id, AppealActionDTO dto)
		{
			await _service.RejectAsync(id, dto.Remarks);
			return Ok("Appeal Rejected");
		}
	}
}