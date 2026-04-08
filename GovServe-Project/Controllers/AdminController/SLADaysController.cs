using GovServe_Project.DTOs.AdminDTO;
using GovServe_Project.Services.Interfaces.AdminServiceInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GovServe_Project.Controllers.AdminController
{
    [Route("api/sladays")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class SLADaysController : ControllerBase
    {
        private readonly ISLADayService _service;

        public SLADaysController(ISLADayService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(SLADayCreateDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = result.SLADayID }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SLADayUpdateDto dto)
        {
            return Ok(await _service.UpdateAsync(id, dto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}