using GovServe_Project.Data;
using GovServe_Project.Models.AdminModels;
using GovServe_Project.Repository.Interface.AdminRepositoryInterface;
using Microsoft.EntityFrameworkCore;

namespace GovServe_Project.Repositories
{
    public class WorkflowStageRepository : IWorkflowStageRepository
    {
        private readonly GovServe_ProjectContext _context;

        public WorkflowStageRepository(GovServe_ProjectContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WorkflowStage>> GetAllAsync()
        {
            return await _context.WorkflowStages.ToListAsync();
        }

        public async Task<WorkflowStage?> GetByIdAsync(int id)
        {
            return await _context.WorkflowStages.FindAsync(id);
        }

        public async Task AddAsync(WorkflowStage stage)
        {
            await _context.WorkflowStages.AddAsync(stage);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(WorkflowStage stage)
        {
            _context.WorkflowStages.Update(stage);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(WorkflowStage stage)
        {
            _context.WorkflowStages.Remove(stage);
            await _context.SaveChangesAsync();
        }
    }
}