using GovServe_Project.Data;
using GovServe_Project.Enum;
using GovServe_Project.Models.AdminModels;
using GovServe_Project.Repository.Interface.AdminRepositoryInterface;
using Microsoft.EntityFrameworkCore;

namespace GovServe_Project.Repository.Repository_Implentation.AdminRepositoryImplementation
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly GovServe_ProjectContext _context;

        public ServiceRepository(GovServe_ProjectContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Service>> GetAllAsync()
        {

            return await _context.Services
                            .Include(s => s.Department)
                            .ToListAsync();


        }

        public async Task<Service?> GetByIdAsync(int id)
        {

            return await _context.Services
                            .Include(s => s.Department)
                            .FirstOrDefaultAsync(s => s.ServiceID == id);

        }
        public async Task<IEnumerable<Service>> GetActiveAsync()
        {
            return await _context.Services
                .Include(s=> s.Department)
                .Where(s => s.Status == ServiceStatus.Active)
                .ToListAsync();
        }


        public async Task AddAsync(Service service)
        {
            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Service service)
        {
            // Entity is already tracked from GetByIdAsync
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Service service)
        {
            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Service>> GetByDepartmentNameAsync(string departmentName)
        {
            return await _context.Services
                .Include(s => s.Department)
                .Where(s => s.Department != null && s.Department.DepartmentName.ToLower() == departmentName.ToLower()).ToListAsync();
        }
        public async Task<int> GetServiceCountAsync()
        {
            return await _context.Services.CountAsync();
        }

        public async Task<int> GetActiveServiceCountAsync()
        {
            return await _context.Services
                .Where(s => s.Status == ServiceStatus.Active)
                .CountAsync();
        }

    }
}