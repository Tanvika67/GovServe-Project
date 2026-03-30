using GovServe_Project.DTOs.CitizenDTO;
using GovServe_Project.Models.CitizenModels;
using GovServe_Project.Repositories.Citizen;
using System.Threading.Tasks;

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

			return await _repo.CreateAsync(details);
		}

		public async Task<CitizenDetails> UpdatePersonalDetailsAsync(UpdateCitizenDetailsDTO dto)
		{
			var existing = await _repo.GetByIdAsync(dto.PersonalDetailID);
			if (existing == null)
				throw new System.Exception("Citizen details not found");

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
			

			return await _repo.UpdateAsync(existing);
		}

		public async Task<CitizenDetails> GetByApplicationIdAsync(int applicationId)
		{
			return await _repo.GetByApplicationIdAsync(applicationId);
		}

		public async Task<CitizenDetails> GetByIdAsync(int personalDetailId)
		{
			return await _repo.GetByIdAsync(personalDetailId);
		}
	}
}