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
            => await _context.SLARecords.ToListAsync();

        public async Task<SLARecords> GetByIdAsync(int id)
            => await _context.SLARecords.FindAsync(id);

        public async Task<IEnumerable<SLARecords>> GetByStatusAsync(SLAStatus status)
        {
            return await _context.SLARecords
                .Include(x => x.Case)
                .Where(x => x.Status == status)
                .ToListAsync(); // List internally 
        }

        public async Task AddAsync(SLARecords record)
            => await _context.SLARecords.AddAsync(record);

        public void Update(SLARecords record)
            => _context.SLARecords.Update(record);

        public void Delete(SLARecords record)
            => _context.SLARecords.Remove(record);

        public async Task SaveAsync()
            => await _context.SaveChangesAsync();
    }




}
