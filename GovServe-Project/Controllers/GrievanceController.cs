using GovServe.Models;
using GovServe_Project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace GovServe_Project.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class GrievanceController : ControllerBase
	{
		private readonly IGrievanceService _service;
		public GrievanceController(IGrievanceService service) => _service = service;

		// Citizen raises grievance
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] Grievance grievance)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);

			var result = await _service.CreateGrievanceAsync(grievance);
			return Ok(new { message = "Grievance raised successfully.", data = result });
		}

		// Officer/Supervisor resolves grievance
		[HttpPut("{id}/resolve")]
		public async Task<IActionResult> Resolve(int id, [FromBody] string remarks)
		{
			if (string.IsNullOrWhiteSpace(remarks))
				return BadRequest(new { message = "Remarks are required to resolve grievance." });

			await _service.ResolveGrievanceAsync(id, remarks);
			return Ok(new { message = "Grievance resolved successfully." });
		}

		// Forward grievance to supervisor
		[HttpPut("{id}/forward/{supervisorId}")]
		public async Task<IActionResult> Forward(int id, int supervisorId)
		{
			await _service.ForwardToSupervisorAsync(id, supervisorId);
			return Ok(new { message = "Grievance forwarded to supervisor successfully." });
		}

		// Supervisor rejects grievance
		[HttpPut("{id}/reject")]
		public async Task<IActionResult> Reject(int id, [FromBody] string remarks)
		{
			if (string.IsNullOrWhiteSpace(remarks))
				return BadRequest(new { message = "Remarks are required to reject grievance." });

			await _service.RejectGrievanceAsync(id, remarks);
			return Ok(new { message = "Grievance rejected successfully." });
		}
	}
}
