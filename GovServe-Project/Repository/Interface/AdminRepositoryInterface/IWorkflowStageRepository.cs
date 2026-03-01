using GovServe_Project.Models;

namespace GovServe_Project.Repositories.Interface.AdminRepositoryInterface
{
    public interface IWorkflowStageRepository
    {
        Task<IEnumerable<WorkflowStage>> GetAllAsync(); // Get all workflow stages
        Task<IEnumerable<WorkflowStage>> GetByServiceAsync(int serviceId); // Get stages for a specific service
        Task<WorkflowStage?> GetByIdAsync(int id); // Get single stage
        Task AddAsync(WorkflowStage stage); // Add new stage
        void Update(WorkflowStage stage); // Update existing stage
        void Delete(WorkflowStage stage); // Delete stage
        Task SaveAsync(); // Save changes
    }
}

