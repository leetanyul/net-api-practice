using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Tan.Application.Dtos;
using Tan.Application.Facades.Interfaces;

namespace Tan.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class SampleController(
    ILogger<SampleController> logger,
    ISampleFacade customerFacade) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "sample:r")]
    public async Task<ActionResult<PaginationDto<SampleResponseDto>>> Get(
    [FromQuery] SampleFilterDto customerFilterDto, CancellationToken cancellationToken)
    {
        try
        {
            var result = await customerFacade.GetListByFilterAsync(customerFilterDto, cancellationToken);
            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Sample get ex Activity Id : {activityId}",
                Activity.Current?.Id);

            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// get(id) ²®µ¥±â¸¸ ±¸Çö
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [Authorize(Policy = "sample:r")]
    public async Task<ActionResult<SampleResponseDto>> Get(long id, CancellationToken cancellationToken)
    {
        try
        {
            return new SampleResponseDto();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Sample get(id) ex Activity Id : {activityId}",
                Activity.Current?.Id);

            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// post ²®µ¥±â¸¸ ±¸Çö
    /// </summary>
    /// <param name="customerRequestDto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Policy = "sample:w")]
    public async Task<IActionResult> Post([FromBody] SampleRequestDto customerRequestDto,
        CancellationToken cancellationToken)
    {
        return CreatedAtAction(nameof(Get), new { }, new { });
    }

    /// <summary>
    /// put ²®µ¥±â¸¸ ±¸Çö
    /// </summary>
    /// <param name="id"></param>
    /// <param name="customerRequestDto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut]
    [Authorize(Policy = "sample:w")]
    public async Task<IActionResult> Put(long id, [FromBody] SampleRequestDto customerRequestDto,
        CancellationToken cancellationToken)
    {
        return NoContent();
    }

    /// <summary>
    /// delete ²®µ¥±â¸¸ ±¸Çö
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete]
    [Authorize(Policy = "sample:w")]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        return NoContent();
    }
}
