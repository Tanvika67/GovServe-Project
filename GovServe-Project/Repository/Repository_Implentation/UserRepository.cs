using GovServe_Project.Data;
using GovServe_Project.Migrations;
using GovServe_Project.Models;
using GovServe_Project.Models;
using GovServe_Project.Models.AdminModels;
using GovServe_Project.Repository.Interface;
using Microsoft.EntityFrameworkCore;

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

		// Check if department exists
		public async Task<bool> DepartmentExistsAsync(int departmentId)
		{
			return await _context.Departments
				.AnyAsync(d => d.DepartmentID == departmentId);
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

		public async Task<List<Users>> GetPendingUsers()
		{
			return await _context.User
				.Where(x => x.Status == "Pending")
				.ToListAsync();
		}

		public async Task<Users> GetUserById(int id)
		{
			return await _context.User
				.FirstOrDefaultAsync(x => x.UserId == id);
		}

		public async Task UpdateUser(Users user)
		{
			_context.User.Update(user);
			await _context.SaveChangesAsync();
		}

	}

	}


