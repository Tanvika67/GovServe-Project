using GovServe_Project.Data;
using GovServe_Project.Models;
using GovServe_Project.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using GovServe_Project.Models;

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
	}
}

