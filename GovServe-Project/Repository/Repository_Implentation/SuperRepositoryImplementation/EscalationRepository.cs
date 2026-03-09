using GovServe_Project.Data;
using GovServe_Project.Enum;
using GovServe_Project.Models.AdminModels;
using GovServe_Project.Models.SuperModels;
using GovServe_Project.Repository.Interface.SuperRepositoryInterface;
using Microsoft.EntityFrameworkCore;

namespace GovServe_Project.Repository.Repository_Implentation.SuperRepositoryImplementation
{
	public class EscalationRepository : IEscalationRepository
	{
		private readonly GovServe_ProjectContext _context;

		public EscalationRepository(GovServe_ProjectContext context)
		{
			_context = context;
		}

		public async Task CreateAsync(Escalation escalation)
		{
			await _context.Escalation.AddAsync(escalation);
			await _context.SaveChangesAsync();
		}
		public async Task<List<SLARecords>> GetSLABreachedCasesAsync()
		{
			return await _context.SLARecords
				.Where(s => s.Status == SLAStatus.Breached)
				.ToListAsync();
		}
		public async Task<SLARecords> GetByCaseIdAsync(int caseId)
		{
			return await _context.SLARecords
				.FirstOrDefaultAsync(s => s.CaseId == caseId);
		}
		public async Task<List<Escalation>> GetAllAsync()
		{
			return await _context.Escalation.ToListAsync();
		}

		public async Task<int> GetEscalationCountAsync()
		{
			return await _context.Escalation.CountAsync();
		}
	}
}
 