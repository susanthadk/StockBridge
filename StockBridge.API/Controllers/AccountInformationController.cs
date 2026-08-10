using Microsoft.AspNetCore.Mvc;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.AccountInformations;
using StockBridge.Application.Interfaces;
using System.Net;

namespace StockBridge.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountInformationController(IAccountInformationService service) : BaseController
{
    private readonly IAccountInformationService _service = service;

    [HttpGet("GetAllAccountInformations")]
    [ProducesResponseType(typeof(ResponseInfo<List<AccountInformationDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<AccountInformationDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<AccountInformationDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<AccountInformationDto>?>>> GetAllAccountInformations(CancellationToken ct)
    {
        var result = await _service.GetAllAccountInformations(ct);
        return HandleResponse(result);
    }

    [HttpGet("GetAccountInformations/{pageNo}/{pageSize}")]
    [ProducesResponseType(typeof(ResponseInfo<List<AccountInformationDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<AccountInformationDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<AccountInformationDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<AccountInformationDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<AccountInformationDto>?>>> GetAllAccountInformations(int pageNo, int pageSize, CancellationToken ct)
    {
        if (pageNo < 1 || pageSize < 1)
            return BadRequest(ResponseInfo<List<AccountInformationDto>?>.Failure("Page and PageSize must be greater than 0.", HttpStatusCode.BadRequest));

        var result = await _service.GetAllAccountInformations(pageNo, pageSize, ct);
        return HandleResponse(result);
    }

    [HttpGet("GetAccountInformation/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<AccountInformationDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<AccountInformationDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<AccountInformationDto?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<AccountInformationDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<AccountInformationDto?>>> GetAccountInformation(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<AccountInformationDto?>.Failure("Invalid AccountInformation Id.", HttpStatusCode.BadRequest));

        var result = await _service.GetAccountInformationById(id, ct);
        return HandleResponse(result);
    }

    [HttpGet("SearchAccountInformation/{fieldName}")]
    [ProducesResponseType(typeof(ResponseInfo<List<AccountInformationDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<AccountInformationDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<AccountInformationDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<AccountInformationDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<AccountInformationDto>?>>> SearchAccountInformation(string fieldName, string searchString, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(fieldName) || string.IsNullOrWhiteSpace(searchString))
            return BadRequest(ResponseInfo<List<AccountInformationDto>?>.Failure("Field name and search string cannot be empty.", HttpStatusCode.BadRequest));

        var result = await _service.SearchAccountInformation(fieldName, searchString, ct);
        return HandleResponse(result);
    }

    [HttpPost("CreateAccountInformation")]
    [ProducesResponseType(typeof(ResponseInfo<AccountInformationDto?>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseInfo<AccountInformationDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<AccountInformationDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<AccountInformationDto?>>> CreateAccountInformation([FromBody] AccountInformationDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<AccountInformationDto?>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.AddAccountInformation(dto, ct);

        if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Created)
            return CreatedAtAction(nameof(CreateAccountInformation), new { id = result.Data?.AccountInformationId }, result);

        return HandleResponse(result);
    }

    [HttpPut("UpdateAccountInformation")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> UpdateAccountInformation([FromBody] AccountInformationDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.UpdateAccountInformation(dto, ct);
        return HandleResponse(result);
    }

    [HttpDelete("DeleteAccountInformation/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> DeleteAccountInformation(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid AccountInformation Id.", HttpStatusCode.BadRequest));

        var result = await _service.DeleteAccountInformation(id, ct);
        return HandleResponse(result);
    }
}