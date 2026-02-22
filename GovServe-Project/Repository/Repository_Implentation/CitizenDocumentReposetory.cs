using Microsoft.EntityFrameworkCore;
using GovServe_Project.Data;
using GovServe_Project.Models;
using GovServe_Project.Repository.Interface;

namespace GovServe_Project.Repository.Repository_Implentation
{
	public class CitizenDocumentRepository : ICitizenDocumentRepository
	{
		private readonly GovServe_ProjectContext _context;

		// Inject DbContext
		public CitizenDocumentRepository(GovServe_ProjectContext context)
		{
			_context = context;
		}

		// Insert document
		public async Task Upload(CitizenDocument doc)
		{
			await _context.CitizenDocument.AddAsync(doc);
			await _context.SaveChangesAsync();
		}

		// Get documents by ApplicationId
		public async Task<List<CitizenDocument>> GetByApplication(int applicationId)
		{
			return await _context.CitizenDocument
				.Where(x => x.ApplicationID == applicationId)
				.ToListAsync();
		}

		// Get document by Id
		public async Task<CitizenDocument> GetById(int id)
		{
			return await _context.CitizenDocument.FindAsync(id);
		}

		// Update document
		public async Task Update(CitizenDocument doc)
		{
			_context.CitizenDocument.Update(doc);
			await _context.SaveChangesAsync();
		}

		// Delete document
		public async Task Delete(CitizenDocument doc)
		{
			_context.CitizenDocument.Remove(doc);
			await _context.SaveChangesAsync();
		}
	}
}
