using GovServe_Project.Data;
using GovServe_Project.Models;
using GovServe_Project.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using GovServe_Project.Models;
using Microsoft.AspNetCore.Identity;
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
		// Once cross verify with everyone creating enum is difficult
		public async Task<List<Users>> GetOfficersByDepartmentAsync(int departmentId)
		{
			var officerRoleId = await GetRoleIdByNameAsync("Officer");

			return await _context.User
				.Where(u => u.DepartmentID == departmentId && u.RoleID == officerRoleId)
				.ToListAsync();
		}
		public async Task<int> GetRoleIdByNameAsync(string roleName)
		{
			return await _context.Roles
				.Where(r => r.RoleName == roleName)
				.Select(r => r.RoleID)
				.FirstOrDefaultAsync();
		}
		public async Task<int> GetActiveCaseCountByOfficerAsync(int officerId)
		{
			return await _context.Case
				.CountAsync(c => c.AssignedOfficerId == officerId
							  && c.Status != "Completed");
		}
		public async Task<Users> GetByEmailAsync(string email)
		{
			return await _context.User.FirstOrDefaultAsync(x => x.Email == email);
		}
	}
}

