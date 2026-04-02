using GovServe_Project.DTOs.SupervisorDTO;
using GovServe_Project.Models.SuperModels;
using Microsoft.CodeAnalysis.Operations;
namespace GovServe_Project.Services.Interfaces.SuperServiceInterface
{
	public interface INotificationService
	{
		Task SendNotificationAsync(int userId, string message, int caseId);
		Task SendManualNotification(NotificationDto dto);
		Task<List<Notification>> GetUserNotificationsAsync(int userId);
		Task<List<Notification>> GetUnreadNotifications(int userId);
		Task NotifyCaseCreated(int caseId);
		Task NotifyCaseAssigned(int caseId,int OfficerId);
		Task NotifyCaseEscalated(int caseId,int newOfficerId);
		Task NotifySLABreach(int caseId);
		Task<int> GetUnreadCountAsync(int userId);
		Task MarkAsReadAsync(int notificationId);
	    Task SendNotificationAsync(int userId, string message, int caseId, string category);
		
	}
}
