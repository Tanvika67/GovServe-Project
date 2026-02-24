using GovServe_Project.Repository.Interface;
using GovServe_Project.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GovServe_Project.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class EscalationController : ControllerBase
	{
		private readonly IEscalationService _service;

		public EscalationController(IEscalationService service)
		{
			_service = service;
		}

		[HttpPost("escalate")]
		public async Task<IActionResult> Escalate(int caseId, int newOfficerId, int supervisorId, string reason)
		{
			return Ok(await _service.EscalateCaseAsync(caseId, newOfficerId, supervisorId, reason));
		}

		[HttpGet("count")]
		public async Task<IActionResult> Count()
		{
			return Ok(await _service.GetEscalationCountAsync());
		}
	}
}
