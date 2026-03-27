using GovServe_Project.DTOs.SupervisorDTO;
using GovServe_Project.Models.SuperModels;
using GovServe_Project.Repository.Interface.SuperRepositoryInterface;
using GovServe_Project.Services.Interfaces;
using GovServe_Project.Services.Interfaces.SuperServiceInterface;
using Microsoft.EntityFrameworkCore;

namespace GovServe_Project.Services.Service_Implementation.SuperServiceImplementation
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
				CaseId = caseId,
				Message = message,
				Category = "Update", // or Assignment/Escalation
				CreatedDate = DateTime.Now,
				IsRead = false
			};

			await _repo.AddAsync(notification);
			await _repo.SaveAsync();
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
			var notification = await _repo.GetByIdAsync(notificationId);

			if (notification != null)
			{
				notification.IsRead = true;
				await _repo.SaveAsync();
			}
		}

		public async Task SendNotificationAsync(int userId, string message, int caseId, string category)
		{
			Notification n = new Notification();
			n.UserId = userId;
			n.CaseId = caseId;     
			n.Message = message;
			n.Category = category; 
			n.Status = "Unread";   
			n.CreatedDate = DateTime.Now;

			await _repo.AddAsync(n); 
			await _repo.SaveAsync(); 
		}

	}
}