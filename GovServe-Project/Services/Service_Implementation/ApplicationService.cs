using Microsoft.EntityFrameworkCore;
using GovServe_Project.Data;
using GovServe_Project.Repository.Interface;
using GovServe_Project.Models;
using GovServe_Project.Services.Interfaces;

namespace GovServe_Project.Services.Service_Implementation
{
	public class ApplicationService : IApplicationService
	{
		private readonly IApplicationRepository _repository;

		public ApplicationService(IApplicationRepository repository)
		{
			_repository = repository;
		}

		// Create Application
		public async Task CreateApplication(Application app)
		{

			app.ApplicationStatus = "Pending";

			await _repository.AddApplication(app);
		}

		// My Applications
		public async Task<List<Application>> MyApplications(int userId)
		{
			return await _repository.GetByUser(userId);
		}

		// Application Status
		public async Task<Application> ApplicationStatus(int id)
		{
			return await _repository.GetById(id);
		}

		// Delete Application (Only Pending)
		public async Task DeleteApplication(int id)
		{
			var data = await _repository.GetById(id);

			if (data != null && data.ApplicationStatus == "Pending")
			{
				await _repository.Delete(data);
			}
		}
	}
}
