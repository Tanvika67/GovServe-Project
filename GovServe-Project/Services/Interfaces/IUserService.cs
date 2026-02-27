using GovServe_Project.DTOs;

namespace GovServe_Project.Services.Interfaces
{
	public interface IUserService
	{
		Task<string> RegisterAsync(RegisterDTO dto);

		Task<string> LoginAsync(LoginDTO dto);
	}
}
