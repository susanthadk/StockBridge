using Microsoft.AspNetCore.Mvc;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.Accounts;
using StockBridge.Application.Interfaces;
using System.Net;

namespace StockBridge.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController(IAccountService service, ILogger<AccountController> logger) : ControllerBase
{
    private readonly IAccountService _service = service;
    private readonly ILogger<AccountController> _logger = logger;

    [HttpGet("GetAllAccounts")]
    [Produces("application/json", "application/xml", "application/x-yaml")]
    [ProducesResponseType(typeof(ResponseInfo<List<AccountDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<AccountDto>>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<List<AccountDto>>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<AccountDto>?>>> GetAllAccounts(CancellationToken cancellationToken)
    {
        try
        {
            var result = await _service.GetAllAccounts(cancellationToken);
            return ToActionResult(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error in GetAllAccounts.");
            return StatusCode(StatusCodes.Status500InternalServerError, ResponseInfo<List<AccountDto>?>.Failure("An unexpected error occurred.", HttpStatusCode.InternalServerError));
        }
    }

    [HttpGet("GetAccounts/{pageNo}/{pageSize}")]
    [Produces("application/json", "application/xml", "application/x-yaml")]
    [ProducesResponseType(typeof(ResponseInfo<List<AccountDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<AccountDto>>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<AccountDto>>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<List<AccountDto>>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<AccountDto>?>>> GetAllAccounts(int pageNo, int pageSize, CancellationToken cancellationToken)
    {
        if (pageNo < 1 || pageSize < 1)
        {
            return BadRequest(ResponseInfo<List<AccountDto>?>.Failure("Page and PageSize must be greater than 0", HttpStatusCode.BadRequest));
        }

        try
        {
            var result = await _service.GetAllAccounts(pageNo, pageSize, cancellationToken);
            return ToActionResult(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Unexpected error in GetAllAccounts in page {pageNo}.");
            return StatusCode(StatusCodes.Status500InternalServerError, ResponseInfo<List<AccountDto>?>.Failure("An unexpected error occurred.", HttpStatusCode.InternalServerError));
        }
    }

    [HttpGet("GetAccount/{id}")]
    [Produces("application/json", "application/xml", "application/x-yaml")]
    [ProducesResponseType(typeof(ResponseInfo<AccountDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<AccountDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<AccountDto?>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<AccountDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<AccountDto?>>> GetAccount(int id)
    {
        if (id <= 0)
        {
            return BadRequest(ResponseInfo<AccountDto?>.Failure("Invalid Account Id. Id must be greater than 0.", HttpStatusCode.BadRequest));
        }

        try
        {
            var result = await _service.GetAccountById(id);
            return ToActionResult(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Unexpected error getting Account with Id {id}.");
            return StatusCode(StatusCodes.Status500InternalServerError, ResponseInfo<AccountDto?>.Failure("An unexpected error occurred.", HttpStatusCode.InternalServerError));
        }
    }

    [HttpGet("SearchAccount/{fieldName}")]
    [Produces("application/json", "application/xml", "application/x-yaml")]
    [ProducesResponseType(typeof(ResponseInfo<List<AccountDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<AccountDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<AccountDto>?>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<List<AccountDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<AccountDto>?>>> SearchAccount(string fieldName, string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString) || string.IsNullOrWhiteSpace(fieldName))
        {
            return BadRequest(ResponseInfo<List<AccountDto>?>.Failure("Field name or search string cannot be empty.", HttpStatusCode.BadRequest));
        }

        try
        {
            var result = await _service.SearchAccount(fieldName, searchString);
            return ToActionResult(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Unexpected error searching Account with field {fieldName} & search string {searchString}.");
            return StatusCode(StatusCodes.Status500InternalServerError, ResponseInfo<List<AccountDto>?>.Failure("An unexpected error occurred.", HttpStatusCode.InternalServerError));
        }
    }

    [HttpPost("CreateAccount")]
    [Produces("application/json", "application/xml", "application/x-yaml")]
    [ProducesResponseType(typeof(ResponseInfo<AccountDto?>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseInfo<AccountDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<AccountDto?>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<AccountDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<AccountDto?>>> CreateAccount([FromBody] AccountDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ResponseInfo<AccountDto?>.Failure("Invalid model parameters.", HttpStatusCode.BadRequest));
        }

        try
        {
            var result = await _service.AddAccount(dto);
            return result.StatusCode switch
            {
                HttpStatusCode.Created => Created($"/api/accounts/{result.Data?.AccountId}", result),
                _ => ToActionResult(result)
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error creating an Account.");
            return StatusCode(StatusCodes.Status500InternalServerError, ResponseInfo<AccountDto?>.Failure("An unexpected error occurred.", HttpStatusCode.InternalServerError));
        }
    }

    [HttpPut("UpdateAccount")]
    [Produces("application/json", "application/xml", "application/x-yaml")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> UpdateAccount([FromBody] AccountDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ResponseInfo<bool>.Failure("Invalid model parameters.", HttpStatusCode.BadRequest));
        }

        try
        {
            var result = await _service.UpdateAccount(dto);
            return ToActionResult(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error updating Account.");
            return StatusCode(StatusCodes.Status500InternalServerError, ResponseInfo<bool>.Failure("An unexpected error occurred.", HttpStatusCode.InternalServerError));
        }
    }

    [HttpDelete("DeleteAccount/{id}")]
    [Produces("application/json", "application/xml", "application/x-yaml")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> DeleteAccount(int id)
    {
        if (id <= 0)
        {
            return BadRequest(ResponseInfo<bool>.Failure("Invalid Account Id. Id must be greater than 0.", HttpStatusCode.BadRequest));
        }

        try
        {
            var result = await _service.DeleteAccount(id);
            return ToActionResult(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Unexpected error deleting Account with Id {id}.");
            return StatusCode(StatusCodes.Status500InternalServerError, ResponseInfo<bool>.Failure("An unexpected error occurred.", HttpStatusCode.InternalServerError));
        }
    }

    private ActionResult ToActionResult<T>(ResponseInfo<T> result)
    {
        return result.StatusCode switch
        {
            HttpStatusCode.OK => Ok(result),
            HttpStatusCode.Created => Created(string.Empty, result),
            HttpStatusCode.NotFound => NotFound(result),
            HttpStatusCode.BadRequest => BadRequest(result),
            HttpStatusCode.InternalServerError => StatusCode(StatusCodes.Status500InternalServerError, result),
            _ => StatusCode(StatusCodes.Status500InternalServerError, result)
        };
    }
}