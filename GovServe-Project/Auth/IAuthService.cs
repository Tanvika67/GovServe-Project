using GovServe_Project.DTOs;

namespace GovServe_Project.Auth
{
    public interface IAuthService
    {
		Task<string> Login(LoginDTO dto);
	}
}
