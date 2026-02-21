using GovServe_Project.Data;
using GovServe_Project.Models;
using GovServe_Project.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace GovServe_Project.Repository.Repository_Implementation
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly GovServe_ProjectContext _context;

        public DepartmentRepository(GovServe_ProjectContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _context.Department.ToListAsync();
        }

        public async Task<Department?> GetByIdAsync(int id)
        {
            return await _context.Department.FindAsync(id);
        }

        public async Task<Department> AddAsync(Department department)
        {
            _context.Department.Add(department);
            await _context.SaveChangesAsync();
            return department;
        }

        public async Task<Department?> UpdateAsync(Department department)
        {
            var existing = await _context.Department.FindAsync(department.DepartmentID);
            if (existing == null)
                return null;

            existing.DepartmentName = department.DepartmentName;
            existing.Description = department.Description;
            existing.Status = department.Status;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var department = await _context.Department.FindAsync(id);
            if (department == null)
                return false;

            _context.Department.Remove(department);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}