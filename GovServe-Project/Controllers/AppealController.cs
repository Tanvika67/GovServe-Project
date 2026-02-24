using GovServe.Models;
using GovServe_Project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace GovServe_Project.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AppealController : ControllerBase
	{
		private readonly IAppealService _service;
		public AppealController(IAppealService service) => _service = service;

		// Citizen files appeal
		[HttpPost]
		public async Task<IActionResult> File([FromBody] Appeal appeal)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);

			var result = await _service.FileAppealAsync(appeal);
			return Ok(new { message = "Appeal filed successfully.", data = result });
		}

		// Supervisor/Admin approves appeal
		[HttpPut("{id}/approve")]
		public async Task<IActionResult> Approve(int id)
		{
			await _service.ApproveAppealAsync(id);
			return Ok(new { message = "Appeal approved and grievance reopened." });
		}

		// Supervisor/Admin rejects appeal
		[HttpPut("{id}/reject")]
		public async Task<IActionResult> Reject(int id)
		{
			await _service.RejectAppealAsync(id);
			return Ok(new { message = "Appeal rejected. Final close." });
		}
	}
}