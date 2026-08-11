using Microsoft.AspNetCore.Mvc;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.Formulas;
using StockBridge.Application.Interfaces;
using System.Net;

namespace StockBridge.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FormulaController(IFormulaService service) : BaseController
{
    private readonly IFormulaService _service = service;

    [HttpGet("GetAllFormulas")]
    [ProducesResponseType(typeof(ResponseInfo<List<FormulaHeaderDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<FormulaHeaderDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<FormulaHeaderDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<FormulaHeaderDto>?>>> GetAllFormulas(CancellationToken ct)
    {
        var result = await _service.GetAllFormulas(ct);
        return HandleResponse(result);
    }

    [HttpGet("GetFormulaById/{headerId}")]
    [ProducesResponseType(typeof(ResponseInfo<FormulaHeaderDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<FormulaHeaderDto?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<FormulaHeaderDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<FormulaHeaderDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<FormulaHeaderDto?>>> GetFormulaById(int headerId, CancellationToken ct)
    {
        if (headerId <= 0)
            return BadRequest(ResponseInfo<FormulaHeaderDto?>.Failure("Invalid Formula Id.", HttpStatusCode.BadRequest));

        var result = await _service.GetFormulaById(headerId, ct);
        return HandleResponse(result);
    }

    [HttpPost("CreateFormula")]
    [ProducesResponseType(typeof(ResponseInfo<FormulaHeaderDto?>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseInfo<FormulaHeaderDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<FormulaHeaderDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<FormulaHeaderDto?>>> CreateFormula([FromBody] CreateFormulaHeaderDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<FormulaHeaderDto?>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.AddFormula(dto, ct);

        if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Created)
            return CreatedAtAction(nameof(CreateFormula), new { id = result.Data?.FormulaHeaderId }, result);

        return HandleResponse(result);
    }

    [HttpPut("UpdateFormula")]
    [ProducesResponseType(typeof(ResponseInfo<FormulaHeaderDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<FormulaHeaderDto?>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<FormulaHeaderDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<FormulaHeaderDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<FormulaHeaderDto?>>> UpdateFormula([FromBody] UpdateFormulaHeaderDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<FormulaHeaderDto?>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.UpdateFormula(dto, ct);
        return HandleResponse(result);
    }

    [HttpDelete("DeleteFormula/{headerId}")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> DeleteFormula(int headerId, CancellationToken ct)
    {
        if (headerId <= 0)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid Formula Id.", HttpStatusCode.BadRequest));

        var result = await _service.DeleteFormula(headerId, ct);
        return HandleResponse(result);
    }

    [HttpGet("ValidateFormula/{headerId}")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> ValidateFormula(int headerId, CancellationToken ct)
    {
        if (headerId <= 0)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid Formula Id.", HttpStatusCode.BadRequest));

        var result = await _service.IsExist(headerId, ct);
        return HandleResponse(result);
    }
}