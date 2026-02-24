using GovServe_Project.Models;
namespace GovServe_Project.Repository.Interface
{
		public interface INotificationRepository
		{
			Task CreateAsync(Notification notification);
			Task<List<Notification>> GetByUserIdAsync(int userId);
			Task<int> GetUnreadCountAsync(int userId);
			Task MarkAsReadAsync(int notificationId);
		}
	
}
