using System.Threading;
using System.Threading.Tasks;
using GovServe.Models;
using GovServe_Project.DTOs;
using GovServe_Project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GovServe_Project.Controllers
{
	// API endpoints for Grievance module
	[Route("api/[controller]")]
	[ApiController]
	public class GrievanceController : ControllerBase
	{
		private readonly IGrievanceService _service;

		public GrievanceController(IGrievanceService service)
		{
			_service = service;
		}

		// Citizen raises grievance
		[HttpPost]
		public async Task<IActionResult> Create(GrievanceCreateDTO dto)
		{
			await _service.CreateGrievanceAsync(dto);
			return Ok("Grievance Raised Successfully");
		}

		// Citizen dashboard (remarks visible here)
		[HttpGet("my/{citizenId}")]
		public async Task<IActionResult> My(int citizenId)
		{
			return Ok(await _service.GetMyGrievancesAsync(citizenId));
		}

		// Officer pending list
		[HttpGet("pending")]
		public async Task<IActionResult> Pending()
		{
			return Ok(await _service.GetPendingGrievancesAsync());
		}

		// Supervisor forwarded list
		[HttpGet("forwarded")]
		public async Task<IActionResult> Forwarded()
		{
			return Ok(await _service.GetForwardedGrievancesAsync());
		}

		// All list
		[HttpGet("all")]
		public async Task<IActionResult> All()
		{
			return Ok(await _service.GetAllGrievancesAsync());
		}

		// Dashboard counts
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

		// Resolve grievance
		[HttpPut("{id}/resolve")]
		public async Task<IActionResult> Resolve(int id, GrievanceActionDTO dto)
		{
			await _service.ResolveAsync(id, dto.Remarks);
			return Ok("Resolved Successfully");
		}

		// Reject grie vance
		[HttpPut("{id}/reject")]
		public async Task<IActionResult> Reject(int id, GrievanceActionDTO dto)
		{
			await _service.RejectAsync(id, dto.Remarks);
			return Ok("Rejected Successfully");
		}

		// Forward grievance
		[HttpPut("{id}/forward")]
		public async Task<IActionResult> Forward(int id, GrievanceActionDTO dto)
		{
			await _service.ForwardAsync(id, dto.Remarks);
			return Ok("Forwarded To Supervisor");
		}
	}
	}
