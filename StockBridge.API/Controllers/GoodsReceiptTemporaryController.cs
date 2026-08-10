using Microsoft.AspNetCore.Mvc;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.GoodsReceiptTemporaries;
using StockBridge.Application.Interfaces;
using System.Net;

namespace StockBridge.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GoodsReceiptTemporaryController(IGoodsReceiptTemporaryService service) : BaseController
{
    private readonly IGoodsReceiptTemporaryService _service = service;

    [HttpGet("GetAllGoodsReceiptTemporaries")]
    [ProducesResponseType(typeof(ResponseInfo<List<GoodsReceiptTemporaryHeaderDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<GoodsReceiptTemporaryHeaderDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<GoodsReceiptTemporaryHeaderDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<GoodsReceiptTemporaryHeaderDto>?>>> GetAllGoodsReceiptTemporaries(CancellationToken ct)
    {
        var result = await _service.GetAllGoodsReceiptTemporaries(ct);
        return HandleResponse(result);
    }

    [HttpGet("GetGoodsReceiptTemporaryById/{headerId}")]
    [ProducesResponseType(typeof(ResponseInfo<GoodsReceiptTemporaryHeaderDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<GoodsReceiptTemporaryHeaderDto?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<GoodsReceiptTemporaryHeaderDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<GoodsReceiptTemporaryHeaderDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<GoodsReceiptTemporaryHeaderDto?>>> GetGoodsReceiptTemporaryById(long headerId, CancellationToken ct)
    {
        if (headerId <= 0)
            return BadRequest(ResponseInfo<GoodsReceiptTemporaryHeaderDto?>.Failure("Invalid GoodsReceiptTemporary Id.", HttpStatusCode.BadRequest));

        var result = await _service.GetGoodsReceiptTemporaryById(headerId, ct);
        return HandleResponse(result);
    }

    [HttpPost("CreateGoodsReceiptTemporary")]
    [ProducesResponseType(typeof(ResponseInfo<GoodsReceiptTemporaryHeaderDto?>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseInfo<GoodsReceiptTemporaryHeaderDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<GoodsReceiptTemporaryHeaderDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<GoodsReceiptTemporaryHeaderDto?>>> CreateGoodsReceiptTemporary([FromBody] CreateGoodsReceiptTemporaryHeaderDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<GoodsReceiptTemporaryHeaderDto?>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.AddGoodsReceiptTemporary(dto, ct);

        if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Created)
            return CreatedAtAction(nameof(CreateGoodsReceiptTemporary), new { id = result.Data?.GoodsReceiptTemporaryHeaderId }, result);

        return HandleResponse(result);
    }

    [HttpPut("UpdateGoodsReceiptTemporary")]
    [ProducesResponseType(typeof(ResponseInfo<GoodsReceiptTemporaryHeaderDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<GoodsReceiptTemporaryHeaderDto?>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<GoodsReceiptTemporaryHeaderDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<GoodsReceiptTemporaryHeaderDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<GoodsReceiptTemporaryHeaderDto?>>> UpdateGoodsReceiptTemporary([FromBody] UpdateGoodsReceiptTemporaryHeaderDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<GoodsReceiptTemporaryHeaderDto?>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.UpdateGoodsReceiptTemporary(dto, ct);
        return HandleResponse(result);
    }

    [HttpDelete("DeleteGoodsReceiptTemporary/{headerId}")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> DeleteGoodsReceiptTemporary(long headerId, CancellationToken ct)
    {
        if (headerId <= 0)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid GoodsReceiptTemporary Id.", HttpStatusCode.BadRequest));

        var result = await _service.DeleteGoodsReceiptTemporary(headerId, ct);
        return HandleResponse(result);
    }

    [HttpGet("ValidateGoodsReceiptTemporary/{headerId}")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> ValidateGoodsReceiptTemporary(long headerId, CancellationToken ct)
    {
        if (headerId <= 0)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid GoodsReceiptTemporary Id.", HttpStatusCode.BadRequest));

        var result = await _service.IsExist(headerId, ct);
        return HandleResponse(result);
    }
}