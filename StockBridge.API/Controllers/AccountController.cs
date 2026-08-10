using Microsoft.AspNetCore.Mvc;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.Accounts;
using StockBridge.Application.Interfaces;
using System.Net;

namespace StockBridge.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController(IAccountService service) : BaseController
{
    private readonly IAccountService _service = service;

    [HttpGet("GetAllAccounts")]
    [ProducesResponseType(typeof(ResponseInfo<List<AccountDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<AccountDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<AccountDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<AccountDto>?>>> GetAllAccounts(CancellationToken ct)
    {
        var result = await _service.GetAllAccounts(ct);
        return HandleResponse(result);
    }

    [HttpGet("GetAccounts/{pageNo}/{pageSize}")]
    [ProducesResponseType(typeof(ResponseInfo<List<AccountDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<AccountDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<AccountDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<AccountDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<AccountDto>?>>> GetAllAccounts(int pageNo, int pageSize, CancellationToken ct)
    {
        if (pageNo < 1 || pageSize < 1)
            return BadRequest(ResponseInfo<List<AccountDto>?>.Failure("Page and PageSize must be greater than 0.", HttpStatusCode.BadRequest));

        var result = await _service.GetAllAccounts(pageNo, pageSize, ct);
        return HandleResponse(result);
    }

    [HttpGet("GetAccount/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<AccountDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<AccountDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<AccountDto?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<AccountDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<AccountDto?>>> GetAccount(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<AccountDto?>.Failure("Invalid Account Id.", HttpStatusCode.BadRequest));

        var result = await _service.GetAccountById(id, ct);
        return HandleResponse(result);
    }

    [HttpGet("SearchAccount/{fieldName}")]
    [ProducesResponseType(typeof(ResponseInfo<List<AccountDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<AccountDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<AccountDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<AccountDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<AccountDto>?>>> SearchAccount(string fieldName, string searchString, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(fieldName) || string.IsNullOrWhiteSpace(searchString))
            return BadRequest(ResponseInfo<List<AccountDto>?>.Failure("Field name and search string cannot be empty.", HttpStatusCode.BadRequest));

        var result = await _service.SearchAccount(fieldName, searchString, ct);
        return HandleResponse(result);
    }

    [HttpPost("CreateAccount")]
    [ProducesResponseType(typeof(ResponseInfo<AccountDto?>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseInfo<AccountDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<AccountDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<AccountDto?>>> CreateAccount([FromBody] AccountDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<AccountDto?>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.AddAccount(dto, ct);

        if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Created)
            return CreatedAtAction(nameof(CreateAccount), new { id = result.Data?.AccountId }, result);

        return HandleResponse(result);
    }

    [HttpPut("UpdateAccount")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> UpdateAccount([FromBody] AccountDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.UpdateAccount(dto, ct);
        return HandleResponse(result);
    }

    [HttpDelete("DeleteAccount/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> DeleteAccount(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid Account Id.", HttpStatusCode.BadRequest));

        var result = await _service.DeleteAccount(id, ct);
        return HandleResponse(result);
    }
}