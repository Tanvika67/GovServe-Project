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
using GovServe_Project.DTOs;
using GovServe_Project.Services.Service_Implementation.CitizenService_Implementation;
using GovServe_Project.Services.Interfaces.CitizenService_Interface;
using GovServe_Project.DTOs.CitizenDTO;

namespace GovServe_Project.Controllers.CitizenController
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

		// 1. Create Application
		[HttpPost("create")]
		public async Task<IActionResult> CreateApplication(CreateApplicationDTO dto)
		{

			var result = await _applicationService.CreateApplicationAsync(dto);

			return Ok(result);
		}

		// 2. My Applications (Citizen Dashboard)
		[HttpGet("my")]
		public async Task<IActionResult> MyApplications(int userId)
		{

			var applications = await _applicationService.GetMyApplicationsAsync(userId);

			return Ok(applications);
		}

		
		// 3. Application Status
		[HttpGet("status/{id}")]
		public async Task<IActionResult> ApplicationStatus(int ApplicationId)
		{

			var status = await _applicationService.GetApplicationStatusAsync(ApplicationId);

			if (status == null)
				return NotFound("Application not found");

			return Ok(status);
		}

		// 4. Delete Application
		[HttpDelete("delete/{id}")]
		public async Task<IActionResult> DeleteApplication(int ApplicationId)
		{

			var result = await _applicationService.DeleteApplicationAsync(ApplicationId);

			if (!result)
				return NotFound("Application not found");

			return Ok("Application Deleted Successfully");
		}
	}
}
