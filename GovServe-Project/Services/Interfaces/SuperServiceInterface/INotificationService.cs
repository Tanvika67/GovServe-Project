using GovServe_Project.Models.SuperModels;
using Microsoft.CodeAnalysis.Operations;
namespace GovServe_Project.Services.Interfaces.SuperServiceInterface
{
	public interface INotificationService
	{
		Task SendNotificationAsync(int userId, string message, int caseId);

		Task<List<Notification>> GetUserNotificationsAsync(int userId);

		Task<int> GetUnreadCountAsync(int userId);

		Task MarkAsReadAsync(int notificationId);
	
	    Task SendNotificationAsync(int userId, string message, int caseId, string category);
		

	}
}
