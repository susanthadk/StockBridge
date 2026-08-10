using Microsoft.AspNetCore.Mvc;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.Items;
using StockBridge.Application.Interfaces;
using System.Net;

namespace StockBridge.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ItemController(IItemService service) : BaseController
{
    private readonly IItemService _service = service;

    [HttpGet("GetAllItems")]
    [ProducesResponseType(typeof(ResponseInfo<List<ItemDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<ItemDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<ItemDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<ItemDto>?>>> GetAllItems(CancellationToken ct)
    {
        var result = await _service.GetAllItems(ct);
        return HandleResponse(result);
    }

    [HttpGet("GetItems/{pageNo}/{pageSize}")]
    [ProducesResponseType(typeof(ResponseInfo<List<ItemDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<ItemDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<ItemDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<ItemDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<ItemDto>?>>> GetAllItems(int pageNo, int pageSize, CancellationToken ct)
    {
        if (pageNo < 1 || pageSize < 1)
            return BadRequest(ResponseInfo<List<ItemDto>?>.Failure("Page and PageSize must be greater than 0.", HttpStatusCode.BadRequest));

        var result = await _service.GetAllItems(pageNo, pageSize, ct);
        return HandleResponse(result);
    }

    [HttpGet("GetItem/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<ItemDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<ItemDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<ItemDto?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<ItemDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<ItemDto?>>> GetItem(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<ItemDto?>.Failure("Invalid Item Id.", HttpStatusCode.BadRequest));

        var result = await _service.GetItemById(id, ct);
        return HandleResponse(result);
    }

    [HttpGet("SearchItem/{fieldName}")]
    [ProducesResponseType(typeof(ResponseInfo<List<ItemDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<ItemDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<ItemDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<ItemDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<ItemDto>?>>> SearchItem(string fieldName, string searchString, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(fieldName) || string.IsNullOrWhiteSpace(searchString))
            return BadRequest(ResponseInfo<List<ItemDto>?>.Failure("Field name and search string cannot be empty.", HttpStatusCode.BadRequest));

        var result = await _service.SearchItem(fieldName, searchString, ct);
        return HandleResponse(result);
    }

    [HttpPost("CreateItem")]
    [ProducesResponseType(typeof(ResponseInfo<ItemDto?>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseInfo<ItemDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<ItemDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<ItemDto?>>> CreateItem([FromBody] ItemDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<ItemDto?>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.AddItem(dto, ct);

        if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Created)
            return CreatedAtAction(nameof(CreateItem), new { id = result.Data?.ItemId }, result);

        return HandleResponse(result);
    }

    [HttpPut("UpdateItem")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> UpdateItem([FromBody] ItemDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.UpdateItem(dto, ct);
        return HandleResponse(result);
    }

    [HttpDelete("DeleteItem/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> DeleteItem(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid Item Id.", HttpStatusCode.BadRequest));

        var result = await _service.DeleteItem(id, ct);
        return HandleResponse(result);
    }
}