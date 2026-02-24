using GovServe_Project.Models;
namespace GovServe_Project.Services.Interfaces
{
	public interface INotificationService
	{
		Task<List<Notification>> GetNotificationsAsync(int userId);
	}
}
