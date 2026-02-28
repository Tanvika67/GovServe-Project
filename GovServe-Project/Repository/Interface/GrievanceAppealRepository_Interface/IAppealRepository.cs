using System;
using GovServe_Project.Models;
using GovServe_Project.Models.GrievanceAppealModel;
namespace GovServe_Project.Repository.Interface
{
	public interface IAppealRepository
	{
		// Add new appeal into database
		Task AddAsync(Appeal appeal);

		// Get appeals by Application ID (Citizen My Appeals)
		Task<List<Appeal>> GetByApplicationAsync(int applicationId);

		// Get appeal by ID (Status view)
		Task<Appeal?> GetByIdAsync(int id);
	}
}
