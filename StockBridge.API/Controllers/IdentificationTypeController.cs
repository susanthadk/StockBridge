using Microsoft.AspNetCore.Mvc;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.IdentificationTypes;
using StockBridge.Application.Interfaces;
using System.Net;

namespace StockBridge.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IdentificationTypeController(IIdentificationTypeService service) : BaseController
{
    private readonly IIdentificationTypeService _service = service;

    [HttpGet("GetAllIdentificationTypes")]
    [ProducesResponseType(typeof(ResponseInfo<List<IdentificationTypeDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<IdentificationTypeDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<IdentificationTypeDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<IdentificationTypeDto>?>>> GetAllIdentificationTypes(CancellationToken ct)
    {
        var result = await _service.GetAllIdentificationTypes(ct);
        return HandleResponse(result);
    }

    [HttpGet("GetIdentificationTypes/{pageNo}/{pageSize}")]
    [ProducesResponseType(typeof(ResponseInfo<List<IdentificationTypeDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<IdentificationTypeDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<IdentificationTypeDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<IdentificationTypeDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<IdentificationTypeDto>?>>> GetAllIdentificationTypes(int pageNo, int pageSize, CancellationToken ct)
    {
        if (pageNo < 1 || pageSize < 1)
            return BadRequest(ResponseInfo<List<IdentificationTypeDto>?>.Failure("Page and PageSize must be greater than 0.", HttpStatusCode.BadRequest));

        var result = await _service.GetAllIdentificationTypes(pageNo, pageSize, ct);
        return HandleResponse(result);
    }

    [HttpGet("GetIdentificationType/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<IdentificationTypeDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<IdentificationTypeDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<IdentificationTypeDto?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<IdentificationTypeDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<IdentificationTypeDto?>>> GetIdentificationType(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<IdentificationTypeDto?>.Failure("Invalid IdentificationType Id.", HttpStatusCode.BadRequest));

        var result = await _service.GetIdentificationTypeById(id, ct);
        return HandleResponse(result);
    }

    [HttpGet("SearchIdentificationType/{fieldName}")]
    [ProducesResponseType(typeof(ResponseInfo<List<IdentificationTypeDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<IdentificationTypeDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<IdentificationTypeDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<IdentificationTypeDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<IdentificationTypeDto>?>>> SearchIdentificationType(string fieldName, string searchString, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(fieldName) || string.IsNullOrWhiteSpace(searchString))
            return BadRequest(ResponseInfo<List<IdentificationTypeDto>?>.Failure("Field name and search string cannot be empty.", HttpStatusCode.BadRequest));

        var result = await _service.SearchIdentificationType(fieldName, searchString, ct);
        return HandleResponse(result);
    }

    [HttpPost("CreateIdentificationType")]
    [ProducesResponseType(typeof(ResponseInfo<IdentificationTypeDto?>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseInfo<IdentificationTypeDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<IdentificationTypeDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<IdentificationTypeDto?>>> CreateIdentificationType([FromBody] IdentificationTypeDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<IdentificationTypeDto?>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.AddIdentificationType(dto, ct);

        if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Created)
            return CreatedAtAction(nameof(CreateIdentificationType), new { id = result.Data?.IdentificationTypeId }, result);

        return HandleResponse(result);
    }

    [HttpPut("UpdateIdentificationType")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> UpdateIdentificationType([FromBody] IdentificationTypeDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.UpdateIdentificationType(dto, ct);
        return HandleResponse(result);
    }

    [HttpDelete("DeleteIdentificationType/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> DeleteIdentificationType(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid IdentificationType Id.", HttpStatusCode.BadRequest));

        var result = await _service.DeleteIdentificationType(id, ct);
        return HandleResponse(result);
    }
}