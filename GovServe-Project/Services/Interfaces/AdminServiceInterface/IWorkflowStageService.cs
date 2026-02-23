using GovServe_Project.DTOs;

namespace GovServe_Project.Services
{
    public interface IWorkflowStageService
    {
        Task<IEnumerable<WorkflowStageResponseDTO>> GetAllAsync();
        Task<WorkflowStageResponseDTO> GetByIdAsync(int id);
        Task<WorkflowStageResponseDTO> CreateAsync(WorkflowStageDTO dto);
        Task<WorkflowStageResponseDTO> UpdateAsync(int id, WorkflowStageDTO dto);
        Task DeleteAsync(int id);
    }
}