using GovServe_Project.DTOs.SupervisorDTO;
using GovServe_Project.Repository.Interface;
using GovServe_Project.Services.Interfaces;
using GovServe_Project.Services.Interfaces.SuperServiceInterface;
using GovServe_Project.Services.Service_Implementation.SuperServiceImplementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GovServe_Project.Controllers.SupervisorController.cs
{
	[ApiController]
	[Route("api/[controller]")]
	//[Authorize(Roles = "Supervisor")]
	public class EscalationController : ControllerBase
	{
		private readonly IEscalationService _service;

		public EscalationController(IEscalationService service)
		{
			_service = service;
		}


		//Automatically finds and escalates them without my involvement it called by background job
		[HttpPost("auto-escalate")]
		public async Task<IActionResult> AutoEscalate()
		{
			var result = await _service.AutoEscalateAsync();
			return Ok(result);
		}
		
		//Returns total no.of escalated cases useful for dashboard
		[HttpGet("count")]
		public async Task<IActionResult> Count()
		{
			return Ok(await _service.GetEscalationCountAsync());
		}
	}
}
