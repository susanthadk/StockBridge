using Microsoft.AspNetCore.Mvc;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.PriceLists;
using StockBridge.Application.Interfaces;
using System.Net;

namespace StockBridge.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PriceListController(IPriceListService service) : BaseController
{
    private readonly IPriceListService _service = service;

    [HttpGet("GetAllPriceLists")]
    [ProducesResponseType(typeof(ResponseInfo<List<PriceListDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<PriceListDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<PriceListDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<PriceListDto>?>>> GetAllPriceLists(CancellationToken ct)
    {
        var result = await _service.GetAllPriceLists(ct);
        return HandleResponse(result);
    }

    [HttpGet("GetPriceLists/{pageNo}/{pageSize}")]
    [ProducesResponseType(typeof(ResponseInfo<List<PriceListDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<PriceListDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<PriceListDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<PriceListDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<PriceListDto>?>>> GetAllPriceLists(int pageNo, int pageSize, CancellationToken ct)
    {
        if (pageNo < 1 || pageSize < 1)
            return BadRequest(ResponseInfo<List<PriceListDto>?>.Failure("Page and PageSize must be greater than 0.", HttpStatusCode.BadRequest));

        var result = await _service.GetAllPriceLists(pageNo, pageSize, ct);
        return HandleResponse(result);
    }

    [HttpGet("GetPriceList/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<PriceListDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<PriceListDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<PriceListDto?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<PriceListDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<PriceListDto?>>> GetPriceList(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<PriceListDto?>.Failure("Invalid PriceList Id.", HttpStatusCode.BadRequest));

        var result = await _service.GetPriceListById(id, ct);
        return HandleResponse(result);
    }

    [HttpGet("SearchPriceList/{fieldName}")]
    [ProducesResponseType(typeof(ResponseInfo<List<PriceListDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<PriceListDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<PriceListDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<PriceListDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<PriceListDto>?>>> SearchPriceList(string fieldName, string searchString, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(fieldName) || string.IsNullOrWhiteSpace(searchString))
            return BadRequest(ResponseInfo<List<PriceListDto>?>.Failure("Field name and search string cannot be empty.", HttpStatusCode.BadRequest));

        var result = await _service.SearchPriceList(fieldName, searchString, ct);
        return HandleResponse(result);
    }

    [HttpPost("CreatePriceList")]
    [ProducesResponseType(typeof(ResponseInfo<PriceListDto?>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseInfo<PriceListDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<PriceListDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<PriceListDto?>>> CreatePriceList([FromBody] PriceListDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<PriceListDto?>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.AddPriceList(dto, ct);

        if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Created)
            return CreatedAtAction(nameof(CreatePriceList), new { id = result.Data?.PriceListId }, result);

        return HandleResponse(result);
    }

    [HttpPut("UpdatePriceList")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> UpdatePriceList([FromBody] PriceListDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.UpdatePriceList(dto, ct);
        return HandleResponse(result);
    }

    [HttpDelete("DeletePriceList/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> DeletePriceList(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid PriceList Id.", HttpStatusCode.BadRequest));

        var result = await _service.DeletePriceList(id, ct);
        return HandleResponse(result);
    }
}