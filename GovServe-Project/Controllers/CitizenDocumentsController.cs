using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GovServe_Project.Data;
using GovServe_Project.Models;
using GovServe_Project.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GovServe_Project.Extensions;

namespace GovServe_Project.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CitizenDocumentController : ControllerBase
	{
		private readonly ICitizenDocumentService _service;

		public CitizenDocumentController(ICitizenDocumentService service)
		{
			_service = service;
		}

		// Upload Document
		[HttpPost("upload")]
		public async Task<IActionResult> Upload(int applicationId, string documentName, IFormFile file)
		{
			// Call extension validation
			if (!file.IsValidDocument())
				throw new Exception("Invalid file. Only jpg, png, pdf allowed and max size 2MB.");

			var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

			var path = Path.Combine("wwwroot/Documents", fileName);

			using (var stream = new FileStream(path, FileMode.Create))
			{
				await file.CopyToAsync(stream);
			}

			CitizenDocument doc = new CitizenDocument()
			{
				ApplicationID = applicationId,
				DocumentName = documentName,
				FilePath = fileName
			};

			await _service.Upload(doc);

			return Ok("Document Uploaded Successfully");
		}

		// My Documents
		[HttpGet("application/{applicationId}")]
		public async Task<IActionResult> MyDocuments(int applicationId)
		{
			var data = await _service.MyDocuments(applicationId);
			return Ok(data);
		}

		// Re-upload
		[HttpPut("reupload/{id}")]
		public async Task<IActionResult> ReUpload(int id, IFormFile file)
		{
			if (!file.IsValidDocument())
				throw new Exception("Invalid file.");

			var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

			var path = Path.Combine("wwwroot/Documents", fileName);

			using (var stream = new FileStream(path, FileMode.Create))
			{
				await file.CopyToAsync(stream);
			}

			await _service.ReUpload(id, fileName);

			return Ok("Document Reuploaded Successfully");
		}

		// Delete Document
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			await _service.Delete(id);
			return Ok("Document Deleted Successfully");
		}
	}
}
