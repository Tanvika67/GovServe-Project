using GovServe_Project.DTOs;
using GovServe_Project.Models;

namespace GovServe_Project.Repository.Interface
{
	public interface IUserRepository
	{
		Task AddAsync(Users user);

		Task<Users> GetByEmailAsync(string email);
	}
}
