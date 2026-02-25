using GovServe_Project.Data;
using GovServe_Project.Models;
using GovServe_Project.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace GovServe_Project.Repository.Repository_Implentation
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
 