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

			// Role/Department validation
			if (dto.RoleName == "Officer")
			{
				if (dto.DepartmentID == null)
					return "Department is required for Officer role.";

				var departmentExists = await _repository.DepartmentExistsAsync(dto.DepartmentID.Value);
				if (!departmentExists)
					return "Invalid DepartmentID.";
			}
			else
			{
				// For non-Officer roles, force department fields to null
				dto.DepartmentID = null;
				dto.DepartmentName = null;
			}

			var user = new Users
			{
				FullName = dto.FullName,
				Email = dto.Email,
				Phone = dto.Phone,
				Password = dto.Password,
				RoleID = dto.RoleID,
				RoleName = dto.RoleName,
				DepartmentID = dto.DepartmentID,
				DepartmentName = dto.DepartmentName,
			};

			// Auto-approve Citizens, others go Pending
			if (dto.RoleName == "Citizen")
				user.Status = "Approved";
			else
				user.Status = "Pending";

			await _repository.AddAsync(user);

			if (user.Status == "Pending")
				return "Your registration request is sent to admin";

			return "Registration Successful";
		}

		// Get profile
		public async Task<Users> GetUserProfile(int id)
		{
			return await _repository.GetByIdAsync(id);
		}

		// Get all users 
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

		//Get All Pending Users
		public async Task<List<Users>> GetPendingUsers()
			{
				return await _repository.GetPendingUsers();
			}

		// Approve User
		public async Task<string> ApproveUser(int userId)
			{
				var user = await _repository.GetUserById(userId);

				if (user == null)
					return "User not found";

				if (user.Status != "Pending")
					return "User already processed";

				user.Status = "Approved";

				await _repository.UpdateUser(user);

				return "User approved successfully";
			}

		// Reject User
		public async Task<string> RejectUser(int userId)
			{
				var user = await _repository.GetUserById(userId);

				if (user == null)
					return "User not found";

				if (user.Status != "Pending")
					return "User already processed";

				user.Status = "Rejected";

				await _repository.UpdateUser(user);

				return "User rejected successfully";
			}
		}
	}


