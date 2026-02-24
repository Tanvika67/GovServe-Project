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

		public async Task CreateAsync(Notification notification)
		{
			await _context.Notification.AddAsync(notification);
			await _context.SaveChangesAsync();
		}

		public async Task<List<Notification>> GetByUserIdAsync(int userId)
		{
			return await _context.Notification
				.Where(x => x.UserId == userId)
				.OrderByDescending(x => x.CreatedDate)
				.ToListAsync();
		}

		public async Task<int> GetUnreadCountAsync(int userId)
		{
			return await _context.Notification
				.CountAsync(x => x.UserId == userId && !x.IsRead);
		}

		public async Task MarkAsReadAsync(int notificationId)
		{
			var data = await _context.Notification.FindAsync(notificationId);

			if (data != null)
			{
				data.IsRead = true;
				await _context.SaveChangesAsync();
			}
		}
	}
}
