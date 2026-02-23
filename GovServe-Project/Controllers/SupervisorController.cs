using GovServe_Project.Models;
using GovServe_Project.Repository.Interface;
using GovServe_Project.Services.Interfaces;
using GovServe_Project.Services.Service_Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GovServe_Project.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SupervisorController : ControllerBase
	{
		private readonly ISupervisorService _service;

		public SupervisorController(ISupervisorService service)
		{
			_service = service;
		}

		// GET: api/Supervisor/unassigned
		[HttpGet("unassigned")]
		public ActionResult<List<Case>> GetUnassignedCases()
		{
			return Ok(_service.GetUnassignedCases());
		}

		// GET: api/Supervisor/sla-breach
		[HttpGet("sla-breach")]
		public ActionResult<List<Case>> GetSlaBreachedCases()
		{
			return Ok(_service.GetSlaBreachedCases());
		}

		// POST: api/Supervisor/assign
		[HttpPost("assign")]
		public IActionResult AssignOfficer(int caseId, int officerId)
		{
			var result = _service.AssignOfficer(caseId, officerId);
			return Ok(result);
		}

		// POST: api/Supervisor/reassign
		[HttpPost("reassign")]
		public ActionResult ReassignOfficer(int caseId,int newOfficerId,string reason)
		{
			var result = _service.ReassignOfficer(caseId,newOfficerId,reason);
			return Ok(result);
		}

		// GET: api/Supervisor/escalations
		[HttpGet("escalations/{caseId}")]
		public ActionResult<List<Escalation>> GetEscalations(int caseId)
		{
			return Ok(_service.GetEscalations(caseId));
		}

		// GET: api/Supervisor/notifications
		[HttpGet("notifications/{userId}")]
		public ActionResult<List<Notification>> GetNotifications(int userId)
		{
			return Ok(_service.GetNotifications(userId));
		}

		// PUT: api/Supervisor/notification
		[HttpPut("notification/{notificationId}")]
		public ActionResult MarkAsRead(int notificationId)
		{
			var result = _service.MarkNotificationRead(notificationId);
			return Ok(result);
		}
	}
}

