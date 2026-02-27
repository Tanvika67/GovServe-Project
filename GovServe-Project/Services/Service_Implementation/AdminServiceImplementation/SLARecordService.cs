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

            // Auto-check status (if you truly want to update on read)
            foreach (var record in records)
            {
                UpdateStatus(record);
            }

            // ⚠ Consider removing this save for "query" methods to avoid side effects.
            await _repository.SaveAsync();

            return records.Select(MapToDto);
        }

        public async Task<SLARecordResponseDto> GetByIdAsync(int id)
        {
            var record = await _repository.GetByIdAsync(id)
                ?? throw new NotFoundException("SLA record not found");

            // Auto-check status (if you truly want to update on read)
            UpdateStatus(record);

            // ⚠ Consider removing this save for "query" methods to avoid side effects.
            await _repository.SaveAsync();

            return MapToDto(record);
        }

        public async Task<IEnumerable<SLARecordResponseDto>> GetBreachedCasesAsync()
        {
            // Assumes repository returns up-to-date statuses.
            // If not, you'd need a refresh step before this query.
            var records = await _repository.GetByStatusAsync(SLAStatus.Breached);
            return records.Select(MapToDto);
        }

        public async Task<IEnumerable<SLARecordResponseDto>> GetOnTimeCasesAsync()
        {
            // Assumes repository returns up-to-date statuses.
            var records = await _repository.GetByStatusAsync(SLAStatus.OnTime);
            return records.Select(MapToDto);
        }

        public async Task<SLARecordResponseDto> CreateAsync(SLARecordCreateDto dto)
        {
            // 1) Get workflow stage to fetch SLA days
            var stage = await _stageRepository.GetByIdAsync(dto.StageID)
                ?? throw new NotFoundException("Workflow stage not found");

            // 2) Calculate EndDate automatically (this is actually a SLA due date)
            var calculatedEndDate = dto.StartDate.AddDays(stage.SLA_Days);

            var record = new SLARecord
            {
                CaseID = dto.CaseID,
                StageID = dto.StageID,
                StartDate = dto.StartDate,
                EndDate = calculatedEndDate
            };

            // 3) Calculate Status
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

        // Automatic status update: Breached if now > EndDate (SLA target), else OnTime.
        private void UpdateStatus(SLARecord record)
        {
            // If EndDate is a nullable target, guard for null:
            // if (record.EndDate == null) { record.Status = SLAStatus.OnTime; return; }

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