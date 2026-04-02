using GovServe_Project.DTOs.AdminDTO;
using GovServe_Project.Exceptions;
using GovServe_Project.Models.AdminModels;
using GovServe_Project.Repository.Interface.AdminRepositoryInterface;
using GovServe_Project.Services.Interfaces.AdminServiceInterface;

namespace GovServe_Project.Services.Service_Implementation.AdminServiceImplementation
{
    public class SLADayService : ISLADayService
    {
        private readonly ISLADayRepository _repository;

        public SLADayService(ISLADayRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SLADayResponseDto>> GetAllAsync()
        {
            var slaDays = await _repository.GetAllAsync();

            return slaDays.Select(x => new SLADayResponseDto
            {
                SLADayID = x.SLADayID,
                ServiceName = x.Service.ServiceName,
                RoleName = x.Role.RoleName,
                Days = x.Days
            });
        }

        public async Task<SLADayResponseDto> GetByIdAsync(int id)
        {
            var sla = await _repository.GetByIdAsync(id)
                ?? throw new NotFoundException("SLA Day not found");

            return new SLADayResponseDto
            {
                SLADayID = sla.SLADayID,
                ServiceName = sla.Service.ServiceName,
                RoleName = sla.Role.RoleName,
                Days = sla.Days
            };
        }

        public async Task<SLADayResponseDto> CreateAsync(SLADayCreateDto dto)
        {
            var existing = await _repository
                .GetByServiceAndRoleAsync(dto.ServiceID, dto.RoleID);

            if (existing != null)
                throw new BadRequestException("SLA already exists for this service and role");

            var slaDay = new SLADays
            {
                ServiceID = dto.ServiceID,
                RoleID = dto.RoleID,
                Days = dto.Days
            };

            await _repository.AddAsync(slaDay);
            return await GetByIdAsync(slaDay.SLADayID);
        }

        public async Task<SLADayResponseDto> UpdateAsync(int id, SLADayCreateDto dto)
        {
            var slaDay = await _repository.GetByIdAsync(id)
                ?? throw new NotFoundException("SLA Day not found");

            slaDay.ServiceID = dto.ServiceID;
            slaDay.RoleID = dto.RoleID;
            slaDay.Days = dto.Days;

            await _repository.UpdateAsync(slaDay);
            return await GetByIdAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            var slaDay = await _repository.GetByIdAsync(id)
                ?? throw new NotFoundException("SLA Day not found");

            await _repository.DeleteAsync(slaDay);
        }
    }
}
