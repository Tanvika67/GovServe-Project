using GovServe_Project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GovServe_Project.Controllers
{
	[ApiController]
	[Route("api/notification")]
	public class NotificationController : ControllerBase
	{
		// WRITE HERE (inside class, top section)
		private readonly INotificationService _service;

		//  CONSTRUCTOR (Dependency Injection)
		public NotificationController(INotificationService service)
		{
			_service = service;
		}

		// API METHOD
		[HttpGet("{userId}")]
		public async Task<IActionResult> Get(int userId)
		{
			var data = await _service.GetNotificationsAsync(userId);
			return Ok(data);
		}
	}
}

