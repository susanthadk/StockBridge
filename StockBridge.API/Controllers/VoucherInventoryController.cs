using Microsoft.AspNetCore.Mvc;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.VoucherInventories;
using StockBridge.Application.Interfaces;
using System.Net;

namespace StockBridge.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VoucherInventoryController(IVoucherInventoryService service) : BaseController
{
    private readonly IVoucherInventoryService _service = service;

    [HttpGet("GetAllVoucherInventories")]
    [ProducesResponseType(typeof(ResponseInfo<List<VoucherInventoryHeaderDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<VoucherInventoryHeaderDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<VoucherInventoryHeaderDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<VoucherInventoryHeaderDto>?>>> GetAllVoucherInventories(CancellationToken ct)
    {
        var result = await _service.GetAllVoucherInventories(ct);
        return HandleResponse(result);
    }

    [HttpGet("GetVoucherInventoryById/{headerId}")]
    [ProducesResponseType(typeof(ResponseInfo<VoucherInventoryHeaderDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<VoucherInventoryHeaderDto?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<VoucherInventoryHeaderDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<VoucherInventoryHeaderDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<VoucherInventoryHeaderDto?>>> GetVoucherInventoryById(long headerId, CancellationToken ct)
    {
        if (headerId <= 0)
            return BadRequest(ResponseInfo<VoucherInventoryHeaderDto?>.Failure("Invalid VoucherInventory Id.", HttpStatusCode.BadRequest));

        var result = await _service.GetVoucherInventoryById(headerId, ct);
        return HandleResponse(result);
    }

    [HttpPost("CreateVoucherInventory")]
    [ProducesResponseType(typeof(ResponseInfo<VoucherInventoryHeaderDto?>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseInfo<VoucherInventoryHeaderDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<VoucherInventoryHeaderDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<VoucherInventoryHeaderDto?>>> CreateVoucherInventory([FromBody] CreateVoucherInventoryHeaderDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<VoucherInventoryHeaderDto?>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.AddVoucherInventory(dto, ct);

        if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Created)
            return CreatedAtAction(nameof(CreateVoucherInventory), new { id = result.Data?.VoucherInventoryHeaderId }, result);

        return HandleResponse(result);
    }

    [HttpPut("UpdateVoucherInventory")]
    [ProducesResponseType(typeof(ResponseInfo<VoucherInventoryHeaderDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<VoucherInventoryHeaderDto?>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<VoucherInventoryHeaderDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<VoucherInventoryHeaderDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<VoucherInventoryHeaderDto?>>> UpdateVoucherInventory([FromBody] UpdateVoucherInventoryHeaderDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<VoucherInventoryHeaderDto?>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.UpdateVoucherInventory(dto, ct);
        return HandleResponse(result);
    }

    [HttpDelete("DeleteVoucherInventory/{headerId}")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> DeleteVoucherInventory(long headerId, CancellationToken ct)
    {
        if (headerId <= 0)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid VoucherInventory Id.", HttpStatusCode.BadRequest));

        var result = await _service.DeleteVoucherInventory(headerId, ct);
        return HandleResponse(result);
    }

    [HttpGet("ValidateVoucherInventory/{headerId}")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> ValidateVoucherInventory(long headerId, CancellationToken ct)
    {
        if (headerId <= 0)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid VoucherInventory Id.", HttpStatusCode.BadRequest));

        var result = await _service.IsExist(headerId, ct);
        return HandleResponse(result);
    }
}