
using GovServe_Project.Models.AdminModels;

namespace GovServe_Project.Repository.Interface.AdminRepositoryInterface
{
    public interface IWorkflowStageRepository
    {
        Task<IEnumerable<WorkflowStage>> GetAllAsync();
        Task<WorkflowStage?> GetByIdAsync(int id);
        Task AddAsync(WorkflowStage stage);
        Task UpdateAsync(WorkflowStage stage);
        Task DeleteAsync(WorkflowStage stage);
    }
}