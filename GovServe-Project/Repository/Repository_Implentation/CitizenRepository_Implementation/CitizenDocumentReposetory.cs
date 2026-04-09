using System;
using GovServe_Project.Data;
using GovServe_Project.Models;
using GovServe_Project.Models.AdminModels;
using GovServe_Project.Models.CitizenModels;
using GovServe_Project.Repository.Interface;
using GovServe_Project.Repository.Interface.CitizenRepository_Interface;
using GovServe_Project.Repository.Interface.CitizenRepository_Interface;
using GovServe_Project.Repository.Repository_Implentation.CitizenRepository_Implementation;
using Microsoft.EntityFrameworkCore;

namespace GovServe_Project.Repository.Repository_Implentation.CitizenRepository_Implementation
{
	public class CitizenDocumentRepository : ICitizenDocumentRepository
	{
		private readonly GovServe_ProjectContext _context;

		public CitizenDocumentRepository(GovServe_ProjectContext context)
		{
			_context = context;
		}

		//Add Documents
		public async Task AddAsync(CitizenDocument document)
		{
			await _context.CitizenDocument.AddAsync(document);
			await _context.SaveChangesAsync();
		}

		//Get My All documents
		public async Task<List<CitizenDocument>> GetMyAllDocuments(int userId)
		{
			return await _context.CitizenDocument
				.Include(d => d.Application)
				.Include(d => d.RequiredDocument)
				.Where(d => d.Application.UserId == userId)
				.ToListAsync();
		}

		//GET Required Documents
		public async Task<List<RequiredDocument>> GetRequiredDocumentsByService(int serviceId)
		{
			return await _context.RequiredDocuments
								 .Where(d => d.ServiceID == serviceId)
								 .ToListAsync();
		}

		//get documents by application id
		public async Task<List<CitizenDocument>> GetDocumentsByApplicationId(int applicationId)
		{
			return await _context.CitizenDocument
				.Where(d => d.ApplicationID == applicationId)
				.ToListAsync();
		}

		// Get By Id
		public async Task<CitizenDocument> GetByIdAsync(int id)
		{
			return await _context.CitizenDocument.FindAsync(id);
		}

		//Update Documents
		public async Task Update(CitizenDocument document)
		{
			_context.CitizenDocument.Update(document);
			await _context.SaveChangesAsync();
		}
		public async Task DeleteAsync(CitizenDocument document)
		{
			_context.CitizenDocument.Remove(document);
			await _context.SaveChangesAsync();
		}

	}
}