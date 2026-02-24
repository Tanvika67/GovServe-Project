using Microsoft.EntityFrameworkCore;
using GovServe_Project.Models;
using GovServe_Project.Data;
using GovServe_Project.Repository.Interface;

namespace GovServe_Project.Repository.Repository_Implentation
{
	public class CaseRepository : ICaseRepository
	{
		private readonly GovServe_ProjectContext _context;

		public CaseRepository(GovServe_ProjectContext context)
		{
			_context = context;
		}

		public async Task<List<Case>> GetAllAsync()
		{
			return await _context.Case.ToListAsync();
		}

		public async Task<Case> GetByIdAsync(int caseId)
		{
			return await _context.Case.FirstOrDefaultAsync(c => c.CaseId == caseId);
		}

		public async Task<List<Case>> GetByStatusAsync(string status)
		{
			return await _context.Case.Where(c => c.Status == status).ToListAsync();
		}

		public async Task<List<Case>> GetEscalatedAsync()
		{
			return await _context.Case.Where(c => c.IsEscalated).ToListAsync();
		}

		public async Task<List<Case>> GetSlaBreachedCasesAsync()
		{
			return await _context.Case
				.Where(c => c.AssignedDate < DateTime.Now.AddDays(-3) && !c.IsEscalated)
				.ToListAsync();
		}

		public async Task UpdateAsync(Case caseData)
		{
			_context.Case.Update(caseData);
			await _context.SaveChangesAsync();
		}

		public async Task<int> CountByStatusAsync(string status)
		{
			return await _context.Case.CountAsync(c => c.Status == status);
		}

		public async Task<int> CountEscalatedAsync()
		{
			return await _context.Case.CountAsync(c => c.IsEscalated);
		}
	}
}