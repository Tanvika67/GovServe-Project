using GovServe_Project.DTOs;
using GovServe_Project.DTOs.AdminDTO;
using GovServe_Project.Exceptions;
using GovServe_Project.Models.AdminModels;
using GovServe_Project.Repository.Interface.AdminRepositoryInterface;
using GovServe_Project.Services.Interfaces.AdminServiceInterface;

namespace GovServe_Project.Services.Service_Implementation.AdminServiceImplementation
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repository;

        public RoleService(IRoleRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<RoleResponseDto>> GetAllAsync()
        {
            var roles = await _repository.GetAllAsync();

            return roles.Select(r => new RoleResponseDto
            {
                RoleID = r.RoleID,
                RoleName = r.RoleName
            });
        }

        public async Task<RoleResponseDto> GetByIdAsync(int id)
        {
            var role = await _repository.GetByIdAsync(id)
                ?? throw new NotFoundException("Role not found");

            return new RoleResponseDto
            {
                RoleID = role.RoleID,
                RoleName = role.RoleName
            };
        }

        public async Task<RoleResponseDto> CreateAsync(RoleCreateDto dto)
        {
            var existing = await _repository.GetByNameAsync(dto.RoleName);
            if (existing != null)
               throw new BadRequestException("Role already exists");

            var role = new Role
            {
                RoleName = dto.RoleName
            };

            await _repository.AddAsync(role);
            return await GetByIdAsync(role.RoleID);

            return new RoleResponseDto
            {
                RoleID = role.RoleID,
                RoleName = role.RoleName
            };
        }

        public async Task<RoleResponseDto> UpdateAsync(int id, RoleCreateDto dto)
        {
            var role = await _repository.GetByIdAsync(id)
                ?? throw new NotFoundException("Role not found");

            role.RoleName = dto.RoleName;

            await _repository.UpdateAsync(role);
             return await GetByIdAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            var role = await _repository.GetByIdAsync(id)
                ?? throw new NotFoundException("Role not found");

            await _repository.DeleteAsync(role);
        }
    }

}
