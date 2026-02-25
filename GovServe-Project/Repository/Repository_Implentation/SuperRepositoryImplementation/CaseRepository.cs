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

		public async Task<IEnumerable<Case>> GetAllAsync()
		{
			return await _context.Case.ToListAsync();
		}

		public async Task<IEnumerable<Case>> GetByStatusAsync(string status)
		{
			return await _context.Case
				.Where(x => x.Status == status)
				.ToListAsync();
		}

		public async Task<Case> GetByIdAsync(int id)
		{
			return await _context.Case.FindAsync(id);
		}

		public async Task AddAsync(Case c)
		{
			await _context.Case.AddAsync(c);
		}

		public void Update(Case c)
		{
			_context.Case.Update(c);
		}

		public async Task SaveAsync()
		{
			await _context.SaveChangesAsync();
		}
	}
}