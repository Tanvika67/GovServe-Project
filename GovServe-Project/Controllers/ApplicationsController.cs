using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GovServe_Project.Data;
using GovServe_Project.Models;
using GovServe_Project.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GovServe_Project.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ApplicationsController : ControllerBase
	{
		private readonly IApplicationService _service;

		public ApplicationsController(IApplicationService service)
		{
			_service = service;
		}

		// Create Application
		[HttpPost]
		public async Task<IActionResult> CreateApplication(Application model)
		{
			await _service.CreateApplication(model);
			return Ok("Application Submitted Successfully");
		}

		// My Applications
		[HttpGet("user/{userId}")]
		public async Task<IActionResult> MyApplications(int userId)
		{
			var data = await _service.MyApplications(userId);
			return Ok(data);
		}

		// Application Status
		[HttpGet("status/{id}")]
		public async Task<IActionResult> ApplicationStatus(int id)
		{
			var data = await _service.ApplicationStatus(id);

			if (data == null)
				return NotFound();

			return Ok(data);
		}

		// Delete Application
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteApplication(int id)
		{
			await _service.DeleteApplication(id);
			return Ok("Application Deleted");
		}
	}
}