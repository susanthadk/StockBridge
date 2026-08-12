using Microsoft.AspNetCore.Mvc;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.Designations;
using StockBridge.Application.Interfaces;
using System.Net;

namespace StockBridge.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DesignationController(IDesignationService service) : BaseController
{
    private readonly IDesignationService _service = service;

    [HttpGet("GetAllDesignations")]
    [ProducesResponseType(typeof(ResponseInfo<List<DesignationDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<DesignationDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<DesignationDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<DesignationDto>?>>> GetAllDesignations(CancellationToken ct)
    {
        var result = await _service.GetAllDesignations(ct);
        return HandleResponse(result);
    }

    [HttpGet("GetDesignations/{pageNo}/{pageSize}")]
    [ProducesResponseType(typeof(ResponseInfo<List<DesignationDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<DesignationDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<DesignationDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<DesignationDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<DesignationDto>?>>> GetAllDesignations(int pageNo, int pageSize, CancellationToken ct)
    {
        if (pageNo < 1 || pageSize < 1)
            return BadRequest(ResponseInfo<List<DesignationDto>?>.Failure("Page and PageSize must be greater than 0.", HttpStatusCode.BadRequest));

        var result = await _service.GetAllDesignations(pageNo, pageSize, ct);
        return HandleResponse(result);
    }

    [HttpGet("GetDesignation/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<DesignationDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<DesignationDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<DesignationDto?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<DesignationDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<DesignationDto?>>> GetDesignation(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<DesignationDto?>.Failure("Invalid Designation Id.", HttpStatusCode.BadRequest));

        var result = await _service.GetDesignationById(id, ct);
        return HandleResponse(result);
    }

    [HttpGet("SearchDesignation/{fieldName}")]
    [ProducesResponseType(typeof(ResponseInfo<List<DesignationDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<DesignationDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<DesignationDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<DesignationDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<DesignationDto>?>>> SearchDesignation(string fieldName, string searchString, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(fieldName) || string.IsNullOrWhiteSpace(searchString))
            return BadRequest(ResponseInfo<List<DesignationDto>?>.Failure("Field name and search string cannot be empty.", HttpStatusCode.BadRequest));

        var result = await _service.SearchDesignation(fieldName, searchString, ct);
        return HandleResponse(result);
    }

    [HttpPost("CreateDesignation")]
    [ProducesResponseType(typeof(ResponseInfo<DesignationDto?>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseInfo<DesignationDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<DesignationDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<DesignationDto?>>> CreateDesignation([FromBody] DesignationDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<DesignationDto?>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.AddDesignation(dto, ct);

        if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Created)
            return CreatedAtAction(nameof(CreateDesignation), new { id = result.Data?.DesignationId }, result);

        return HandleResponse(result);
    }

    [HttpPut("UpdateDesignation")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> UpdateDesignation([FromBody] DesignationDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.UpdateDesignation(dto, ct);
        return HandleResponse(result);
    }

    [HttpDelete("DeleteDesignation/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> DeleteDesignation(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid Designation Id.", HttpStatusCode.BadRequest));

        var result = await _service.DeleteDesignation(id, ct);
        return HandleResponse(result);
    }
}