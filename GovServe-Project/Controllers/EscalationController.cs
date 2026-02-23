using GovServe_Project.Repository.Interface;
using GovServe_Project.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace GovServe_Project.Controllers
{
	[ApiController]
	[Route("api/escalation")]
	public class EscalationController : ControllerBase
	{
		private readonly IEscalationRepository _repo;

		public EscalationController(IEscalationRepository repo)
		{
			_repo = repo;
		}

		[HttpGet("count")]
		public async Task<IActionResult> Count()
		{
			return Ok(await _repo.GetCountAsync());
		}
	}
}
