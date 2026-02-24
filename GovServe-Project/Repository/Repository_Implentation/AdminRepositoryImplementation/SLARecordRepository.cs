using GovServe_Project.Data;
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

        public async Task<IEnumerable<SLARecord>> GetAllAsync()
        {
           return await _context.SLARecords.ToListAsync();

        }
          

        public async Task<SLARecord?> GetByIdAsync(int id)
        {
            return await _context.SLARecords.FindAsync(id);

        }
            

        public async Task AddAsync(SLARecord record)
        {
            await _context.SLARecords.AddAsync(record);
            await _context.SaveChangesAsync();

        }
           

        public async Task UpdateAsync(SLARecord record)
        {
            _context.SLARecords.Update(record);
            await _context.SaveChangesAsync();

        }
            

        public async Task DeleteAsync(SLARecord record)
        {
            _context.SLARecords.Remove(record);
            await _context.SaveChangesAsync();

        }
          
    }


}
