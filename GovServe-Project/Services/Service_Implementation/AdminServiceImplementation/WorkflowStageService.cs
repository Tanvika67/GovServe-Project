using GovServe_Project.DTOs;
using GovServe_Project.Exceptions;
using GovServe_Project.Models;
using GovServe_Project.Repositories.Interface.AdminRepositoryInterface;
using GovServe_Project.Repository.Interface.AdminRepositoryInterface;
using GovServe_Project.Services_Interfaces_AdminServiceInterface;

namespace GovServe_Project.Services.Service_Implementation.AdminServiceImplementation
{
    public class WorkflowStageService : IWorkflowStageService
    {
        private readonly IWorkflowStageRepository _repository;
        private readonly ISLADayRepository _slaDayRepository;

        public WorkflowStageService(
            IWorkflowStageRepository repository,
            ISLADayRepository slaDayRepository)
        {
            _repository = repository;
            _slaDayRepository = slaDayRepository;
        }

        public async Task<IEnumerable<WorkflowStageResponseDto>> GetAllAsync()
        {
            var stages = await _repository.GetAllAsync();
            return stages.Select(MapToDto);
        }

        public async Task<IEnumerable<WorkflowStageResponseDto>> GetByServiceAsync(int serviceId)
        {
            var stages = await _repository.GetByServiceAsync(serviceId);
            return stages.Select(MapToDto);
        }

        public async Task<WorkflowStageResponseDto> GetByIdAsync(int id)
        {
            var stage = await _repository.GetByIdAsync(id)
                ?? throw new NotFoundException("Workflow stage not found");

            return MapToDto(stage);
        }

        // ✅ CREATE (AUTO-SLA)
        public async Task<WorkflowStageResponseDto> CreateAsync(WorkflowStageCreateDto dto)
        {
            var slaConfig = await _slaDayRepository
                .GetByServiceAndRoleAsync(dto.ServiceID, dto.RoleID)
                ?? throw new NotFoundException(
                    "SLA not configured for this service and role");

            var stage = new WorkflowStage
            {
                ServiceID = dto.ServiceID,
                ResponsibleRoleID = dto.RoleID,
                SequenceNumber = dto.SequenceNumber,
                SLA_Days = slaConfig.Days
            };

            await _repository.AddAsync(stage);
            await _repository.SaveAsync();

            return MapToDto(stage);
        }

        public async Task UpdateAsync(int id, WorkflowStageCreateDto dto)
        {
            var stage = await _repository.GetByIdAsync(id)
                ?? throw new NotFoundException("Workflow stage not found");

            var slaConfig = await _slaDayRepository
                .GetByServiceAndRoleAsync(dto.ServiceID, dto.RoleID)
                ?? throw new NotFoundException(
                    "SLA not configured for this service and role");

            stage.ServiceID = dto.ServiceID;
            stage.ResponsibleRoleID = dto.RoleID;
            stage.SequenceNumber = dto.SequenceNumber;
            stage.SLA_Days = slaConfig.Days;

            _repository.Update(stage);
            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var stage = await _repository.GetByIdAsync(id)
                ?? throw new NotFoundException("Workflow stage not found");

            _repository.Delete(stage);
            await _repository.SaveAsync();
        }

        // ✅ REASSIGN (AUTO-SLA)
        public async Task<WorkflowStageResponseDto> ReassignAsync(int stageId, WorkflowStageReassignDto dto)
        {
            var stage = await _repository.GetByIdAsync(stageId)
                ?? throw new NotFoundException("Workflow stage not found");

            var slaConfig = await _slaDayRepository
                .GetByServiceAndRoleAsync(stage.ServiceID, dto.NewRoleID)
                ?? throw new NotFoundException(
                    "SLA not configured for this service and role");

            stage.ResponsibleRoleID = dto.NewRoleID;
            stage.SLA_Days = slaConfig.Days;

            _repository.Update(stage);
            await _repository.SaveAsync();

            return MapToDto(stage);
        }

        private static WorkflowStageResponseDto MapToDto(WorkflowStage stage)
        {
            return new WorkflowStageResponseDto
            {
                StageID = stage.StageID,
                ServiceName = stage.Service?.ServiceName ?? "",
                ResponsibleRole = stage.Role?.RoleName ?? "",
                SequenceNumber = stage.SequenceNumber,
                SLA_Days = stage.SLA_Days
            };
        }
    }
}
