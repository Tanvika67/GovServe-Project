using GovServe_Project.Data;
using GovServe_Project.Models;
using Microsoft.EntityFrameworkCore;
using GovServe_Project.Repository.Interface;
namespace GovServe_Project.Repository.Repository_Implentation
{
	public class NotificationRepository : INotificationRepository
	{
		private readonly GovServe_ProjectContext _context;

		public NotificationRepository(GovServe_ProjectContext context)
		{
			_context = context;
		}
		public async Task<List<Notification>> GetNotificationsAsync(int userId)
		{
			return await _context.Notification
				.Where(n => n.UserId == userId)
				.ToListAsync();
		}

		public async Task CreateAsync(Notification notification)
		{
			await _context.Notification.AddAsync(notification);
			await _context.SaveChangesAsync();
		}
	}
}
