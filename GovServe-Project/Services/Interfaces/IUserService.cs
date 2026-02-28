using GovServe_Project.DTOs;
using GovServe_Project.Models;

namespace GovServe_Project.Services.Interfaces
{
	public interface IUserService
	{
		Task<string> RegisterAsync(RegisterDTO dto);

		Task<Users> GetUserProfile(int id);
		Task<List<Users>> GetAllUsers();
		Task<bool> UpdateUser(int id, Users model);
		Task<bool> DeleteUser(int id);
	}
}
