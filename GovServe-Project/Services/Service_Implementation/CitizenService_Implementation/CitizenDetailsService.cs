using System.Threading.Tasks;
using GovServe_Project.DTOs.CitizenDTO;
using GovServe_Project.Exceptions;
using GovServe_Project.Models.CitizenModels;
using GovServe_Project.Repositories.Citizen;

namespace GovServe_Project.Services.Citizen
{
	public class CitizenDetailsService : ICitizenDetailsService
	{
		private readonly ICitizenDetailsRepository _repo;

		public CitizenDetailsService(ICitizenDetailsRepository repo)
		{
			_repo = repo;
		}

		public async Task<CitizenDetails> CreatePersonalDetailsAsync(CreateCitizenDetailsDTO dto)
		{
			var details = new CitizenDetails
			{
				ApplicationID = dto.ApplicationID,
				FullName = dto.FullName,
				Gender = dto.Gender,
				DateOfBirth = dto.DateOfBirth,
				FatherName = dto.FatherName,
				MotherName = dto.MotherName,
				Email = dto.Email,
				Phone = dto.Phone,
				AddressLine1 = dto.AddressLine1,
				AddressLine2 = dto.AddressLine2,
				City = dto.City,
				State = dto.State,
				Pincode = dto.Pincode,
				AadhaarNumber = dto.AadhaarNumber,
			};

			try
			{
				// Keeping the original return type
				return await _repo.CreateAsync(details);
			}
			catch (System.Exception)
			{
				// This prevents the code from stopping if a mandatory database field is missing
				throw new BadRequestException("Failed to save details. Please ensure ApplicationID and all mandatory fields are valid.");
			}
		}

		public async Task<CitizenDetails> UpdateByApplicationIdAsync(int applicationId, UpdateCitizenDetailsDTO dto)
		{
			// 1. Get By ApplicationId
			var existing = await _repo.GetByApplicationIdAsync(applicationId);

			if (existing == null)
				throw new NotFoundException($"Citizen details for Application ID {applicationId} not found.");

			// 2. Fields update kara
			existing.FullName = dto.FullName;
			existing.Gender = dto.Gender;
			existing.DateOfBirth = dto.DateOfBirth;
			existing.FatherName = dto.FatherName;
			existing.MotherName = dto.MotherName;
			existing.Email = dto.Email;
			existing.Phone = dto.Phone;
			existing.AddressLine1 = dto.AddressLine1;
			existing.AddressLine2 = dto.AddressLine2;
			existing.City = dto.City;
			existing.State = dto.State;
			existing.Pincode = dto.Pincode;
			existing.AadhaarNumber = dto.AadhaarNumber;

			// 3. Save kara
			return await _repo.UpdateAsync(existing);
		}

		public async Task<CitizenDetails> GetByApplicationIdAsync(int applicationId)
		{
			var details = await _repo.GetByApplicationIdAsync(applicationId);

			if (details == null)
				throw new NotFoundException($"No details found for Application ID {applicationId}.");

			return details;
		}

		public async Task<CitizenDetails> GetByIdAsync(int personalDetailId)
		{
			var details = await _repo.GetByIdAsync(personalDetailId);

			if (details == null)
				throw new NotFoundException($"Citizen detail with ID {personalDetailId} not found.");

			return details;
		}
	}
}