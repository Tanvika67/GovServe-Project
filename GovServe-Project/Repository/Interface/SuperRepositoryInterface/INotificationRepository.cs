using GovServe_Project.Models.SuperModels;
namespace GovServe_Project.Repository.Interface.SuperRepositoryInterface
{
	public interface INotificationRepository
	{
		Task AddAsync(Notification notification);
		Task<List<Notification>> GetByUserIdAsync(int userId);
		Task<int> GetUnreadCountAsync(int userId);
		Task<List<Notification>> GetUnreadNotifications(int userId);
        Task<List<Notification>> GetReadNotifications(int userId);

		Task<Notification> GetByIdAsync(int id);

        void Update(Notification notification);
        Task SaveAsync();

		
	}

}
