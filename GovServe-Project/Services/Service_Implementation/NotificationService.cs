using GovServe_Project.Models;
using GovServe_Project.Repository.Interface;
using GovServe_Project.Services.Interfaces;
namespace GovServe_Project.Services.Service_Implementation
{
	public class NotificationService : INotificationService
	{
		private readonly INotificationRepository _repo;

		public NotificationService(INotificationRepository repo)
		{
			_repo = repo;
		}

		public async Task SendNotificationAsync(int userId, string message, int caseId)
		{
			var notification = new Notification
			{
				UserId = userId,
				Message = message,
				CaseId = caseId,
				CreatedDate = DateTime.Now,
				IsRead = false
			};

			await _repo.CreateAsync(notification);
		}

		public async Task<List<Notification>> GetUserNotificationsAsync(int userId)
		{
			return await _repo.GetByUserIdAsync(userId);
		}

		public async Task<int> GetUnreadCountAsync(int userId)
		{
			return await _repo.GetUnreadCountAsync(userId);
		}

		public async Task MarkAsReadAsync(int notificationId)
		{
			await _repo.MarkAsReadAsync(notificationId);
		}
	}
}
