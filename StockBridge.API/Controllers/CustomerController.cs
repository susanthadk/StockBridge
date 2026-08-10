using Microsoft.AspNetCore.Mvc;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.Customers;
using StockBridge.Application.Interfaces;
using System.Net;

namespace StockBridge.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController(ICustomerService service) : BaseController
{
    private readonly ICustomerService _service = service;

    [HttpGet("GetAllCustomers")]
    [ProducesResponseType(typeof(ResponseInfo<List<CustomerDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<CustomerDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<CustomerDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<CustomerDto>?>>> GetAllCustomers(CancellationToken ct)
    {
        var result = await _service.GetAllCustomers(ct);
        return HandleResponse(result);
    }

    [HttpGet("GetCustomers/{pageNo}/{pageSize}")]
    [ProducesResponseType(typeof(ResponseInfo<List<CustomerDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<CustomerDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<CustomerDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<CustomerDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<CustomerDto>?>>> GetAllCustomers(int pageNo, int pageSize, CancellationToken ct)
    {
        if (pageNo < 1 || pageSize < 1)
            return BadRequest(ResponseInfo<List<CustomerDto>?>.Failure("Page and PageSize must be greater than 0.", HttpStatusCode.BadRequest));

        var result = await _service.GetAllCustomers(pageNo, pageSize, ct);
        return HandleResponse(result);
    }

    [HttpGet("GetCustomer/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<CustomerDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<CustomerDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<CustomerDto?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<CustomerDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<CustomerDto?>>> GetCustomer(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<CustomerDto?>.Failure("Invalid Customer Id.", HttpStatusCode.BadRequest));

        var result = await _service.GetCustomerById(id, ct);
        return HandleResponse(result);
    }

    [HttpGet("SearchCustomer/{fieldName}")]
    [ProducesResponseType(typeof(ResponseInfo<List<CustomerDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<CustomerDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<CustomerDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<CustomerDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<CustomerDto>?>>> SearchCustomer(string fieldName, string searchString, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(fieldName) || string.IsNullOrWhiteSpace(searchString))
            return BadRequest(ResponseInfo<List<CustomerDto>?>.Failure("Field name and search string cannot be empty.", HttpStatusCode.BadRequest));

        var result = await _service.SearchCustomer(fieldName, searchString, ct);
        return HandleResponse(result);
    }

    [HttpPost("CreateCustomer")]
    [ProducesResponseType(typeof(ResponseInfo<CustomerDto?>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseInfo<CustomerDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<CustomerDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<CustomerDto?>>> CreateCustomer([FromBody] CustomerDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<CustomerDto?>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.AddCustomer(dto, ct);

        if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Created)
            return CreatedAtAction(nameof(CreateCustomer), new { id = result.Data?.CustomerId }, result);

        return HandleResponse(result);
    }

    [HttpPut("UpdateCustomer")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> UpdateCustomer([FromBody] CustomerDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.UpdateCustomer(dto, ct);
        return HandleResponse(result);
    }

    [HttpDelete("DeleteCustomer/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> DeleteCustomer(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid Customer Id.", HttpStatusCode.BadRequest));

        var result = await _service.DeleteCustomer(id, ct);
        return HandleResponse(result);
    }
}