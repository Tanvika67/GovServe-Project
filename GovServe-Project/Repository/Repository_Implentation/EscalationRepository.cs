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
 
        public async Task CreateEscalationAsync(Escalation escalation)
        {
            await _context.Escalation.AddAsync(escalation);
            await _context.SaveChangesAsync();
        }
        public async Task<int> GetCountAsync()
        {
            return await _context.Escalation.CountAsync();
        }
    }
}
 
