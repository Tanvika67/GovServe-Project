using GovServe_Project.DTOs;
using GovServe_Project.DTOs.Admin.GovServe_Project.DTOs;
using GovServe_Project.Exceptions;
using GovServe_Project.Models;
using GovServe_Project.Repositories;
using GovServe_Project.Repositories.Interface.AdminRepositoryInterface;
using GovServe_Project.Repository.Interface.AdminRepositoryInterface;
using GovServe_Project.Services_Interfaces_AdminServiceInterface;

namespace GovServe_Project.Services.Service_Implementation.AdminServiceImplementation
{
    public class WorkflowStageService : IWorkflowStageService
    {
        private readonly IWorkflowStageRepository _repository;
        private readonly ISLADayRepository _slaDayRepository; // Used to fetch SLA days per role

        public WorkflowStageService(
            IWorkflowStageRepository repository,
            ISLADayRepository slaDayRepository)
        {
            _repository = repository;
            _slaDayRepository = slaDayRepository;
        }

        // Get all stages
        public async Task<IEnumerable<WorkflowStageResponseDto>> GetAllAsync()
        {
            var stages = await _repository.GetAllAsync();
            return stages.Select(MapToDto);
        }

        // Get stages for a service
        public async Task<IEnumerable<WorkflowStageResponseDto>> GetByServiceAsync(int serviceId)
        {
            var stages = await _repository.GetByServiceAsync(serviceId);
            return stages.Select(MapToDto);
        }

        // Get stage by ID
        public async Task<WorkflowStageResponseDto> GetByIdAsync(int id)
        {
            var stage = await _repository.GetByIdAsync(id)
                ?? throw new NotFoundException("Workflow stage not found");
            return MapToDto(stage);
        }

        // Create new stage
        public async Task<WorkflowStageResponseDto> CreateAsync(WorkflowStageCreateDto dto)
        {
            //  Fetch SLA days automatically
            var slaConfig = await _slaDayRepository.GetByRoleAsync(dto.ResponsibleRole)
                ?? throw new NotFoundException("SLA not configured for this role");

            var stage = new WorkflowStage
            {
                ServiceID = dto.ServiceID,
                ResponsibleRole = dto.ResponsibleRole,
                SequenceNumber = dto.SequenceNumber,
                SLA_Days = slaConfig.Days
            };

            await _repository.AddAsync(stage);
            await _repository.SaveAsync();

            return MapToDto(stage);
        }

        // Update stage
        public async Task UpdateAsync(int id, WorkflowStageCreateDto dto)
        {
            var stage = await _repository.GetByIdAsync(id)
                ?? throw new NotFoundException("Workflow stage not found");

            var slaConfig = await _slaDayRepository.GetByRoleAsync(dto.ResponsibleRole)
                ?? throw new NotFoundException("SLA not configured for this role");

            stage.ServiceID = dto.ServiceID;
            stage.ResponsibleRole = dto.ResponsibleRole;
            stage.SequenceNumber = dto.SequenceNumber;
            stage.SLA_Days = slaConfig.Days;

            _repository.Update(stage);
            await _repository.SaveAsync();
        }

        // Delete stage
        public async Task DeleteAsync(int id)
        {
            var stage = await _repository.GetByIdAsync(id)
                ?? throw new NotFoundException("Workflow stage not found");

            _repository.Delete(stage);
            await _repository.SaveAsync();
        }

        // Reassign stage to a new role
        public async Task<WorkflowStageResponseDto> ReassignAsync(int stageId, WorkflowStageReassignDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.NewResponsibleRole))
                throw new BadRequestException("New role is required.");

            var stage = await _repository.GetByIdAsync(stageId)
                ?? throw new NotFoundException("Workflow stage not found.");

            var slaConfig = await _slaDayRepository.GetByRoleAsync(dto.NewResponsibleRole)
                ?? throw new NotFoundException("SLA not configured for this role.");

            stage.ResponsibleRole = dto.NewResponsibleRole;
            stage.SLA_Days = slaConfig.Days;

            _repository.Update(stage);
            await _repository.SaveAsync();

            return MapToDto(stage);
        }

        // Helper to map model -> DTO
        private static WorkflowStageResponseDto MapToDto(WorkflowStage stage)
        {
            return new WorkflowStageResponseDto
            {
                StageID = stage.StageID,
                ServiceName = stage.Service?.ServiceName ?? "",
                ResponsibleRole = stage.ResponsibleRole,
                SequenceNumber = stage.SequenceNumber,
                SLA_Days = stage.SLA_Days
            };
        }
    }
}

