using GovServe_Project.Models;
namespace GovServe_Project.Services.Interfaces
{
	public interface INotificationService
	{
		Task SendNotificationAsync(int userId, string message, int caseId);

		Task<List<Notification>> GetUserNotificationsAsync(int userId);

		Task<int> GetUnreadCountAsync(int userId);

		Task MarkAsReadAsync(int notificationId);

		
	}
}
