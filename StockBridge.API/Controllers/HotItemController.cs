using Microsoft.AspNetCore.Mvc;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.HotItems;
using StockBridge.Application.Interfaces;
using System.Net;

namespace StockBridge.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HotItemController(IHotItemService service) : BaseController
{
    private readonly IHotItemService _service = service;

    [HttpGet("GetAllHotItems")]
    [ProducesResponseType(typeof(ResponseInfo<List<HotItemDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<HotItemDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<HotItemDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<HotItemDto>?>>> GetAllHotItems(CancellationToken ct)
    {
        var result = await _service.GetAllHotItems(ct);
        return HandleResponse(result);
    }

    [HttpGet("GetHotItems/{pageNo}/{pageSize}")]
    [ProducesResponseType(typeof(ResponseInfo<List<HotItemDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<HotItemDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<HotItemDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<HotItemDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<HotItemDto>?>>> GetAllHotItems(int pageNo, int pageSize, CancellationToken ct)
    {
        if (pageNo < 1 || pageSize < 1)
            return BadRequest(ResponseInfo<List<HotItemDto>?>.Failure("Page and PageSize must be greater than 0.", HttpStatusCode.BadRequest));

        var result = await _service.GetAllHotItems(pageNo, pageSize, ct);
        return HandleResponse(result);
    }

    [HttpGet("GetHotItem/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<HotItemDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<HotItemDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<HotItemDto?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<HotItemDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<HotItemDto?>>> GetHotItem(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<HotItemDto?>.Failure("Invalid HotItem Id.", HttpStatusCode.BadRequest));

        var result = await _service.GetHotItemById(id, ct);
        return HandleResponse(result);
    }

    [HttpGet("SearchHotItem/{fieldName}")]
    [ProducesResponseType(typeof(ResponseInfo<List<HotItemDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<HotItemDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<HotItemDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<HotItemDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<HotItemDto>?>>> SearchHotItem(string fieldName, string searchString, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(fieldName) || string.IsNullOrWhiteSpace(searchString))
            return BadRequest(ResponseInfo<List<HotItemDto>?>.Failure("Field name and search string cannot be empty.", HttpStatusCode.BadRequest));

        var result = await _service.SearchHotItem(fieldName, searchString, ct);
        return HandleResponse(result);
    }

    [HttpPost("CreateHotItem")]
    [ProducesResponseType(typeof(ResponseInfo<HotItemDto?>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseInfo<HotItemDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<HotItemDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<HotItemDto?>>> CreateHotItem([FromBody] HotItemDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<HotItemDto?>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.AddHotItem(dto, ct);

        if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Created)
            return CreatedAtAction(nameof(CreateHotItem), new { id = result.Data?.HotItemId }, result);

        return HandleResponse(result);
    }

    [HttpPut("UpdateHotItem")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> UpdateHotItem([FromBody] HotItemDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.UpdateHotItem(dto, ct);
        return HandleResponse(result);
    }

    [HttpDelete("DeleteHotItem/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> DeleteHotItem(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid HotItem Id.", HttpStatusCode.BadRequest));

        var result = await _service.DeleteHotItem(id, ct);
        return HandleResponse(result);
    }
}