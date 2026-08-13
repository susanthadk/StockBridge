using Microsoft.AspNetCore.Mvc;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.OrganizationalLevels;
using StockBridge.Application.Interfaces;
using System.Net;

namespace StockBridge.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrganizationalLevelController(IOrganizationalLevelService service) : BaseController
{
    private readonly IOrganizationalLevelService _service = service;

    [HttpGet("GetAllOrganizationalLevels")]
    [ProducesResponseType(typeof(ResponseInfo<List<OrganizationalLevelDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<OrganizationalLevelDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<OrganizationalLevelDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<OrganizationalLevelDto>?>>> GetAllOrganizationalLevels(CancellationToken ct)
    {
        var result = await _service.GetAllOrganizationalLevels(ct);
        return HandleResponse(result);
    }

    [HttpGet("GetOrganizationalLevels/{pageNo}/{pageSize}")]
    [ProducesResponseType(typeof(ResponseInfo<List<OrganizationalLevelDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<OrganizationalLevelDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<OrganizationalLevelDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<OrganizationalLevelDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<OrganizationalLevelDto>?>>> GetAllOrganizationalLevels(int pageNo, int pageSize, CancellationToken ct)
    {
        if (pageNo < 1 || pageSize < 1)
            return BadRequest(ResponseInfo<List<OrganizationalLevelDto>?>.Failure("Page and PageSize must be greater than 0.", HttpStatusCode.BadRequest));

        var result = await _service.GetAllOrganizationalLevels(pageNo, pageSize, ct);
        return HandleResponse(result);
    }

    [HttpGet("GetOrganizationalLevel/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<OrganizationalLevelDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<OrganizationalLevelDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<OrganizationalLevelDto?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<OrganizationalLevelDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<OrganizationalLevelDto?>>> GetOrganizationalLevel(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<OrganizationalLevelDto?>.Failure("Invalid OrganizationalLevel Id.", HttpStatusCode.BadRequest));

        var result = await _service.GetOrganizationalLevelById(id, ct);
        return HandleResponse(result);
    }

    [HttpGet("SearchOrganizationalLevel/{fieldName}")]
    [ProducesResponseType(typeof(ResponseInfo<List<OrganizationalLevelDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<OrganizationalLevelDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<OrganizationalLevelDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<OrganizationalLevelDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<OrganizationalLevelDto>?>>> SearchOrganizationalLevel(string fieldName, string searchString, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(fieldName) || string.IsNullOrWhiteSpace(searchString))
            return BadRequest(ResponseInfo<List<OrganizationalLevelDto>?>.Failure("Field name and search string cannot be empty.", HttpStatusCode.BadRequest));

        var result = await _service.SearchOrganizationalLevel(fieldName, searchString, ct);
        return HandleResponse(result);
    }

    [HttpPost("CreateOrganizationalLevel")]
    [ProducesResponseType(typeof(ResponseInfo<OrganizationalLevelDto?>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseInfo<OrganizationalLevelDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<OrganizationalLevelDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<OrganizationalLevelDto?>>> CreateOrganizationalLevel([FromBody] OrganizationalLevelDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<OrganizationalLevelDto?>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.AddOrganizationalLevel(dto, ct);

        if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Created)
            return CreatedAtAction(nameof(CreateOrganizationalLevel), new { id = result.Data?.LevelId }, result);

        return HandleResponse(result);
    }

    [HttpPut("UpdateOrganizationalLevel")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> UpdateOrganizationalLevel([FromBody] OrganizationalLevelDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.UpdateOrganizationalLevel(dto, ct);
        return HandleResponse(result);
    }

    [HttpDelete("DeleteOrganizationalLevel/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> DeleteOrganizationalLevel(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid OrganizationalLevel Id.", HttpStatusCode.BadRequest));

        var result = await _service.DeleteOrganizationalLevel(id, ct);
        return HandleResponse(result);
    }
}