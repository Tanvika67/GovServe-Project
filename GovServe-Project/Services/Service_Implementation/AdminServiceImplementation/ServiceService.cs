using GovServe_Project.DTOs.Admin;
using GovServe_Project.Exceptions;
using GovServe_Project.Models;
using GovServe_Project.Models.AdminModels;
using GovServe_Project.Repository.Interface.AdminRepositoryInterface;
using GovServe_Project.Services.Interfaces.AdminServiceInterface;


namespace GovServe_Project.Services.Service_Implementation.AdminServiceImplementation
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _repository;

        public ServiceService(IServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ServiceResponseDTO>> GetAllAsync()
        {
            var services = await _repository.GetAllAsync();

            return services.Select(s => new ServiceResponseDTO
            {
                ServiceID = s.ServiceID,
                DepartmentID = s.DepartmentID,
                ServiceName = s.ServiceName,
                Description = s.Description,
                SLA_Days = s.SLA_Days,
                Status = s.Status,
               
            });
        }

        public async Task<ServiceResponseDTO> GetByIdAsync(int id)
        {
            var service = await _repository.GetByIdAsync(id);

            if (service == null)
                throw new NotFoundException("Service not found.");

            return new ServiceResponseDTO
            {
                ServiceID = service.ServiceID,
                DepartmentID = service.DepartmentID,
                ServiceName = service.ServiceName,
                Description = service.Description,
                SLA_Days = service.SLA_Days,
                Status = service.Status,
               
            };
        }

        public async Task<ServiceResponseDTO> CreateAsync(ServiceDTO dto)
        {
            var service = new Service
            {
                DepartmentID = dto.DepartmentID,
                ServiceName = dto.ServiceName,
                Description = dto.Description,
                SLA_Days = dto.SLA_Days,
                Status = dto.Status
            };

            await _repository.AddAsync(service);

            return await GetByIdAsync(service.ServiceID);
        }

        public async Task<ServiceResponseDTO> UpdateAsync(int id, ServiceDTO dto)
        {
            var service = await _repository.GetByIdAsync(id);

            if (service == null)
                throw new NotFoundException("Service not found.");

            service.DepartmentID = dto.DepartmentID;
            service.ServiceName = dto.ServiceName;
            service.Description = dto.Description;
            service.SLA_Days = dto.SLA_Days;
            service.Status = dto.Status;

            await _repository.UpdateAsync(service);

            return await GetByIdAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            var service = await _repository.GetByIdAsync(id);

            if (service == null)
                throw new NotFoundException("Service not found.");

            await _repository.DeleteAsync(service);
        }
    }
}
