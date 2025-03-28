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
    public async Task<IActionResult> Get(
        [FromQuery] SampleFilterDto customerFilterDto,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await customerFacade.GetListByFilterAsync(customerFilterDto, cancellationToken);

            return Ok(ApiResponseDto<PaginationDto<SampleResponseDto>>.Success(result));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Sample get");

            return BadRequest(ApiResponseDto<PaginationDto<SampleResponseDto>>.Fail(ex.Message));
        }
    }

    /// <summary>
    /// get(id) 껍데기만 구현
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [Authorize(Policy = "sample:r")]
    public async Task<IActionResult> Get(long id, CancellationToken cancellationToken)
    {
        try
        {
            // 추후 구현 예정
            var result = new SampleResponseDto();
            return Ok(ApiResponseDto<SampleResponseDto>.Success(result));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Sample get(id)");

            return BadRequest(ApiResponseDto<SampleResponseDto>.Fail(ex.Message));
        }
    }

    /// <summary>
    /// post 껍데기만 구현
    /// </summary>
    /// <param name="customerRequestDto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Policy = "sample:w")]
    public async Task<IActionResult> Post([FromBody] SampleRequestDto customerRequestDto,
         CancellationToken cancellationToken)
    {
        try
        {
            // 추후 구현 예정
            var result = new SampleResponseDto();
            return Ok(ApiResponseDto<SampleResponseDto>.Success(result));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Sample post");

            return BadRequest(ApiResponseDto<SampleResponseDto>.Fail(ex.Message));
        }
    }

    /// <summary>
    /// put 껍데기만 구현
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
        try
        {
            // 추후 구현 예정
            var result = new SampleResponseDto();
            return Ok(ApiResponseDto<SampleResponseDto>.Success(result));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Sample put");

            return BadRequest(ApiResponseDto<SampleResponseDto>.Fail(ex.Message));
        }
    }

    /// <summary>
    /// delete 껍데기만 구현
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete]
    [Authorize(Policy = "sample:w")]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        try
        {
            // 추후 구현 예정
            var result = new SampleResponseDto();
            return Ok(ApiResponseDto<SampleResponseDto>.Success(result));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Sample delete");

            return BadRequest(ApiResponseDto<SampleResponseDto>.Fail(ex.Message));
        }
    }
}
