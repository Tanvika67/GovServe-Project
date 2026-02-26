using GovServe_Project.Models;
namespace GovServe_Project.Repository.Interface
{
	public interface INotificationRepository
	{
		Task AddAsync(Notification notification);
		Task<List<Notification>> GetByUserIdAsync(int userId);
		Task<int> GetUnreadCountAsync(int userId);
		Task<Notification> GetByIdAsync(int id);
		Task SaveAsync();

		
	}

}
