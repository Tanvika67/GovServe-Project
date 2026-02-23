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

		public async Task<List<Notification>> GetNotificationsAsync(int userId)
		{
			return await _repo.GetNotificationsAsync(userId);
		}
	}
}
