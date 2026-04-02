using GovServe_Project.DTOs.AdminDTO;
using GovServe_Project.Enum;
using GovServe_Project.Exceptions;
using GovServe_Project.Models.AdminModels;
using GovServe_Project.Repositories.Interface.AdminRepositoryInterface;
using GovServe_Project.Repository.Interface.AdminRepositoryInterface;
using GovServe_Project.Repository.Interface.SuperRepositoryInterface;
using GovServe_Project.Services.Interfaces.AdminServiceInterface;

namespace GovServe_Project.Services.Service_Implementation.AdminServiceImplementation
{
    public class SLARecordService : ISLARecordService
    {
        private readonly ISLARecordRepository _repository;
        private readonly IWorkflowStageRepository _stageRepository;
        private readonly ICaseRepository _caseRepository;

		public SLARecordService(
            ISLARecordRepository repository,
            IWorkflowStageRepository stageRepository,
            ICaseRepository caseRepository)
        {
            _repository = repository;
            _stageRepository = stageRepository;
            _caseRepository = caseRepository;
		}

        public async Task<IEnumerable<SLARecordResponseDto>> GetAllAsync()
        {
            var records = await _repository.GetAllAsync();

            // Auto-check status (if you truly want to update on read)
            foreach (var record in records)
            {
                UpdateStatus(record);
            }

            return records.Select(MapToDto);
        }

        public async Task<SLARecordResponseDto> GetByIdAsync(int id)
        {
            var record = await _repository.GetByIdAsync(id)
                ?? throw new NotFoundException("SLA record not found");

            // Auto-check status (if you truly want to update on read)
            UpdateStatus(record);
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

			// 2) Calculate EndDate automatically
			var calculatedEndDate = dto.StartDate.AddDays(stage.SLA_Days);

			var record = new SLARecords
			{
				CaseId = dto.CaseID,
				StageID = dto.StageID,
				StartDate = dto.StartDate,
				EndDate = calculatedEndDate
			};

			// 3) Calculate SLA status and update case
			await UpdateStatus(record);

			// 4) Save SLA record
			await _repository.AddAsync(record);
			//await _repository.SaveAsync();

			return MapToDto(record);
		}


		public async Task DeleteAsync(int id)
        {
            var record = await _repository.GetByIdAsync(id)
                ?? throw new NotFoundException("SLA record not found");

            await _repository.DeleteAsync(record);
        }

		private async Task UpdateStatus(SLARecords record)
		{
			if (DateTime.UtcNow > record.EndDate)
				record.Status = SLAStatus.Breached;
			else
				record.Status = SLAStatus.OnTime;

			// Update case table
			var caseObj = await _caseRepository.GetByIdAsync(record.CaseId);

			if (caseObj != null)
			{
				if (record.Status == SLAStatus.Breached)
					caseObj.Status = "Escalated";
				else
					caseObj.Status = "Assigned";

				_caseRepository.Update(caseObj);
				await _caseRepository.SaveAsync();
			}
		}

		private static SLARecordResponseDto MapToDto(SLARecords record)
        {
            return new SLARecordResponseDto
            {
                SLARecordID = record.SLARecordID,
                CaseID = record.CaseId,
                StageID = record.StageID,
                StartDate = record.StartDate,
                EndDate = record.EndDate,
                Status = record.Status
            };
        }

        public async Task<IEnumerable<PendingSlaCaseDto>> GetPendingSlaCasesAsync()
        {
            var cases = await _repository.GetCasesWithoutSLAAsync();

            return cases.Select(c => new PendingSlaCaseDto
            {
                CaseId = c.CaseId,
                ApplicationNumber = $"APP-{c.ApplicationID}",     // ✅ derived
                ServiceName = c.Application?.ServiceName ?? "",   // ✅ FIX HERE
                DepartmentName = c.Department?.DepartmentName ?? "",
                OfficerName = c.AssignedOfficer != null
                 ? c.AssignedOfficer.FullName
                 : "Unassigned",

                OfficerDepartment = c.AssignedOfficer?.Department?.DepartmentName ?? "",
                Status = c.Status,
                LastUpdated = c.LastUpdated
            });
        }


    }
}