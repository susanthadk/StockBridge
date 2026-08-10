using Microsoft.AspNetCore.Mvc;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.SalesRepresentatives;
using StockBridge.Application.Interfaces;
using System.Net;

namespace StockBridge.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SalesRepresentativeController(ISalesRepresentativeService service) : BaseController
{
    private readonly ISalesRepresentativeService _service = service;

    [HttpGet("GetAllSalesRepresentatives")]
    [ProducesResponseType(typeof(ResponseInfo<List<SalesRepresentativeDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<SalesRepresentativeDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<SalesRepresentativeDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<SalesRepresentativeDto>?>>> GetAllSalesRepresentatives(CancellationToken ct)
    {
        var result = await _service.GetAllSalesRepresentatives(ct);
        return HandleResponse(result);
    }

    [HttpGet("GetSalesRepresentatives/{pageNo}/{pageSize}")]
    [ProducesResponseType(typeof(ResponseInfo<List<SalesRepresentativeDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<SalesRepresentativeDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<SalesRepresentativeDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<SalesRepresentativeDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<SalesRepresentativeDto>?>>> GetAllSalesRepresentatives(int pageNo, int pageSize, CancellationToken ct)
    {
        if (pageNo < 1 || pageSize < 1)
            return BadRequest(ResponseInfo<List<SalesRepresentativeDto>?>.Failure("Page and PageSize must be greater than 0.", HttpStatusCode.BadRequest));

        var result = await _service.GetAllSalesRepresentatives(pageNo, pageSize, ct);
        return HandleResponse(result);
    }

    [HttpGet("GetSalesRepresentative/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<SalesRepresentativeDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<SalesRepresentativeDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<SalesRepresentativeDto?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<SalesRepresentativeDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<SalesRepresentativeDto?>>> GetSalesRepresentative(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<SalesRepresentativeDto?>.Failure("Invalid SalesRepresentative Id.", HttpStatusCode.BadRequest));

        var result = await _service.GetSalesRepresentativeById(id, ct);
        return HandleResponse(result);
    }

    [HttpGet("SearchSalesRepresentative/{fieldName}")]
    [ProducesResponseType(typeof(ResponseInfo<List<SalesRepresentativeDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<SalesRepresentativeDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<SalesRepresentativeDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<SalesRepresentativeDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<SalesRepresentativeDto>?>>> SearchSalesRepresentative(string fieldName, string searchString, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(fieldName) || string.IsNullOrWhiteSpace(searchString))
            return BadRequest(ResponseInfo<List<SalesRepresentativeDto>?>.Failure("Field name and search string cannot be empty.", HttpStatusCode.BadRequest));

        var result = await _service.SearchSalesRepresentative(fieldName, searchString, ct);
        return HandleResponse(result);
    }

    [HttpPost("CreateSalesRepresentative")]
    [ProducesResponseType(typeof(ResponseInfo<SalesRepresentativeDto?>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseInfo<SalesRepresentativeDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<SalesRepresentativeDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<SalesRepresentativeDto?>>> CreateSalesRepresentative([FromBody] SalesRepresentativeDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<SalesRepresentativeDto?>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.AddSalesRepresentative(dto, ct);

        if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Created)
            return CreatedAtAction(nameof(CreateSalesRepresentative), new { id = result.Data?.SalesRepresentativeId }, result);

        return HandleResponse(result);
    }

    [HttpPut("UpdateSalesRepresentative")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> UpdateSalesRepresentative([FromBody] SalesRepresentativeDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.UpdateSalesRepresentative(dto, ct);
        return HandleResponse(result);
    }

    [HttpDelete("DeleteSalesRepresentative/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> DeleteSalesRepresentative(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid SalesRepresentative Id.", HttpStatusCode.BadRequest));

        var result = await _service.DeleteSalesRepresentative(id, ct);
        return HandleResponse(result);
    }
}