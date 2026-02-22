using GovServe_Project.Data;
using GovServe_Project.Models.AdminModels;
using GovServe_Project.Repository.Interface.AdminRepositoryInterface;
using Microsoft.EntityFrameworkCore;

namespace GovServe_Project.Repository.Repository_Implentation.Admin
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
            return await _context.RequiredDocuments.ToListAsync();
        }

        public async Task<RequiredDocument?> GetByIdAsync(int id)
        {
            return await _context.RequiredDocuments.FindAsync(id);
        }

        public async Task AddAsync(RequiredDocument document)
        {
            await _context.RequiredDocuments.AddAsync(document);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RequiredDocument document)
        {
            _context.RequiredDocuments.Update(document);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(RequiredDocument document)
        {
            _context.RequiredDocuments.Remove(document);
            await _context.SaveChangesAsync();
        }
    }
}