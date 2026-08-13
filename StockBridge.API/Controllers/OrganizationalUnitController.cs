using Microsoft.AspNetCore.Mvc;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.OrganizationalUnits;
using StockBridge.Application.Interfaces;
using System.Net;

namespace StockBridge.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrganizationalUnitController(IOrganizationalUnitService service) : BaseController
{
    private readonly IOrganizationalUnitService _service = service;

    [HttpGet("GetAllOrganizationalUnits")]
    [ProducesResponseType(typeof(ResponseInfo<List<OrganizationalUnitDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<OrganizationalUnitDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<OrganizationalUnitDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<OrganizationalUnitDto>?>>> GetAllOrganizationalUnits(CancellationToken ct)
    {
        var result = await _service.GetAllOrganizationalUnits(ct);
        return HandleResponse(result);
    }

    [HttpGet("GetOrganizationalUnits/{pageNo}/{pageSize}")]
    [ProducesResponseType(typeof(ResponseInfo<List<OrganizationalUnitDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<OrganizationalUnitDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<OrganizationalUnitDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<OrganizationalUnitDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<OrganizationalUnitDto>?>>> GetAllOrganizationalUnits(int pageNo, int pageSize, CancellationToken ct)
    {
        if (pageNo < 1 || pageSize < 1)
            return BadRequest(ResponseInfo<List<OrganizationalUnitDto>?>.Failure("Page and PageSize must be greater than 0.", HttpStatusCode.BadRequest));

        var result = await _service.GetAllOrganizationalUnits(pageNo, pageSize, ct);
        return HandleResponse(result);
    }

    [HttpGet("GetOrganizationalUnit/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<OrganizationalUnitDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<OrganizationalUnitDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<OrganizationalUnitDto?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<OrganizationalUnitDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<OrganizationalUnitDto?>>> GetOrganizationalUnit(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<OrganizationalUnitDto?>.Failure("Invalid OrganizationalUnit Id.", HttpStatusCode.BadRequest));

        var result = await _service.GetOrganizationalUnitById(id, ct);
        return HandleResponse(result);
    }

    [HttpGet("SearchOrganizationalUnit/{fieldName}")]
    [ProducesResponseType(typeof(ResponseInfo<List<OrganizationalUnitDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<OrganizationalUnitDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<OrganizationalUnitDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<OrganizationalUnitDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<OrganizationalUnitDto>?>>> SearchOrganizationalUnit(string fieldName, string searchString, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(fieldName) || string.IsNullOrWhiteSpace(searchString))
            return BadRequest(ResponseInfo<List<OrganizationalUnitDto>?>.Failure("Field name and search string cannot be empty.", HttpStatusCode.BadRequest));

        var result = await _service.SearchOrganizationalUnit(fieldName, searchString, ct);
        return HandleResponse(result);
    }

    [HttpPost("CreateOrganizationalUnit")]
    [ProducesResponseType(typeof(ResponseInfo<OrganizationalUnitDto?>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseInfo<OrganizationalUnitDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<OrganizationalUnitDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<OrganizationalUnitDto?>>> CreateOrganizationalUnit([FromBody] OrganizationalUnitDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<OrganizationalUnitDto?>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.AddOrganizationalUnit(dto, ct);

        if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Created)
            return CreatedAtAction(nameof(CreateOrganizationalUnit), new { id = result.Data?.OrganizationalUnitId }, result);

        return HandleResponse(result);
    }

    [HttpPut("UpdateOrganizationalUnit")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> UpdateOrganizationalUnit([FromBody] OrganizationalUnitDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.UpdateOrganizationalUnit(dto, ct);
        return HandleResponse(result);
    }

    [HttpDelete("DeleteOrganizationalUnit/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> DeleteOrganizationalUnit(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid OrganizationalUnit Id.", HttpStatusCode.BadRequest));

        var result = await _service.DeleteOrganizationalUnit(id, ct);
        return HandleResponse(result);
    }
}