using GovServe_Project.DTOs.Admin;
using GovServe_Project.DTOs.AdminDTO;
using GovServe_Project.Exceptions;
using GovServe_Project.Models.AdminModels;
using GovServe_Project.Repository.Interface.AdminRepositoryInterface;
using GovServe_Project.Services.Interfaces.AdminServiceInterface;
using Microsoft.AspNetCore.Mvc;

namespace GovServe_Project.Services.Service_Implementation.AdminServiceImplementation
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repository;

        public DepartmentService(IDepartmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<DepartmentResponseDTO>> GetAllAsync()
        {
            var departments = await _repository.GetAllAsync();

            return departments.Select(d => new DepartmentResponseDTO
            {
                DepartmentID = d.DepartmentID,
                DepartmentName = d.DepartmentName,
                Description = d.Description,
                Status = d.Status
            });
        }

        public async Task<DepartmentResponseDTO> GetByIdAsync(int id)
        {
            var department = await _repository.GetByIdAsync(id);

            if (department == null)
                throw new NotFoundException("Department not found");
            
            return new DepartmentResponseDTO
            {
                DepartmentID = department.DepartmentID,
                DepartmentName = department.DepartmentName,
                Description = department.Description,
                Status = department.Status
            };
        }

        public async Task<IEnumerable<DepartmentResponseDTO>> GetActiveAsync()
        {
            var departments = await _repository.GetActiveAsync();

            return departments.Select(d => new DepartmentResponseDTO
            {
                DepartmentID = d.DepartmentID,
                DepartmentName = d.DepartmentName,
                Description = d.Description,
                Status = d.Status
            });
        }


        public async Task<DepartmentResponseDTO> CreateAsync(DepartmentDTO dto)
        {
            var department = new Department
            {
                DepartmentName = dto.DepartmentName,
                Description = dto.Description,
                Status = dto.Status
            };

            var created = await _repository.AddAsync(department);

            return new DepartmentResponseDTO
            {
                DepartmentID = created.DepartmentID,
                DepartmentName = created.DepartmentName,
                Description = created.Description,
                Status = created.Status
            };
        }

        public async Task<DepartmentResponseDTO> UpdateAsync(int id, DepartmentDTO dto)
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

            return new DepartmentResponseDTO
            {
                DepartmentID = updated.DepartmentID,
                DepartmentName = updated.DepartmentName,
                Description = updated.Description,
                Status = updated.Status
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var deleted = await _repository.DeleteAsync(id);

            if (!deleted)
                throw new NotFoundException("Department not found");

            return true;
        }

        //Get Total Count of Departments
        public async Task<int> GetTotalCountAsync()
        {
            var count = await _repository.GetTotalCountAsync();
            return count;
        }


        }
  }
