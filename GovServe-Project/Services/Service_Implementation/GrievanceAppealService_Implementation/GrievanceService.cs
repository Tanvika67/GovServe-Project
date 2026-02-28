using GovServe_Project.Models;
using GovServe_Project.Models.GrievanceAppealModel;
using GovServe_Project.Repository.Interface;
using GovServe_Project.Services.Interfaces;
using GovServe_Project.Services.Interfaces.GrievanceAppealService_Interface;
namespace GovServe_Project.Services.Service_Implementation.GrievanceAppealService_Implementation
{
	public class GrievanceService : IGrievanceService
	{
		private readonly IGrievanceRepository _repository;

		// Constructor injection
		public GrievanceService(IGrievanceRepository repository)
		{
			_repository = repository;
		}

		// Citizen raises grievance
		public async Task RaiseGrievanceAsync(Grievance grievance)
		{
			// Set default values
			grievance.FiledDate = DateTime.Now;
			grievance.Status = "Submitted";

			// Save grievance
			await _repository.AddAsync(grievance);
		}
     
		// Get grievances for logged citizen
		public async Task<List<Grievance>> MyGrievancesAsync(int citizenId)
		{
			return await _repository.GetByCitizenAsync(citizenId);
		}
 
		// Get grievance details/Status by Id
		public async Task<Grievance?> GrievanceStatusAsync(int id)
		{
			return await _repository.GetByIdAsync(id);
		}
	}
}
