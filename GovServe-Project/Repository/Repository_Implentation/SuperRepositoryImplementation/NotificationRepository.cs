using GovServe_Project.Data;
using GovServe_Project.Models.SuperModels;
using Microsoft.EntityFrameworkCore;
using GovServe_Project.Repository.Interface.SuperRepositoryInterface;
namespace GovServe_Project.Repository.Repository_Implentation.SuperRepositoryImplementation
{
	public class NotificationRepository : INotificationRepository
	{
		private readonly GovServe_ProjectContext _context;

		public NotificationRepository(GovServe_ProjectContext context)
		{
			_context = context;
		}

		public async Task AddAsync(Notification notification)
		{
			await _context.Notification.AddAsync(notification);
		}

		public async Task<List<Notification>> GetByUserIdAsync(int userId)
		{
			return await _context.Notification
				.Where(n => n.UserId == userId)
				.OrderByDescending(n => n.CreatedDate)
				.ToListAsync();
		}

		public async Task<int> GetUnreadCountAsync(int userId)
		{
			return await _context.Notification
				.CountAsync(n => n.UserId == userId && !n.IsRead);
		}

		public async Task<Notification> GetByIdAsync(int id)
		{
			return await _context.Notification.FindAsync(id);
		}

		public async Task SaveAsync()
		{
			await _context.SaveChangesAsync();
		}

		
	}
}