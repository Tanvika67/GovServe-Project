using System.Threading.Tasks;
using GovServe_Project.Data;
using GovServe_Project.Models.CitizenModels;
using Microsoft.EntityFrameworkCore;

namespace GovServe_Project.Repositories.Citizen
{
	public class CitizenDetailsRepository : ICitizenDetailsRepository
	{
		private readonly GovServe_ProjectContext _context;

		public CitizenDetailsRepository(GovServe_ProjectContext  context)
		{
			_context = context;
		}

		public async Task<CitizenDetails> CreateAsync(CitizenDetails details)
		{
			_context.CitizenDetails.Add(details);
			await _context.SaveChangesAsync();
			return details;
		}

		public async Task<CitizenDetails> UpdateAsync(CitizenDetails details)
		{
			_context.CitizenDetails.Update(details);
			await _context.SaveChangesAsync();
			return details;
		}

		public async Task<CitizenDetails> GetByApplicationIdAsync(int applicationId)
		{
			return await _context.CitizenDetails
								 .FirstOrDefaultAsync(d => d.ApplicationID == applicationId);
		}

		public async Task<CitizenDetails> GetByIdAsync(int personalDetailId)
		{
			return await _context.CitizenDetails
								 .FirstOrDefaultAsync(d => d.PersonalDetailID == personalDetailId);
		}
	}
}