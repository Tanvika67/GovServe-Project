using GovServe_Project.DTOs;
using GovServe_Project.DTOs.Admin.GovServe_Project.DTOs;

namespace GovServe_Project.Services_Interfaces_AdminServiceInterface
{
    public interface IWorkflowStageService
    {
        Task<IEnumerable<WorkflowStageResponseDto>> GetAllAsync();
        Task<IEnumerable<WorkflowStageResponseDto>> GetByServiceAsync(int serviceId);
        Task<WorkflowStageResponseDto> GetByIdAsync(int id);
        Task<WorkflowStageResponseDto> CreateAsync(WorkflowStageCreateDto dto);
        Task UpdateAsync(int id, WorkflowStageCreateDto dto);
        Task DeleteAsync(int id);
        Task<WorkflowStageResponseDto> ReassignAsync(int stageId, WorkflowStageReassignDto dto);
    }
}
