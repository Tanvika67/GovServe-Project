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
        {
            return await _context.Roles.ToListAsync();
        }
           
        public async Task<Role?> GetByIdAsync(int id)
        {
           return await _context.Roles.FindAsync(id);

        }
            

        public async Task<Role?> GetByNameAsync(string roleName)
        {
           return await _context.Roles.FirstOrDefaultAsync(x => x.RoleName == roleName);
        }
            

        public async Task AddAsync(Role role)
        {
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();

        }
            

        public async Task UpdateAsync(Role role)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();

        }
       
   

        public async Task DeleteAsync(Role role)
        {
             _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
        }

       
    }

}
