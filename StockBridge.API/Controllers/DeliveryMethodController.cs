using Microsoft.AspNetCore.Mvc;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.DeliveryMethods;
using StockBridge.Application.Interfaces;
using System.Net;

namespace StockBridge.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DeliveryMethodController(IDeliveryMethodService service) : BaseController
{
    private readonly IDeliveryMethodService _service = service;

    [HttpGet("GetAllDeliveryMethods")]
    [ProducesResponseType(typeof(ResponseInfo<List<DeliveryMethodDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<DeliveryMethodDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<DeliveryMethodDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<DeliveryMethodDto>?>>> GetAllDeliveryMethods(CancellationToken ct)
    {
        var result = await _service.GetAllDeliveryMethods(ct);
        return HandleResponse(result);
    }

    [HttpGet("GetDeliveryMethods/{pageNo}/{pageSize}")]
    [ProducesResponseType(typeof(ResponseInfo<List<DeliveryMethodDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<DeliveryMethodDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<DeliveryMethodDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<DeliveryMethodDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<DeliveryMethodDto>?>>> GetAllDeliveryMethods(int pageNo, int pageSize, CancellationToken ct)
    {
        if (pageNo < 1 || pageSize < 1)
            return BadRequest(ResponseInfo<List<DeliveryMethodDto>?>.Failure("Page and PageSize must be greater than 0.", HttpStatusCode.BadRequest));

        var result = await _service.GetAllDeliveryMethods(pageNo, pageSize, ct);
        return HandleResponse(result);
    }

    [HttpGet("GetDeliveryMethod/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<DeliveryMethodDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<DeliveryMethodDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<DeliveryMethodDto?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<DeliveryMethodDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<DeliveryMethodDto?>>> GetDeliveryMethod(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<DeliveryMethodDto?>.Failure("Invalid DeliveryMethod Id.", HttpStatusCode.BadRequest));

        var result = await _service.GetDeliveryMethodById(id, ct);
        return HandleResponse(result);
    }

    [HttpGet("SearchDeliveryMethod/{fieldName}")]
    [ProducesResponseType(typeof(ResponseInfo<List<DeliveryMethodDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<DeliveryMethodDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<DeliveryMethodDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<DeliveryMethodDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<DeliveryMethodDto>?>>> SearchDeliveryMethod(string fieldName, string searchString, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(fieldName) || string.IsNullOrWhiteSpace(searchString))
            return BadRequest(ResponseInfo<List<DeliveryMethodDto>?>.Failure("Field name and search string cannot be empty.", HttpStatusCode.BadRequest));

        var result = await _service.SearchDeliveryMethod(fieldName, searchString, ct);
        return HandleResponse(result);
    }

    [HttpPost("CreateDeliveryMethod")]
    [ProducesResponseType(typeof(ResponseInfo<DeliveryMethodDto?>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseInfo<DeliveryMethodDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<DeliveryMethodDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<DeliveryMethodDto?>>> CreateDeliveryMethod([FromBody] DeliveryMethodDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<DeliveryMethodDto?>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.AddDeliveryMethod(dto, ct);

        if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Created)
            return CreatedAtAction(nameof(CreateDeliveryMethod), new { id = result.Data?.DeliveryMethodId }, result);

        return HandleResponse(result);
    }

    [HttpPut("UpdateDeliveryMethod")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> UpdateDeliveryMethod([FromBody] DeliveryMethodDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.UpdateDeliveryMethod(dto, ct);
        return HandleResponse(result);
    }

    [HttpDelete("DeleteDeliveryMethod/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> DeleteDeliveryMethod(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid DeliveryMethod Id.", HttpStatusCode.BadRequest));

        var result = await _service.DeleteDeliveryMethod(id, ct);
        return HandleResponse(result);
    }
}
