using Microsoft.AspNetCore.Mvc;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.AreaRoutes;
using StockBridge.Application.Interfaces;
using System.Net;

namespace StockBridge.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AreaRouteController(IAreaRouteService service) : BaseController
{
    private readonly IAreaRouteService _service = service;

    [HttpGet("GetAllAreaRoutes")]
    [ProducesResponseType(typeof(ResponseInfo<List<AreaRouteDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<AreaRouteDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<AreaRouteDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<AreaRouteDto>?>>> GetAllAreaRoutes(CancellationToken ct)
    {
        var result = await _service.GetAllAreaRoutes(ct);
        return HandleResponse(result);
    }

    [HttpGet("GetAreaRoutes/{pageNo}/{pageSize}")]
    [ProducesResponseType(typeof(ResponseInfo<List<AreaRouteDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<AreaRouteDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<AreaRouteDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<AreaRouteDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<AreaRouteDto>?>>> GetAllAreaRoutes(int pageNo, int pageSize, CancellationToken ct)
    {
        if (pageNo < 1 || pageSize < 1)
            return BadRequest(ResponseInfo<List<AreaRouteDto>?>.Failure("Page and PageSize must be greater than 0.", HttpStatusCode.BadRequest));

        var result = await _service.GetAllAreaRoutes(pageNo, pageSize, ct);
        return HandleResponse(result);
    }

    [HttpGet("GetAreaRoute/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<AreaRouteDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<AreaRouteDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<AreaRouteDto?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<AreaRouteDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<AreaRouteDto?>>> GetAreaRoute(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<AreaRouteDto?>.Failure("Invalid AreaRoute Id.", HttpStatusCode.BadRequest));

        var result = await _service.GetAreaRouteById(id, ct);
        return HandleResponse(result);
    }

    [HttpGet("SearchAreaRoute/{fieldName}")]
    [ProducesResponseType(typeof(ResponseInfo<List<AreaRouteDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<AreaRouteDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<AreaRouteDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<AreaRouteDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<AreaRouteDto>?>>> SearchAreaRoute(string fieldName, string searchString, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(fieldName) || string.IsNullOrWhiteSpace(searchString))
            return BadRequest(ResponseInfo<List<AreaRouteDto>?>.Failure("Field name and search string cannot be empty.", HttpStatusCode.BadRequest));

        var result = await _service.SearchAreaRoute(fieldName, searchString, ct);
        return HandleResponse(result);
    }

    [HttpPost("CreateAreaRoute")]
    [ProducesResponseType(typeof(ResponseInfo<AreaRouteDto?>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseInfo<AreaRouteDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<AreaRouteDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<AreaRouteDto?>>> CreateAreaRoute([FromBody] AreaRouteDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<AreaRouteDto?>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.AddAreaRoute(dto, ct);

        if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Created)
            return CreatedAtAction(nameof(CreateAreaRoute), new { id = result.Data?.AreaRouteId }, result);

        return HandleResponse(result);
    }

    [HttpPut("UpdateAreaRoute")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> UpdateAreaRoute([FromBody] AreaRouteDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.UpdateAreaRoute(dto, ct);
        return HandleResponse(result);
    }

    [HttpDelete("DeleteAreaRoute/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> DeleteAreaRoute(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid AreaRoute Id.", HttpStatusCode.BadRequest));

        var result = await _service.DeleteAreaRoute(id, ct);
        return HandleResponse(result);
    }
}