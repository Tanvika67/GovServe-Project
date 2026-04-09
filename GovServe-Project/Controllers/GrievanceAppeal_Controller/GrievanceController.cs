using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GovServe_Project.Data;
using GovServe_Project.DTOs;
using Microsoft.AspNetCore.Authorization;
using GovServe_Project.Models;
using GovServe_Project.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GovServe_Project.Controllers
{
	
	[Route("api/[controller]")]
	[ApiController]
	public class GrievanceController : ControllerBase
	{
		private readonly IGrievanceService _service;

	
		public GrievanceController(IGrievanceService service)
		{
			_service = service;
		}

		[HttpPost]
		[Authorize(Roles = "Citizen")]
		public async Task<IActionResult> RaiseGrievance([FromBody] RaiseGrievanceDTO dto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			await _service.RaiseGrievanceAsync(dto);
			return Ok("Grievance submitted successfully");
		}


		// View My Grievances
		[HttpGet("user/{citizenId}")]
		[Authorize(Roles = "Citizen")]
		public async Task<IActionResult> MyGrievances(int citizenId)
		{
			var data = await _service.MyGrievancesAsync(citizenId);
			return Ok(data);
		}

		// View Grievance Details / Status by GrievanceId
		[HttpGet("status/{grievanceId}")]
		[Authorize(Roles = "Citizen")]
		public async Task<IActionResult> GrievanceStatus(int grievanceId)
		{
			var data = await _service.GrievanceStatusAsync(grievanceId);

			if (data == null)
				return NotFound("Grievance not found");

			return Ok(data);
		}


		// OFFICER ACTIONS


		// View all grievances 
		[HttpGet("all")]
		[Authorize(Roles = "Officer")]
		public async Task<IActionResult> GetAllGrievances()
		{
			var data = await _service.GetAllGrievancesAsync();
			var result=data.Select(g => new
			{
				g.GrievanceId,
				g.ApplicationID,
				g.UserId,
				g.Reason,
				g.Description,
				g.FiledDate,
				g.Status,
				g.Remarks
			}).ToList();	
			return Ok(data);
		}

		// Resolve grievance
		[HttpPut("resolve/{id}")]
		[Authorize(Roles = "Officer")]
		public async Task<IActionResult> ResolveGrievance(int id, GrievanceActionDTO dto)
		{
			dto.GrievanceId = id;
			await _service.ResolveGrievanceAsync(dto);
			return Ok("Grievance resolved successfully.");
		}

		// Reject grievance
		[HttpPut("reject/{id}")]
		[Authorize(Roles = "Officer")]
		public async Task<IActionResult> RejectGrievance(int id, GrievanceActionDTO dto)
		{
			dto.GrievanceId = id;
			await _service.RejectGrievanceAsync(dto);
			return Ok("Grievance rejected successfully.");
		}

		

		[HttpGet("count/pending")]
		[Authorize(Roles = "Officer")]
		public async Task<IActionResult> GetPendingGrievanceCount()
		{
			var count = await _service.GetPendingGrievanceCountAsync();
			return Ok(new { PendingGrievanceCount = count });
		}

		[HttpGet("count/resolved")]
		[Authorize(Roles = "Officer")]
		public async Task<IActionResult> GetResolvedGrievanceCount()
		{
			var count = await _service.GetResolvedGrievanceCountAsync();
			return Ok(new { ResolvedGrievanceCount = count });
		}
	}

}