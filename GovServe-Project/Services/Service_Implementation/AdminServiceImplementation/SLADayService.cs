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
            var list = await _repository.GetAllAsync();

            return list.Select(x => new SLADayResponseDto
            {
                SLADayID = x.SLADayID,
                RoleName = x.RoleName,
                Days = x.Days
            });
        }

        public async Task<SLADayResponseDto> GetByIdAsync(int id)
        {
            var slaDay = await _repository.GetByIdAsync(id)
                ?? throw new NotFoundException("SLA configuration not found");

            return new SLADayResponseDto
            {
                SLADayID = slaDay.SLADayID,
                RoleName = slaDay.RoleName,
                Days = slaDay.Days
            };
        }

        public async Task<SLADayResponseDto> CreateAsync(SLADayCreateDto dto)
        {
            var existing = await _repository.GetByRoleAsync(dto.RoleName);
            //if (existing != null)
            //    throw new BadRequestException("SLA already configured for this role");

            var slaDay = new SLADays
            {
                RoleName = dto.RoleName,
                Days = dto.Days
            };

            await _repository.AddAsync(slaDay);
            return await GetByIdAsync(slaDay.SLADayID);

            return new SLADayResponseDto
            {
                SLADayID = slaDay.SLADayID,
                RoleName = slaDay.RoleName,
                Days = slaDay.Days
            };
        }

        public async Task<SLADayResponseDto> UpdateAsync(int id, SLADayCreateDto dto)
        {
            var slaDay = await _repository.GetByIdAsync(id)
                ?? throw new NotFoundException("SLA configuration not found");

            slaDay.RoleName = dto.RoleName;
            slaDay.Days = dto.Days;

            await _repository.UpdateAsync(slaDay);
            return await GetByIdAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            var slaDay = await _repository.GetByIdAsync(id)
                ?? throw new NotFoundException("SLA configuration not found");

            await _repository.DeleteAsync(slaDay);
        }
    }


}
