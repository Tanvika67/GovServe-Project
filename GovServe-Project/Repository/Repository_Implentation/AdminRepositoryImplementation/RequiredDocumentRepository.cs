using GovServe_Project.Data;
using GovServe_Project.Models.AdminModels;
using GovServe_Project.Repository.Interface.AdminRepositoryInterface;
using Microsoft.EntityFrameworkCore;

namespace GovServe_Project.Repository.Repository_Implentation.AdminRepositoryImplementation
{
    public class RequiredDocumentRepository : IRequiredDocumentRepository
    {
        private readonly GovServe_ProjectContext _context;

        public RequiredDocumentRepository(GovServe_ProjectContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RequiredDocument>> GetAllAsync()
        {

            return await _context.RequiredDocuments
                    .Include(d => d.Service)
                    .ToListAsync();

        }

        public async Task<RequiredDocument?> GetByIdAsync(int id)
        {

            return await _context.RequiredDocuments
                    .Include(d => d.Service)
                    .FirstOrDefaultAsync(d => d.DocumentID == id);

        }

        public async Task AddAsync(RequiredDocument document)
        {
            await _context.RequiredDocuments.AddAsync(document);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RequiredDocument document)
        {
            // Entity already tracked from GetByIdAsync
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(RequiredDocument document)
        {
            _context.RequiredDocuments.Remove(document);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RequiredDocument>> GetByServiceNameAsync(string serviceName)
        {
            return await _context.RequiredDocuments
                .Include(d => d.Service)
                .Where(d => d.Service != null &&
                            d.Service.ServiceName.ToLower()
                            .Contains(serviceName.ToLower()))
                .ToListAsync();
        }

        //For citizen
        public async Task<IEnumerable<RequiredDocument>> GetByServiceIdAsync(int serviceId)

        {

          return await _context.RequiredDocuments

           .Include(d => d.Service)

         .Where(d => d.ServiceID == serviceId)

         .ToListAsync();

    }
}
}