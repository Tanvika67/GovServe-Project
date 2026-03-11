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

		// My Applications (Citizen Dashboard)
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

		//  Delete Application
		[HttpDelete("delete/{id}")]
		[Authorize(Roles = "Citizen")]
		public async Task<IActionResult> DeleteApplication(int ApplicationId)
		{

			var result = await _applicationService.DeleteApplicationAsync(ApplicationId);

			if (!result)
				return NotFound("Application not found");

			return Ok("Application Deleted Successfully");
		}

		//Assigned application

		[HttpGet("assigned/{officerId}")]
		//[Authorize(Roles = "Officer")]
		public async Task<IActionResult> GetAssignedCases(int officerId) //method created
		{
			var result = await _applicationService.GetAssignedCases(officerId);
			return Ok(result);
		}

		//Approved cases

		[HttpGet("approved /{officerId}")]
		//[Authorize(Roles = "Officer")]
		public async Task<IActionResult> GetApproved(int officerId)
		{
			var result = await _applicationService.GetApprovedCases(officerId);
			return Ok(result);

		}

		//pending cases

		[HttpGet("pending /{officerId}")]
		//[Authorize(Roles = "Officer")]
		public async Task<IActionResult> GetPending(int officerId)
		{
			var result = await _applicationService.GetPendingCases(officerId);
			return Ok(result);

		}

		//rejected cases

		[HttpGet("Reject /{officerId}")]
		//[Authorize(Roles = "Officer")]
		public async Task<IActionResult> GetRejected(int officerId)
		{
			var result = await _applicationService.GetRejectedCases(officerId);
			return Ok(result);

		}
		//resubmitted cases

		[HttpGet("resubmitted /{officerId}")]
		//[Authorize(Roles = "Officer")]
		public async Task<IActionResult> GetResubmitted(int officerId)
		{
			var result = await _applicationService.GetResubmittedCases(officerId);
			return Ok(result);

		}
	}
}
