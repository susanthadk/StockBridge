using Microsoft.AspNetCore.Mvc;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.Employees;
using StockBridge.Application.Interfaces;
using System.Net;

namespace StockBridge.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController(IEmployeeService service) : BaseController
{
    private readonly IEmployeeService _service = service;

    [HttpGet("GetAllEmployees")]
    [ProducesResponseType(typeof(ResponseInfo<List<EmployeeDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<EmployeeDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<EmployeeDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<EmployeeDto>?>>> GetAllEmployees(CancellationToken ct)
    {
        var result = await _service.GetAllEmployees(ct);
        return HandleResponse(result);
    }

    [HttpGet("GetEmployees/{pageNo}/{pageSize}")]
    [ProducesResponseType(typeof(ResponseInfo<List<EmployeeDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<EmployeeDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<EmployeeDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<EmployeeDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<EmployeeDto>?>>> GetAllEmployees(int pageNo, int pageSize, CancellationToken ct)
    {
        if (pageNo < 1 || pageSize < 1)
            return BadRequest(ResponseInfo<List<EmployeeDto>?>.Failure("Page and PageSize must be greater than 0.", HttpStatusCode.BadRequest));

        var result = await _service.GetAllEmployees(pageNo, pageSize, ct);
        return HandleResponse(result);
    }

    [HttpGet("GetEmployee/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<EmployeeDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<EmployeeDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<EmployeeDto?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<EmployeeDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<EmployeeDto?>>> GetEmployee(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<EmployeeDto?>.Failure("Invalid Employee Id.", HttpStatusCode.BadRequest));

        var result = await _service.GetEmployeeById(id, ct);
        return HandleResponse(result);
    }

    [HttpGet("SearchEmployee/{fieldName}")]
    [ProducesResponseType(typeof(ResponseInfo<List<EmployeeDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<EmployeeDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<EmployeeDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<EmployeeDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<EmployeeDto>?>>> SearchEmployee(string fieldName, string searchString, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(fieldName) || string.IsNullOrWhiteSpace(searchString))
            return BadRequest(ResponseInfo<List<EmployeeDto>?>.Failure("Field name and search string cannot be empty.", HttpStatusCode.BadRequest));

        var result = await _service.SearchEmployee(fieldName, searchString, ct);
        return HandleResponse(result);
    }

    [HttpPost("CreateEmployee")]
    [ProducesResponseType(typeof(ResponseInfo<EmployeeDto?>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseInfo<EmployeeDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<EmployeeDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<EmployeeDto?>>> CreateEmployee([FromBody] EmployeeDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<EmployeeDto?>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.AddEmployee(dto, ct);

        if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Created)
            return CreatedAtAction(nameof(CreateEmployee), new { id = result.Data?.EmployeeId }, result);

        return HandleResponse(result);
    }

    [HttpPut("UpdateEmployee")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> UpdateEmployee([FromBody] EmployeeDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.UpdateEmployee(dto, ct);
        return HandleResponse(result);
    }

    [HttpDelete("DeleteEmployee/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> DeleteEmployee(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid Employee Id.", HttpStatusCode.BadRequest));

        var result = await _service.DeleteEmployee(id, ct);
        return HandleResponse(result);
    }
}