using System;
using GovServe_Project.Data;
using GovServe_Project.Models;
using GovServe_Project.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using GovServe_Project.Repository.Interface.CitizenRepository_Interface;
using GovServe_Project.Repository.Repository_Implentation.CitizenRepository_Implementation;
using GovServe_Project.Repository.Interface.CitizenRepository_Interface;
using GovServe_Project.Models.CitizenModels;

namespace GovServe_Project.Repository.Repository_Implentation.CitizenRepository_Implementation
{
	public class CitizenDocumentRepository : ICitizenDocumentRepository
	{
		private readonly GovServe_ProjectContext _context;

		public CitizenDocumentRepository(GovServe_ProjectContext context)
		{
			_context = context;
		}

		public async Task AddAsync(CitizenDocument document)
		{
			await _context.CitizenDocument.AddAsync(document);
			await _context.SaveChangesAsync();
		}

		public async Task<List<CitizenDocument>> GetMyAllDocuments(int userId)
		{
			return await _context.CitizenDocument
				.Include(d => d.Application)
				.Where(d => d.Application.UserId == userId)
				.ToListAsync();
		}

		public async Task<List<CitizenDocument>> GetDocumentsByApplicationId(int applicationId)
		{
			return await _context.CitizenDocument
				.Where(d => d.ApplicationID == applicationId)
				.ToListAsync();
		}

		public async Task<CitizenDocument> GetByIdAsync(int id)
		{
			return await _context.CitizenDocument.FindAsync(id);
		}

		public async Task DeleteAsync(CitizenDocument document)
		{
			_context.CitizenDocument.Remove(document);
			await _context.SaveChangesAsync();
		}

		public async Task Update(CitizenDocument document)
		{
			_context.CitizenDocument.Update(document);
			await _context.SaveChangesAsync();
		}


	}
}