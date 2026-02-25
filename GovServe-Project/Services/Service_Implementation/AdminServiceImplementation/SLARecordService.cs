
using GovServe_Project.DTOs.AdminDTO;
using GovServe_Project.Enum;
using GovServe_Project.Exceptions;
using GovServe_Project.Models.AdminModels;
using GovServe_Project.Repository.Interface.AdminRepositoryInterface;
using GovServe_Project.Services.Interfaces.AdminServiceInterface;

namespace GovServe_Project.Services.Service_Implementation.AdminServiceImplementation
{
    public class SLARecordService : ISLARecordService
    {
        private readonly ISLARecordRepository _repository;
        private readonly IWorkflowStageRepository _stageRepository;

        public SLARecordService(
            ISLARecordRepository repository,
            IWorkflowStageRepository stageRepository)
        {
            _repository = repository;
            _stageRepository = stageRepository;
        }

        public async Task<IEnumerable<SLARecordResponseDto>> GetAllAsync()
        {
            var records = await _repository.GetAllAsync();

            // Auto check status
            foreach (var record in records)
            {
                UpdateStatus(record);
            }

            await _repository.SaveAsync();

            return records.Select(MapToDto);
        }

        public async Task<SLARecordResponseDto> GetByIdAsync(int id)
        {
            var record = await _repository.GetByIdAsync(id)
                ?? throw new NotFoundException("SLA record not found");

            UpdateStatus(record);
            await _repository.SaveAsync();

            return MapToDto(record);
        }

        public async Task<SLARecordResponseDto> CreateAsync(SLARecordCreateDto dto)
        {
            // 1️⃣ Get workflow stage to fetch SLA days
            var stage = await _stageRepository.GetByIdAsync(dto.StageID)
                ?? throw new NotFoundException("Workflow stage not found");

            // 2️⃣ Calculate EndDate automatically
            var calculatedEndDate = dto.StartDate.AddDays(stage.SLA_Days);

            var record = new SLARecord
            {
                CaseID = dto.CaseID,
                StageID = dto.StageID,
                StartDate = dto.StartDate,
                EndDate = calculatedEndDate
            };

            // 3️⃣ Calculate Status
            UpdateStatus(record);

            await _repository.AddAsync(record);
            await _repository.SaveAsync();

            return MapToDto(record);
        }

        public async Task DeleteAsync(int id)
        {
            var record = await _repository.GetByIdAsync(id)
                ?? throw new NotFoundException("SLA record not found");

            _repository.Delete(record);
            await _repository.SaveAsync();
        }

        //  Automatic Status updated
        private void UpdateStatus(SLARecord record)
        {
            if (DateTime.UtcNow > record.EndDate)
                record.Status = SLAStatus.Breached;
            else
                record.Status = SLAStatus.OnTime;
        }

        private static SLARecordResponseDto MapToDto(SLARecord record)
        {
            return new SLARecordResponseDto
            {
                SLARecordID = record.SLARecordID,
                CaseID = record.CaseID,
                StageID = record.StageID,
                StartDate = record.StartDate,
                EndDate = record.EndDate,
                Status = record.Status
            };
        }
    }


}
