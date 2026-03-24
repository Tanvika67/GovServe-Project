using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GovServe_Project.Data;
using GovServe_Project.DTOs;
using GovServe_Project.Extensions;
using GovServe_Project.Models;
using GovServe_Project.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GovServe_Project.Services.Service_Implementation.CitizenService_Implementation;
using GovServe_Project.Services.Interfaces.CitizenService_Interface;
using GovServe_Project.DTOs.CitizenDTO;
using Microsoft.AspNetCore.Authorization;


namespace GovServe_Project.Controllers.CitizenController
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
	
		public async Task<IActionResult> UploadDocument([FromForm] UploadCitizenDocumentDTO model)
		{
			var result = await _service.UploadDocumentAsync(model);

			if (!result)
				return BadRequest("Upload Failed");

			return Ok("Document Uploaded Successfully");
		}

		[HttpGet("GetMyAllDocuments/{userId}")]
		public async Task<IActionResult> GetMyAllDocuments(int userId)
		{
			var result = await _service.GetMyAllDocuments(userId);

			if (!result.Any())
				return NotFound("No documents found");

			return Ok(result);
		}

		[HttpGet("GetDocumentsByApplicationId/{applicationId}")]
		public async Task<IActionResult> GetDocumentsByApplicationId(int applicationId)
		{
			var result = await _service.GetDocumentsByApplicationId(applicationId);

			if (!result.Any())
				return NotFound("No documents found");

			return Ok(result);
		}

		// Document Status
		[HttpGet("status/{id}")]
		[Authorize(Roles = "Citizen")]
		public async Task<IActionResult> GetDocumentStatus(int id)
		{
			var status = await _service.GetDocumentStatusAsync(id);

			if (status == null)
				return NotFound("Document Not Found");

			return Ok(status);
		}

		// Delete Document
		[HttpDelete("delete/{id}")]
		[Authorize(Roles = "Citizen")]
		public async Task<IActionResult> DeleteDocument(int id)
		{
			var result = await _service.DeleteDocumentAsync(id);

			if (!result)
				return NotFound("Document Not Found");

			return Ok("Document Deleted Successfully");
		}

		[HttpPut("ApproveDocument/{CitizenDocumentID}")]
		[AllowAnonymous]
		//[Authorize(Roles = "Officer")]
		public async Task<IActionResult> ApproveDocument(int CitizenDocumentID)
		{
			var result = await _service.ApproveDocument(CitizenDocumentID);
			if (result == "Document Not Found") return NotFound(result); // Proper Status Code
			return Ok(new { message = result });
		}

		[HttpPut("RejectDocument/{CitizenDocumentID}")]
		//[Authorize(Roles = "Officer")]
		public async Task<IActionResult> RejectDocument(int CitizenDocumentID)
		{
			var result = await _service.RejectDocument(CitizenDocumentID);
			return Ok(result);
		}
	}
}
