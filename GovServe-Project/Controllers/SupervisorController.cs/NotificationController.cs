using GovServe_Project.DTOs.SupervisorDTO;
using GovServe_Project.Services.Interfaces;
using GovServe_Project.Services.Interfaces.SuperServiceInterface;
using GovServe_Project.Services.Service_Implementation.SuperServiceImplementation;
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

		//[Authorize(Roles = "Supervisor")]
		[HttpPost("send")]
		public async Task<IActionResult> Send(NotificationDto dto)
		{
			await _service.SendManualNotification(dto);
			return Ok("Notification sent successfully");
		}
		//Get only unread notifications
		[HttpGet("all/{userId}")]
		//[Authorize(Roles = "Supervisor")]

		public async Task<IActionResult> GetNotifications(int userId)
		{
			var notifications = await _service.GetUserNotificationsAsync(userId);
			return Ok(notifications);
		}

		[HttpGet("unread/{userId}")]
		public async Task<IActionResult> GetUnread(int userId)
		{
			var data = await _service.GetUnreadNotifications(userId);
			return Ok(data);
		}

		//Mark notification as read when I see the message
		[HttpPut("mark-read/{notificationId}")]
		//[Authorize(Roles = "Supervisor")]
		public async Task<IActionResult> MarkRead(int notificationId)
		{
			await _service.MarkAsReadAsync(notificationId);
			return Ok("Marked as read");
		}

        [HttpGet("read/{userId}")]
        public async Task<IActionResult> GetRead(int userId)
        {
            var data = await _service.GetReadNotifications(userId);
            return Ok(data);
        }
    }
}
