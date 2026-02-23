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
	[Route("api/[controller]")]
	[ApiController]
	public class ApplicationController : ControllerBase
	{
		private readonly IApplicationService _applicationService;


		public ApplicationController(IApplicationService applicationService)
		{
			_applicationService = applicationService;
		}


		//Create Application

		[HttpPost("create")]
		public async Task<IActionResult> CreateApplication(CreateApplicationDTO dto)
		{


			var result = await _applicationService.CreateApplicationAsync(dto);


			return Ok(result);
		}


		// My Applications (Citizen Dashboard)

		[HttpGet("my")]
		public async Task<IActionResult> MyApplications(int userId)
		{


			var applications = await _applicationService.GetMyApplicationsAsync(userId);

			return Ok(applications);
		}


		// Application Status

		[HttpGet("status/{id}")]
		public async Task<IActionResult> ApplicationStatus(int id)
		{


			var status = await _applicationService.GetApplicationStatusAsync(id);

			if (status == null)
				return NotFound("Application not found");

			return Ok(status);
		}


		// Delete Application

		[HttpDelete("delete/{id}")]
		public async Task<IActionResult> DeleteApplication(int id)
		{


			var result = await _applicationService.DeleteApplicationAsync(id);

			if (!result)
				return NotFound("Application not found");

			return Ok("Application Deleted Successfully");
		}
	}
}
