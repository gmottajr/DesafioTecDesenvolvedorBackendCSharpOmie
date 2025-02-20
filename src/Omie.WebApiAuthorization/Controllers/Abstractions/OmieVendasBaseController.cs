using Microsoft.AspNetCore.Mvc;
using Omie.Application;
using Omie.Application.Models.Abstractions;

namespace Omie.WebApi;

[ApiController]
[Route("api/[controller]")]
public abstract class OmieVendasBaseController<TDto, TInsertingDto, TKey> : ControllerBase where TDto : IResourceDtoBase where TInsertingDto : IResourceDtoBase
{
    protected readonly IAppServiceBase<TDto, TInsertingDto, TKey> _applicationService;

    public OmieVendasBaseController(IAppServiceBase<TDto, TInsertingDto, TKey> applicationService)
    {
        _applicationService = applicationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _applicationService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(TKey id)
    {
        try
        {
            var result = await _applicationService.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] TInsertingDto dto)
    {
        if (dto == null) return BadRequest();

        try
        {
            var result = await _applicationService.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result }, result);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    [HttpPut]
    public virtual async Task<IActionResult> Update([FromBody] TDto dto)
    {
        if (dto == null) return BadRequest();

        try
        {
            var result = await _applicationService.UpdateAsync(dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> Delete(TKey id)
    {
        try
        {
            await _applicationService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    protected IActionResult HandleException(Exception ex)
    {
        // Centralize error logging or formatting here
        // Add logging if necessary
        return StatusCode(500, new { error = ex.Message });
    }
}
