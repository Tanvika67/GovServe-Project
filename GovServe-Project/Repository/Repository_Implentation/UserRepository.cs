using GovServe_Project.Data;
using GovServe_Project.Models;
using GovServe_Project.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using GovServe_Project.Models;
using GovServe_Project.Models.AdminModels;

namespace GovServe_Project.Repository.Repository_Implentation
{
	public class UserRepository : IUserRepository
	{
		private readonly GovServe_ProjectContext _context;

		public UserRepository(GovServe_ProjectContext context)
		{
			_context = context;
		}

		public async Task AddAsync(Users user)
		{
			await _context.User.AddAsync(user);
			await _context.SaveChangesAsync();
		}
		public async Task<Users> GetByEmailAsync(string email)
		{
			return await _context.User.FirstOrDefaultAsync(x => x.Email == email);
		}

		// Get user by Id
		public async Task<Users> GetByIdAsync(int id)
		{
			return await _context.User.FindAsync(id);
		}

		// Get all users (Admin)
		public async Task<List<Users>> GetAllAsync()
		{
			return await _context.User.ToListAsync();
		}

		// Update user
		public async Task UpdateAsync(Users user)
		{
			_context.User.Update(user);
			await _context.SaveChangesAsync();
		}

		// Delete user
		public async Task DeleteAsync(Users user)
		{
			_context.User.Remove(user);
			await _context.SaveChangesAsync();
		}
		//Available officer by dept to get count of active cases
		public async Task<List<Users>> GetOfficersByDepartmentAsync(int departmentId)
		{
			return await _context.User
				.Where(u => u.RoleName == "Officer" && u.DepartmentID == departmentId)
				.ToListAsync();
		}
		//Count of active cases
		public async Task<int> GetActiveCaseCountByOfficerAsync(int officerId)
		{
			return await _context.Case
				.CountAsync(c => c.AssignedOfficerId == officerId && c.Status != "Completed");
		}
	}
}

