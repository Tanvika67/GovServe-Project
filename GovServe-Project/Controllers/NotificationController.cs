using GovServe_Project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace GovServe_Project.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class NotificationController : ControllerBase
	{
		private readonly INotificationService _service;

		public NotificationController(INotificationService service)
		{
			_service = service;
		}

		[HttpGet("{userId}")]
		public async Task<IActionResult> Get(int userId)
		{
			return Ok(await _service.GetUserNotificationsAsync(userId));
		}

		[HttpGet("unread/{userId}")]
		public async Task<IActionResult> GetUnread(int userId)
		{
			return Ok(await _service.GetUnreadCountAsync(userId));
		}

		[HttpPut("mark-read/{notificationId}")]
		public async Task<IActionResult> MarkRead(int notificationId)
		{
			await _service.MarkAsReadAsync(notificationId);
			return Ok("Marked as read");
		}
	}
}
