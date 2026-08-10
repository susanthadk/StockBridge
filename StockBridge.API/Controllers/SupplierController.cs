using Microsoft.AspNetCore.Mvc;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.Suppliers;
using StockBridge.Application.Interfaces;
using System.Net;

namespace StockBridge.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SupplierController(ISupplierService service) : BaseController
{
    private readonly ISupplierService _service = service;

    [HttpGet("GetAllSuppliers")]
    [ProducesResponseType(typeof(ResponseInfo<List<SupplierDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<SupplierDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<SupplierDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<SupplierDto>?>>> GetAllSuppliers(CancellationToken ct)
    {
        var result = await _service.GetAllSuppliers(ct);
        return HandleResponse(result);
    }

    [HttpGet("GetSuppliers/{pageNo}/{pageSize}")]
    [ProducesResponseType(typeof(ResponseInfo<List<SupplierDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<SupplierDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<SupplierDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<SupplierDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<SupplierDto>?>>> GetAllSuppliers(int pageNo, int pageSize, CancellationToken ct)
    {
        if (pageNo < 1 || pageSize < 1)
            return BadRequest(ResponseInfo<List<SupplierDto>?>.Failure("Page and PageSize must be greater than 0.", HttpStatusCode.BadRequest));

        var result = await _service.GetAllSuppliers(pageNo, pageSize, ct);
        return HandleResponse(result);
    }

    [HttpGet("GetSupplier/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<SupplierDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<SupplierDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<SupplierDto?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<SupplierDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<SupplierDto?>>> GetSupplier(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<SupplierDto?>.Failure("Invalid Supplier Id.", HttpStatusCode.BadRequest));

        var result = await _service.GetSupplierById(id, ct);
        return HandleResponse(result);
    }

    [HttpGet("SearchSupplier/{fieldName}")]
    [ProducesResponseType(typeof(ResponseInfo<List<SupplierDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<SupplierDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<SupplierDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<SupplierDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<SupplierDto>?>>> SearchSupplier(string fieldName, string searchString, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(fieldName) || string.IsNullOrWhiteSpace(searchString))
            return BadRequest(ResponseInfo<List<SupplierDto>?>.Failure("Field name and search string cannot be empty.", HttpStatusCode.BadRequest));

        var result = await _service.SearchSupplier(fieldName, searchString, ct);
        return HandleResponse(result);
    }

    [HttpPost("CreateSupplier")]
    [ProducesResponseType(typeof(ResponseInfo<SupplierDto?>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseInfo<SupplierDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<SupplierDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<SupplierDto?>>> CreateSupplier([FromBody] SupplierDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<SupplierDto?>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.AddSupplier(dto, ct);

        if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Created)
            return CreatedAtAction(nameof(CreateSupplier), new { id = result.Data?.SupplierId }, result);

        return HandleResponse(result);
    }

    [HttpPut("UpdateSupplier")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> UpdateSupplier([FromBody] SupplierDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.UpdateSupplier(dto, ct);
        return HandleResponse(result);
    }

    [HttpDelete("DeleteSupplier/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> DeleteSupplier(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid Supplier Id.", HttpStatusCode.BadRequest));

        var result = await _service.DeleteSupplier(id, ct);
        return HandleResponse(result);
    }
}