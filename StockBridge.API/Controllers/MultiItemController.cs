using Microsoft.AspNetCore.Mvc;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.MultiItems;
using StockBridge.Application.Interfaces;
using System.Net;

namespace StockBridge.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MultiItemController(IMultiItemService service) : BaseController
{
    private readonly IMultiItemService _service = service;

    [HttpGet("GetAllMultiItems")]
    [ProducesResponseType(typeof(ResponseInfo<List<MultiItemDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<MultiItemDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<MultiItemDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<MultiItemDto>?>>> GetAllMultiItems(CancellationToken ct)
    {
        var result = await _service.GetAllMultiItems(ct);
        return HandleResponse(result);
    }

    [HttpGet("GetMultiItems/{pageNo}/{pageSize}")]
    [ProducesResponseType(typeof(ResponseInfo<List<MultiItemDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<MultiItemDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<MultiItemDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<MultiItemDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<MultiItemDto>?>>> GetAllMultiItems(int pageNo, int pageSize, CancellationToken ct)
    {
        if (pageNo < 1 || pageSize < 1)
            return BadRequest(ResponseInfo<List<MultiItemDto>?>.Failure("Page and PageSize must be greater than 0.", HttpStatusCode.BadRequest));

        var result = await _service.GetAllMultiItems(pageNo, pageSize, ct);
        return HandleResponse(result);
    }

    [HttpGet("GetMultiItem/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<MultiItemDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<MultiItemDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<MultiItemDto?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<MultiItemDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<MultiItemDto?>>> GetMultiItem(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<MultiItemDto?>.Failure("Invalid MultiItem Id.", HttpStatusCode.BadRequest));

        var result = await _service.GetMultiItemById(id, ct);
        return HandleResponse(result);
    }

    [HttpGet("SearchMultiItem/{fieldName}")]
    [ProducesResponseType(typeof(ResponseInfo<List<MultiItemDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<MultiItemDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<MultiItemDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<MultiItemDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<MultiItemDto>?>>> SearchMultiItem(string fieldName, string searchString, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(fieldName) || string.IsNullOrWhiteSpace(searchString))
            return BadRequest(ResponseInfo<List<MultiItemDto>?>.Failure("Field name and search string cannot be empty.", HttpStatusCode.BadRequest));

        var result = await _service.SearchMultiItem(fieldName, searchString, ct);
        return HandleResponse(result);
    }

    [HttpPost("CreateMultiItem")]
    [ProducesResponseType(typeof(ResponseInfo<MultiItemDto?>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseInfo<MultiItemDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<MultiItemDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<MultiItemDto?>>> CreateMultiItem([FromBody] MultiItemDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<MultiItemDto?>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.AddMultiItem(dto, ct);

        if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Created)
            return CreatedAtAction(nameof(CreateMultiItem), new { id = result.Data?.MultiItemId }, result);

        return HandleResponse(result);
    }

    [HttpPut("UpdateMultiItem")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> UpdateMultiItem([FromBody] MultiItemDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.UpdateMultiItem(dto, ct);
        return HandleResponse(result);
    }

    [HttpDelete("DeleteMultiItem/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> DeleteMultiItem(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid MultiItem Id.", HttpStatusCode.BadRequest));

        var result = await _service.DeleteMultiItem(id, ct);
        return HandleResponse(result);
    }
}