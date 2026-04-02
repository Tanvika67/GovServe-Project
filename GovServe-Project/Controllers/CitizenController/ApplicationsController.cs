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
using Microsoft.AspNetCore.Authorization;
using GovServe_Project.Models.CitizenModels;


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

		// Create Application
		[HttpPost("create")]
		//[Authorize(Roles = "Citizen")]
		public async Task<IActionResult> CreateApplication(CreateApplicationDTO dto)
		{

			var result = await _applicationService.CreateApplicationAsync(dto);

			return Ok(result);
		}

		// My Applications
		[HttpGet("my")]
		//[Authorize(Roles = "Citizen")]
		public async Task<IActionResult> MyApplications(int userId)
		{

			var applications = await _applicationService.GetMyApplicationsAsync(userId);

			return Ok(applications);
		}

		
		// Application Status
		[HttpGet("status/{id}")]
		//[Authorize(Roles = "Citizen")]
		public async Task<IActionResult> ApplicationStatus(int ApplicationId)
		{

			var status = await _applicationService.GetApplicationStatusAsync(ApplicationId);

			if (status == null)
				return NotFound("Application not found");

			return Ok(status);
		}

        // Get All Applications (Admin / Officer)
        [HttpGet("all")]
        //[Authorize(Roles = "Admin,Officer")]
        public async Task<IActionResult> GetAllApplications()
        {
            var applications = await _applicationService.GetAllApplicationsAsync();
            return Ok(applications);
        }

        //  Delete Application
        [HttpDelete("delete/{id}")]
		//[Authorize(Roles = "Citizen")]
		public async Task<IActionResult> DeleteApplication(int ApplicationId)
		{

			var result = await _applicationService.DeleteApplicationAsync(ApplicationId);

			if (!result)
				return NotFound("Application not found");

			return Ok("Application Deleted Successfully");
		}


		// Update Application 
		[HttpPut("{id}")]
		[Authorize(Roles = "Citizen")]
		public async Task<IActionResult> UpdateApplication(int id, Application application)
		{
			var result = await _applicationService.UpdateApplicationAsync(id, application);

			if (!result)
			{
				return NotFound("Application not found");
			}

			return Ok("Application updated successfully");
		}

	}
}
