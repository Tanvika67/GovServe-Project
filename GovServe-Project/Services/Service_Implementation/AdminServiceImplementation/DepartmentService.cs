using GovServe_Project.DTOs.Admin;
using GovServe_Project.Exceptions;
using GovServe_Project.Models.AdminModels;
using GovServe_Project.Repository.Interface.AdminRepositoryInterface;
using GovServe_Project.Services.Interfaces.AdminServiceInterface;

namespace GovServe_Project.Services.Service_Implementation.AdminServiceImplementation
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repository;

        public DepartmentService(IDepartmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Department> GetByIdAsync(int id)
        {
            var department = await _repository.GetByIdAsync(id);
            if (department == null)
                throw new NotFoundException("Department not found");

            return department;
        }

        public async Task<Department> CreateAsync(DepartmentDTO dto)
        {
            var department = new Department
            {
                DepartmentName = dto.DepartmentName,
                Description = dto.Description,
                Status = dto.Status
            };

            return await _repository.AddAsync(department);
        }

        public async Task<Department> UpdateAsync(int id, DepartmentDTO dto)
        {
            var department = new Department
            {
                DepartmentID = id,
                DepartmentName = dto.DepartmentName,
                Description = dto.Description,
                Status = dto.Status
            };

            var updated = await _repository.UpdateAsync(department);
            if (updated == null)
                throw new NotFoundException("Department not found");

            return updated;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted)
                throw new NotFoundException("Department not found");

            return true;
        }
    }
}
