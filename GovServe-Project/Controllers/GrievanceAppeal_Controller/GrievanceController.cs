using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GovServe_Project.Data;
using GovServe_Project.DTOs;
using GovServe_Project.Models;
using GovServe_Project.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GovServe_Project.Controllers
{
	// API Controller for Citizen Grievance
	[Route("api/[controller]")]
	[ApiController]
	public class GrievanceController : ControllerBase
	{
		private readonly IGrievanceService _service;

		// Constructor injection
		public GrievanceController(IGrievanceService service)
		{
			_service = service;
		}

		// Raise Grievance (Citizen fills grievance form)
		[HttpPost]
		[HttpPost]
		public async Task<IActionResult> RaiseGrievance([FromBody] RaiseGrievanceDTO dto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			await _service.RaiseGrievanceAsync(dto);
			return Ok("Grievance submitted successfully");
		}


		// View My Grievances
		[HttpGet("user/{citizenId}")]
		public async Task<IActionResult> MyGrievances(int citizenId)
		{
			var data = await _service.MyGrievancesAsync(citizenId);
			return Ok(data);
		}

		// View Grievance Details / Status
		[HttpGet("{id}")]
		public async Task<IActionResult> GrievanceStatus(int id)
		{
			var data = await _service.GrievanceStatusAsync(id);

			if (data == null)
				return NotFound("Grievance not found");

			return Ok(data);
		}
	}

}