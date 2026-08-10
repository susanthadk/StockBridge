using Microsoft.AspNetCore.Mvc;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.MainLocations;
using StockBridge.Application.Interfaces;
using System.Net;

namespace StockBridge.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MainLocationController(IMainLocationService service) : BaseController
{
    private readonly IMainLocationService _service = service;

    [HttpGet("GetAllMainLocations")]
    [ProducesResponseType(typeof(ResponseInfo<List<MainLocationDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<MainLocationDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<MainLocationDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<MainLocationDto>?>>> GetAllMainLocations(CancellationToken ct)
    {
        var result = await _service.GetAllMainLocations(ct);
        return HandleResponse(result);
    }

    [HttpGet("GetMainLocations/{pageNo}/{pageSize}")]
    [ProducesResponseType(typeof(ResponseInfo<List<MainLocationDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<MainLocationDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<MainLocationDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<MainLocationDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<MainLocationDto>?>>> GetAllMainLocations(int pageNo, int pageSize, CancellationToken ct)
    {
        if (pageNo < 1 || pageSize < 1)
            return BadRequest(ResponseInfo<List<MainLocationDto>?>.Failure("Page and PageSize must be greater than 0.", HttpStatusCode.BadRequest));

        var result = await _service.GetAllMainLocations(pageNo, pageSize, ct);
        return HandleResponse(result);
    }

    [HttpGet("GetMainLocation/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<MainLocationDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<MainLocationDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<MainLocationDto?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<MainLocationDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<MainLocationDto?>>> GetMainLocation(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<MainLocationDto?>.Failure("Invalid MainLocation Id.", HttpStatusCode.BadRequest));

        var result = await _service.GetMainLocationById(id, ct);
        return HandleResponse(result);
    }

    [HttpGet("SearchMainLocation/{fieldName}")]
    [ProducesResponseType(typeof(ResponseInfo<List<MainLocationDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<MainLocationDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<MainLocationDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<MainLocationDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<MainLocationDto>?>>> SearchMainLocation(string fieldName, string searchString, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(fieldName) || string.IsNullOrWhiteSpace(searchString))
            return BadRequest(ResponseInfo<List<MainLocationDto>?>.Failure("Field name and search string cannot be empty.", HttpStatusCode.BadRequest));

        var result = await _service.SearchMainLocation(fieldName, searchString, ct);
        return HandleResponse(result);
    }

    [HttpPost("CreateMainLocation")]
    [ProducesResponseType(typeof(ResponseInfo<MainLocationDto?>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseInfo<MainLocationDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<MainLocationDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<MainLocationDto?>>> CreateMainLocation([FromBody] MainLocationDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<MainLocationDto?>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.AddMainLocation(dto, ct);

        if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Created)
            return CreatedAtAction(nameof(CreateMainLocation), new { id = result.Data?.MainLocationId }, result);

        return HandleResponse(result);
    }

    [HttpPut("UpdateMainLocation")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> UpdateMainLocation([FromBody] MainLocationDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.UpdateMainLocation(dto, ct);
        return HandleResponse(result);
    }

    [HttpDelete("DeleteMainLocation/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> DeleteMainLocation(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid MainLocation Id.", HttpStatusCode.BadRequest));

        var result = await _service.DeleteMainLocation(id, ct);
        return HandleResponse(result);
    }
}