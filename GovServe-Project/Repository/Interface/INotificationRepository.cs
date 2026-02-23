using GovServe_Project.Models;
namespace GovServe_Project.Repository.Interface
{
	public interface INotificationRepository
	{
		Task CreateAsync(Notification notification);
		Task<List<Notification>> GetNotificationsAsync(int userId);
	}
}
