using GovServe_Project.DTOs;
using GovServe_Project.Models;
using GovServe_Project.Repository.Interface;
using GovServe_Project.Services.Interfaces;

namespace GovServe_Project.Services.Service_Implementation
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _repository;

		public UserService(IUserRepository repository)
		{
			_repository = repository;
		}

		// Registration
		public async Task<string> RegisterAsync(RegisterDTO dto)
		{
			var existingUser = await _repository.GetByEmailAsync(dto.Email);

			if (existingUser != null)
				return "Email already exists";

			var user = new Users
			{
				FullName = dto.FullName,
				Email = dto.Email,
				Phone = dto.Phone,
				Password = dto.Password,
				//Role = "Citizen"
			};

			await _repository.AddAsync(user);

			return "Registration Successful";
		}


		// Login
		public async Task<string> LoginAsync(LoginDTO dto)
		{
			var user = await _repository.GetByEmailAsync(dto.Email);

			if (user == null)
				return "User not found";

			if (user.Password != dto.Password)
				return "Invalid Password";

			return "Login Successful";
		}
	}
}

