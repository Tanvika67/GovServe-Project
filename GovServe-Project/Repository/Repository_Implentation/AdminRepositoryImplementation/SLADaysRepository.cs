using GovServe_Project.Data;
using GovServe_Project.Models.AdminModels;
using GovServe_Project.Repository.Interface.AdminRepositoryInterface;
using Microsoft.EntityFrameworkCore;

namespace GovServe_Project.Repository.Repository_Implentation.AdminRepositoryImplementation
{
    public class SLADayRepository : ISLADayRepository
    {
        private readonly GovServe_ProjectContext _context;

        public SLADayRepository(GovServe_ProjectContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SLADays>> GetAllAsync()
        {
           return await _context.SLADays.ToListAsync();

        }
            

        public async Task<SLADays?> GetByIdAsync(int id)
        {
            return await _context.SLADays.FindAsync(id);

        }
            

        public async Task<SLADays?> GetByRoleAsync(string roleName)
        {
            return await _context.SLADays.FirstOrDefaultAsync(x => x.RoleName == roleName);
        }
           

        public async Task AddAsync(SLADays slaDay)
        {
            await _context.SLADays.AddAsync(slaDay);
            await _context.SaveChangesAsync();

        }
           

        public async Task UpdateAsync(SLADays slaDay)
        {
             _context.SLADays.Update(slaDay);
            await _context.SaveChangesAsync();

        }
           

        public async Task DeleteAsync(SLADays slaDay)
        {
             _context.SLADays.Remove(slaDay);
            await _context.SaveChangesAsync();

        }
           
    }

}
