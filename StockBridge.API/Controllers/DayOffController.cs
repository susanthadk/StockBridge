using Microsoft.AspNetCore.Mvc;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.DayOffs;
using StockBridge.Application.Interfaces;
using System.Net;

namespace StockBridge.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DayOffController(IDayOffService service) : BaseController
{
    private readonly IDayOffService _service = service;

    [HttpGet("GetAllDayOffs")]
    [ProducesResponseType(typeof(ResponseInfo<List<DayOffDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<DayOffDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<DayOffDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<DayOffDto>?>>> GetAllDayOffs(CancellationToken ct)
    {
        var result = await _service.GetAllDayOffs(ct);
        return HandleResponse(result);
    }

    [HttpGet("GetDayOffs/{pageNo}/{pageSize}")]
    [ProducesResponseType(typeof(ResponseInfo<List<DayOffDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<DayOffDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<DayOffDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<DayOffDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<DayOffDto>?>>> GetAllDayOffs(int pageNo, int pageSize, CancellationToken ct)
    {
        if (pageNo < 1 || pageSize < 1)
            return BadRequest(ResponseInfo<List<DayOffDto>?>.Failure("Page and PageSize must be greater than 0.", HttpStatusCode.BadRequest));

        var result = await _service.GetAllDayOffs(pageNo, pageSize, ct);
        return HandleResponse(result);
    }

    [HttpGet("GetDayOff/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<DayOffDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<DayOffDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<DayOffDto?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<DayOffDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<DayOffDto?>>> GetDayOff(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<DayOffDto?>.Failure("Invalid DayOff Id.", HttpStatusCode.BadRequest));

        var result = await _service.GetDayOffById(id, ct);
        return HandleResponse(result);
    }

    [HttpGet("SearchDayOff/{fieldName}")]
    [ProducesResponseType(typeof(ResponseInfo<List<DayOffDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<DayOffDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<DayOffDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<DayOffDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<DayOffDto>?>>> SearchDayOff(string fieldName, string searchString, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(fieldName) || string.IsNullOrWhiteSpace(searchString))
            return BadRequest(ResponseInfo<List<DayOffDto>?>.Failure("Field name and search string cannot be empty.", HttpStatusCode.BadRequest));

        var result = await _service.SearchDayOff(fieldName, searchString, ct);
        return HandleResponse(result);
    }

    [HttpPost("CreateDayOff")]
    [ProducesResponseType(typeof(ResponseInfo<DayOffDto?>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseInfo<DayOffDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<DayOffDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<DayOffDto?>>> CreateDayOff([FromBody] DayOffDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<DayOffDto?>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.AddDayOff(dto, ct);

        if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Created)
            return CreatedAtAction(nameof(CreateDayOff), new { id = result.Data?.DayOffId }, result);

        return HandleResponse(result);
    }

    [HttpPut("UpdateDayOff")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> UpdateDayOff([FromBody] DayOffDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.UpdateDayOff(dto, ct);
        return HandleResponse(result);
    }

    [HttpDelete("DeleteDayOff/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> DeleteDayOff(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid DayOff Id.", HttpStatusCode.BadRequest));

        var result = await _service.DeleteDayOff(id, ct);
        return HandleResponse(result);
    }
}