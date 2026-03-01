using GovServe_Project.DTOs;
using GovServe_Project.Models;

namespace GovServe_Project.Repository.Interface
{
	public interface IUserRepository
	{
		Task AddAsync(Users user);
		Task<Users> GetByEmailAsync(string email);
		Task<Users> GetByIdAsync(int id);
		Task<List<Users>> GetAllAsync();
		Task UpdateAsync(Users user);
		Task DeleteAsync(Users user);
		Task<List<Users>> GetOfficersByDepartmentAsync(int departmentId);
		Task<int> GetActiveCaseCountByOfficerAsync(int officerId);
	}

}
