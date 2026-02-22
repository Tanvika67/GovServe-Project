using GovServe_Project.DTOs;
using GovServe_Project.Exceptions;
using GovServe_Project.Models;
using GovServe_Project.Models.AdminModels;
using GovServe_Project.Repositories;
using GovServe_Project.Repository.Interface.AdminRepositoryInterface;

namespace GovServe_Project.Services
{
    public class WorkflowStageService : IWorkflowStageService
    {
        private readonly IWorkflowStageRepository _repository;

        public WorkflowStageService(IWorkflowStageRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<WorkflowStageResponseDTO>> GetAllAsync()
        {
            var stages = await _repository.GetAllAsync();

            return stages.Select(s => new WorkflowStageResponseDTO
            {
                StageID = s.StageID,
                ServiceID = s.ServiceID,
                ResponsibleRole = s.ResponsibleRole,
                SequenceNumber = s.SequenceNumber,
                SLA_Days = s.SLA_Days
            });
        }

        public async Task<WorkflowStageResponseDTO> GetByIdAsync(int id)
        {
            var stage = await _repository.GetByIdAsync(id);

            if (stage == null)
                throw new NotFoundException("Workflow stage not found.");

            return new WorkflowStageResponseDTO
            {
                StageID = stage.StageID,
                ServiceID = stage.ServiceID,
                ResponsibleRole = stage.ResponsibleRole,
                SequenceNumber = stage.SequenceNumber,
                SLA_Days = stage.SLA_Days
            };
        }

        public async Task<WorkflowStageResponseDTO> CreateAsync(WorkflowStageDTO dto)
        {
            var stage = new WorkflowStage
            {
                ServiceID = dto.ServiceID,
                ResponsibleRole = dto.ResponsibleRole,
                SequenceNumber = dto.SequenceNumber,
                SLA_Days = dto.SLA_Days
            };

            await _repository.AddAsync(stage);

            return await GetByIdAsync(stage.StageID);
        }

        public async Task<WorkflowStageResponseDTO> UpdateAsync(int id, WorkflowStageDTO dto)
        {
            var stage = await _repository.GetByIdAsync(id);

            if (stage == null)
                throw new NotFoundException("Workflow stage not found.");

            stage.ServiceID = dto.ServiceID;
            stage.ResponsibleRole = dto.ResponsibleRole;
            stage.SequenceNumber = dto.SequenceNumber;
            stage.SLA_Days = dto.SLA_Days;

            await _repository.UpdateAsync(stage);

            return await GetByIdAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            var stage = await _repository.GetByIdAsync(id);

            if (stage == null)
                throw new NotFoundException("Workflow stage not found.");

            await _repository.DeleteAsync(stage);
        }
    }
}
