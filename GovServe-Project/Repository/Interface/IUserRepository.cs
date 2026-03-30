using GovServe_Project.DTOs;
using GovServe_Project.Models;

namespace GovServe_Project.Repository.Interface
{
	public interface IUserRepository
	{
		Task AddAsync(Users user);
		Task<bool> DepartmentExistsAsync(int departmentId);
		Task<Users> GetByEmailAsync(string email);
		Task<Users> GetByIdAsync(int id);
		Task<List<Users>> GetAllAsync();
		Task UpdateAsync(Users user);
		Task DeleteAsync(Users user);

		Task<List<Users>> GetPendingUsers();
		Task<Users> GetUserById(int id);
		Task UpdateUser(Users user);

		//------------------Supervisor Methods------------------
		Task<List<Users>> GetOfficersByDepartmentAsync(int departmentId);
		Task<int> GetAdminIdAsync();
		Task<int> GetGrievanceOfficerIdAsync();


	}
}
