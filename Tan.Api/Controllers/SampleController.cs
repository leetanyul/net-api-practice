using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net;
using Tan.Api.Attributes.Authorizations;
using Tan.Application.Dtos;
using Tan.Application.Facades.Interfaces;

namespace Tan.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class SampleController(
    ILogger<SampleController> logger,
    ISampleFacade customerFacade) : ControllerBase
{
    [HttpGet]
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
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        return NoContent();
    }
}
