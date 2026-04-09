using GovServe_Project.Data;
using GovServe_Project.Models.AdminModels;
using GovServe_Project.Repository.Interface.AdminRepositoryInterface;
using Microsoft.EntityFrameworkCore;

namespace GovServe_Project.Repository.Repository_Implentation.AdminRepositoryImplementation
{
    public class EligibilityRuleRepository : IEligibilityRuleRepository
    {
        private readonly GovServe_ProjectContext _context;

        public EligibilityRuleRepository(GovServe_ProjectContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EligibilityRule>> GetAllAsync()
        {

            return await _context.EligibilityRules
                    .Include(r => r.Service)
                    .ToListAsync();
        }

        

        public async Task<EligibilityRule?> GetByIdAsync(int id)
        {

            return await _context.EligibilityRules
                    .Include(r => r.Service)
                    .FirstOrDefaultAsync(r => r.RuleID == id);
        }

        

        public async Task AddAsync(EligibilityRule rule)
        {
            await _context.EligibilityRules.AddAsync(rule);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(EligibilityRule rule)
        {
            _context.EligibilityRules.Update(rule);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(EligibilityRule rule)
        {
            _context.EligibilityRules.Remove(rule);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EligibilityRule>> GetByServiceNameAsync(string serviceName)
        {
            return await _context.EligibilityRules
                .Include(r => r.Service)
                .Where(r => r.Service != null &&
                            r.Service.ServiceName.ToLower()
                            .Contains(serviceName.ToLower()))
                .ToListAsync();
        }

		//for citizen 
		public async Task<IEnumerable<EligibilityRule>> GetByServiceIdAsync(int serviceId)
		{
			return await _context.EligibilityRules
				.Include(r => r.Service)
				.Where(r => r.ServiceID == serviceId)
				.ToListAsync();
		}
	}
}
