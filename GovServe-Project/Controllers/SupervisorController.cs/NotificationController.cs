using GovServe_Project.DTOs.SupervisorDTO;
using GovServe_Project.Services.Interfaces;
using GovServe_Project.Services.Interfaces.SuperServiceInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace GovServe_Project.Controllers.SupervisorController.cs
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

		// Get all notifications from the user
		[HttpGet("{userId}")]
		//[Authorize(Roles = "Supervisor")]
		public async Task<IActionResult> Get(int userId)
		{
			return Ok(await _service.GetUserNotificationsAsync(userId));
		}

		//Get only unread notifications
		[HttpGet("unread/{userId}")]
		//[Authorize(Roles = "Supervisor")]

		public async Task<IActionResult> GetUnread(int userId)
		{
			return Ok(await _service.GetUnreadCountAsync(userId));
		}

		//Mark notification as read when I see the message
		[HttpPut("mark-read/{notificationId}")]
		//[Authorize(Roles = "Supervisor")]
		public async Task<IActionResult> MarkRead(int notificationId)
		{
			await _service.MarkAsReadAsync(notificationId);
			return Ok("Marked as read");
		}
	}
}
