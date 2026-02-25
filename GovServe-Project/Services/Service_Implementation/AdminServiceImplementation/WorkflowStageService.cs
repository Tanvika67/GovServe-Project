using GovServe_Project.DTOs;
using GovServe_Project.DTOs.Admin;
using GovServe_Project.Exceptions;
using GovServe_Project.Models;
using GovServe_Project.Models.AdminModels;
using GovServe_Project.Repository.Interface.AdminRepositoryInterface;
using GovServe_Project.Services.Interfaces.AdminServiceInterface;

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

            public async Task<WorkflowStageResponseDto> CreateAsync(WorkflowStageCreateDto dto)
            {
                // Fetch SLA days from SLADays table
                var slaConfig = await _slaDayRepository.GetByRoleAsync(dto.ResponsibleRole);

                if (slaConfig == null)
                    throw new NotFoundException("SLA not configured for this role");

                var stage = new WorkflowStage
                {
                    ServiceID = dto.ServiceID,
                    ResponsibleRole = dto.ResponsibleRole,
                    SequenceNumber = dto.SequenceNumber,
                    SLA_Days = slaConfig.Days   // Auto assign
                };

                await _repository.AddAsync(stage);

                return MapToDto(stage);
            }

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

                await _repository.UpdateAsync(stage);
            }

            public async Task DeleteAsync(int id)
            {
                var stage = await _repository.GetByIdAsync(id)
                    ?? throw new NotFoundException("Workflow stage not found");

                await _repository.DeleteAsync(stage);
            }

            private static WorkflowStageResponseDto MapToDto(WorkflowStage stage)
            {
                return new WorkflowStageResponseDto
                {
                    StageID = stage.StageID,
                    ServiceID = stage.ServiceID,
                    ResponsibleRole = stage.ResponsibleRole,
                    SequenceNumber = stage.SequenceNumber,
                    SLA_Days = stage.SLA_Days
                };
            }
        }
   }




