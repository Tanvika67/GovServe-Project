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
	public class AppealController : ControllerBase
	{
		private readonly IAppealService _service;

		// Constructor injection
		public AppealController(IAppealService service)
		{
			_service = service;
		}

		// File Appeal
		// Citizen submits appeal after application rejection
		[HttpPost]
		//[Authorize(Roles = "Citizen")]
		public async Task<IActionResult> FileAppeal([FromBody] AppealDTO dto)
		{
			await _service.FileAppealAsync(dto);
			return Ok("Appeal submitted successfully");
		}


		// My Appeals
		[HttpGet("application/{applicationId}")]
		//[Authorize(Roles = "Citizen")]
		public async Task<IActionResult> MyAppeals(int applicationId)
		{
			var data = await _service.MyAppealsAsync(applicationId);
			return Ok(data);
		}

		// Appeal Status (View Only)
		[HttpGet("status/{id}")]
		//[Authorize(Roles = "Citizen")]
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
		// Officer - View Submitted Appeals
		[HttpGet("submitted")]
		[Authorize(Roles = "Officer,Supervisor")]
		public async Task<IActionResult> SubmittedAppeals()
		{
			var data = await _service.GetAllSubmittedAppealsAsync();
			return Ok(data);
		}

		// Officer - Approve Appeal
		[HttpPut("approve")]
		[Authorize(Roles = "Officer,Supervisor")]
		public async Task<IActionResult> ApproveAppeal(AppealActionDTO dto)
		{
			await _service.ApproveAppealAsync(dto);
			return Ok("Appeal Approved Successfully");
		}

		// Officer - Reject Appeal
		[HttpPut("reject")]
		[Authorize(Roles = "Officer,Supervisor")]
		public async Task<IActionResult> RejectAppeal(AppealActionDTO dto)
		{
			await _service.RejectAppealAsync(dto);
			return Ok("Appeal Rejected Successfully");
		}

		[HttpGet("count/pending")]
		[Authorize(Roles = "Officer,Supervisor")]
		public async Task<IActionResult> GetPendingAppeals()
		{
			var count = await _service.GetPendingAppealCountAsync();
			return Ok(new { PendingAppeals = count });

		}
		[HttpGet("count/Resolve")]
		[Authorize(Roles = "Officer,Supervisor")]
		public async Task<IActionResult> GetResolvedAppeals()
		{
			var count = await _service.GetResolvedAppealCountAsync();
			return Ok(new { PendingAppeals = count });
		}

	}
}
