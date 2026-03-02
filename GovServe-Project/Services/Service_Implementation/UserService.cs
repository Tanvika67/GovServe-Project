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
				RoleID = dto.RoleID,
				RoleName=dto.RoleName,
				DepartmentID = dto.DepartmentID
			};

			await _repository.AddAsync(user);

			return "Registration Successful";
		}

		// Get profile
		public async Task<Users> GetUserProfile(int id)
		{
			return await _repository.GetByIdAsync(id);
		}

		// Get all users (Admin)
		public async Task<List<Users>> GetAllUsers()
		{
			return await _repository.GetAllAsync();
		}

		// Update user
		public async Task<bool> UpdateUser(int id, Users model)
		{
			var user = await _repository.GetByIdAsync(id);

			if (user == null)
				return false;

			user.FullName = model.FullName;
			user.Email = model.Email;

			await _repository.UpdateAsync(user);
			return true;
		}

		// Delete user
		public async Task<bool> DeleteUser(int id)
		{
			var user = await _repository.GetByIdAsync(id);

			if (user == null)
				return false;

			await _repository.DeleteAsync(user);
			return true;
		}
	}
}

