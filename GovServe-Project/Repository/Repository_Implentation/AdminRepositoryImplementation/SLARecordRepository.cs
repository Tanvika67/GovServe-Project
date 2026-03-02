using GovServe_Project.Data;
using GovServe_Project.Enum;
using GovServe_Project.Models.AdminModels;
using GovServe_Project.Repository.Interface.AdminRepositoryInterface;
using Microsoft.EntityFrameworkCore;

namespace GovServe_Project.Repository.Repository_Implentation.AdminRepositoryImplementation
{
    public class SLARecordRepository : ISLARecordRepository
    {
        private readonly GovServe_ProjectContext _context;

        public SLARecordRepository(GovServe_ProjectContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SLARecords>> GetAllAsync()
        {
            return await _context.SLARecords.ToListAsync();

        }
            

        public async Task<SLARecords> GetByIdAsync(int id)
        {
             return await _context.SLARecords.FindAsync(id);

        }
           

        // This fetches SLA using CaseId(FK)
		public async Task<SLARecords> GetByCaseIdAsync(int caseId)
		{
			return await _context.SLARecords
				.FirstOrDefaultAsync(x => x.CaseId == caseId);
		}
		public async Task<IEnumerable<SLARecords>> GetByStatusAsync(SLAStatus status)
        {
            return await _context.SLARecords
                .Include(x => x.Case)
                .Where(x => x.Status == status)
                .ToListAsync(); // List internally 
        }

        public async Task AddAsync(SLARecords record)
        {
            await _context.SLARecords.AddAsync(record);
            await _context.SaveChangesAsync();

        }
            

        public async Task UpdateAsync(SLARecords record)
        {
            _context.SLARecords.Update(record);
            await _context.SaveChangesAsync();

        }
            

        public async Task DeleteAsync(SLARecords record)
        {
            _context.SLARecords.Remove(record);
            await _context.SaveChangesAsync();

        }
           
    }


}
