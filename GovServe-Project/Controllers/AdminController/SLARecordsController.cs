
using GovServe_Project.DTOs.AdminDTO;
using GovServe_Project.Services.Interfaces.AdminServiceInterface;
using Microsoft.AspNetCore.Mvc;

namespace GovServe_Project.Controllers.AdminController
{
    [Route("api/[controller]")]
    [ApiController]
    public class SLARecordsController : ControllerBase
    {
        private readonly ISLARecordService _service;

        public SLARecordsController(ISLARecordService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
            => Ok(await _service.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create(SLARecordCreateDto dto)
        {
            var result = await _service.CreateAsync(dto);

            return CreatedAtAction(nameof(Get),
                new { id = result.SLARecordID },
                result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }


}
