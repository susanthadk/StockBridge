using Microsoft.AspNetCore.Mvc;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.InventoryTransactions;
using StockBridge.Application.Interfaces;
using System.Net;

namespace StockBridge.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InventoryTransactionController(IInventoryTransactionService service) : BaseController
{
    private readonly IInventoryTransactionService _service = service;

    [HttpGet("GetAllInventoryTransactions")]
    [ProducesResponseType(typeof(ResponseInfo<List<InventoryHeaderTransactionDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<InventoryHeaderTransactionDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<InventoryHeaderTransactionDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<InventoryHeaderTransactionDto>?>>> GetAllInventoryTransactions(CancellationToken ct)
    {
        var result = await _service.GetAllInventoryTransactions(ct);
        return HandleResponse(result);
    }

    [HttpGet("GetInventoryTransactionById/{headerId}")]
    [ProducesResponseType(typeof(ResponseInfo<InventoryHeaderTransactionDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<InventoryHeaderTransactionDto?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<InventoryHeaderTransactionDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<InventoryHeaderTransactionDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<InventoryHeaderTransactionDto?>>> GetInventoryTransactionById(long headerId, CancellationToken ct)
    {
        if (headerId <= 0)
            return BadRequest(ResponseInfo<InventoryHeaderTransactionDto?>.Failure("Invalid InventoryTransaction Id.", HttpStatusCode.BadRequest));

        var result = await _service.GetInventoryTransactionById(headerId, ct);
        return HandleResponse(result);
    }

    [HttpPost("CreateInventoryTransaction")]
    [ProducesResponseType(typeof(ResponseInfo<InventoryHeaderTransactionDto?>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseInfo<InventoryHeaderTransactionDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<InventoryHeaderTransactionDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<InventoryHeaderTransactionDto?>>> CreateInventoryTransaction([FromBody] CreateInventoryHeaderTransactionDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<InventoryHeaderTransactionDto?>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.AddInventoryTransaction(dto, ct);

        if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Created)
            return CreatedAtAction(nameof(CreateInventoryTransaction), new { id = result.Data?.InventoryHeaderTransactionId }, result);

        return HandleResponse(result);
    }

    [HttpPut("UpdateInventoryTransaction")]
    [ProducesResponseType(typeof(ResponseInfo<InventoryHeaderTransactionDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<InventoryHeaderTransactionDto?>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<InventoryHeaderTransactionDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<InventoryHeaderTransactionDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<InventoryHeaderTransactionDto?>>> UpdateInventoryTransaction([FromBody] UpdateInventoryHeaderTransactionDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<InventoryHeaderTransactionDto?>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.UpdateInventoryTransaction(dto, ct);
        return HandleResponse(result);
    }

    [HttpDelete("DeleteInventoryTransaction/{headerId}")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> DeleteInventoryTransaction(long headerId, CancellationToken ct)
    {
        if (headerId <= 0)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid InventoryTransaction Id.", HttpStatusCode.BadRequest));

        var result = await _service.DeleteInventoryTransaction(headerId, ct);
        return HandleResponse(result);
    }

    [HttpGet("ValidateInventoryTransaction/{headerId}")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> ValidateInventoryTransaction(long headerId, CancellationToken ct)
    {
        if (headerId <= 0)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid InventoryTransaction Id.", HttpStatusCode.BadRequest));

        var result = await _service.IsExist(headerId, ct);
        return HandleResponse(result);
    }
}