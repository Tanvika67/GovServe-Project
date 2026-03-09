using GovServe_Project.Data;
using GovServe_Project.Models.AdminModels;
using GovServe_Project.Repository.Interface.AdminRepositoryInterface;
using Microsoft.EntityFrameworkCore;

namespace GovServe_Project.Repository.Repository_Implentation.AdminRepositoryImplementation
{
    public class RoleRepository : IRoleRepository
    {
        private readonly GovServe_ProjectContext _context;

        public RoleRepository(GovServe_ProjectContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
            =>await _context.Roles.ToListAsync();

        public async Task<Role?> GetByIdAsync(int id)
            => await _context.Roles.FindAsync(id);

        public async Task<Role?> GetByNameAsync(string roleName)
            => await _context.Roles.FirstOrDefaultAsync(x => x.RoleName == roleName);

        public async Task AddAsync(Role role)
            => await _context.Roles.AddAsync(role);

        public void Update(Role role)
            => _context.Roles.Update(role);

        public void Delete(Role role)
            => _context.Roles.Remove(role);

        public async Task SaveAsync()
            => await _context.SaveChangesAsync();
    }

}
