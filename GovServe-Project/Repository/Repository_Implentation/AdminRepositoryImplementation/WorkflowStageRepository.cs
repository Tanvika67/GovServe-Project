using GovServe_Project.Data;
using GovServe_Project.Models;
using GovServe_Project.Repositories.Interface.AdminRepositoryInterface;
using Microsoft.EntityFrameworkCore;

namespace GovServe_Project.Repository.Interface.AdminRepositoryInterface
{
    public class WorkflowStageRepository : IWorkflowStageRepository
    {
        private readonly GovServe_ProjectContext _context;

        public WorkflowStageRepository(GovServe_ProjectContext context)
        {
            _context = context;
        }

        // Get all stages ordered by SequenceNumber
        public async Task<IEnumerable<WorkflowStage>> GetAllAsync()
            =>await _context.WorkflowStages
        .Include(w => w.Service)    // ⭐ add this
        .OrderBy(x => x.SequenceNumber)
        .ToListAsync();


        // Get stages for a specific service
        public async Task<IEnumerable<WorkflowStage>> GetByServiceAsync(int serviceId)
            => await _context.WorkflowStages
                .Include(w => w.Service)
                .Where(x => x.ServiceID == serviceId)
                .OrderBy(x => x.SequenceNumber)
                .ToListAsync();

        // Get stage by ID
        public async Task<WorkflowStage?> GetByIdAsync(int id)
            =>await _context.WorkflowStages
        .Include(w => w.Service)    // ⭐ add this
        .FirstOrDefaultAsync(w => w.StageID == id);


        // Add new stage
        public async Task AddAsync(WorkflowStage stage)
            => await _context.WorkflowStages.AddAsync(stage);

        // Update stage
        public void Update(WorkflowStage stage)
            => _context.WorkflowStages.Update(stage);

        // Delete stage
        public void Delete(WorkflowStage stage)
            => _context.WorkflowStages.Remove(stage);

        // Save changes to database
        public async Task SaveAsync()
            => await _context.SaveChangesAsync();
    }
}

