using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GovServe_Project.Data;
using GovServe_Project.Models;
using GovServe_Project.Models.GrievanceAppealModel;
using GovServe_Project.Repository.Repository_Implentation.GrievanceAppealRepository_implementation;
using GovServe_Project.Services.Interfaces;
using GovServe_Project.Services.Interfaces.GrievanceAppealService_Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GovServe_Project.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AppealsController : ControllerBase
	{
		private readonly IAppealService _service;

		// Constructor injection
		public AppealsController(IAppealService service)
		{
			_service = service;
		}

		// File Appeal
		// Citizen submits appeal after application rejection
		[HttpPost]
		[Authorize(Roles = "Citizen")]
		public async Task<IActionResult> FileAppeal([FromBody] Appeal appeal)
		{
			await _service.FileAppealAsync(appeal);
			return Ok("Appeal submitted successfully");
		}

		// My Appeals
		// Citizen views appeals for specific application
		[HttpGet("application/{applicationId}")]
		[Authorize(Roles = "Citizen")]
		public async Task<IActionResult> MyAppeals(int applicationId)
		{
			var data = await _service.MyAppealsAsync(applicationId);
			return Ok(data);
		}

		// Appeal Status (View Only)
		// Citizen can only view status, cannot update
		[HttpGet("status/{id}")]
		[Authorize(Roles = "Citizen")]
		public async Task<IActionResult> AppealStatus(int id)
		{
			var data = await _service.GetAppealStatusAsync(id);

			if (data == null)
				return NotFound("Appeal not found");

			return Ok(new
			{
				data.AppealID,
				data.Status
			});
		}
	}
}
