using Microsoft.AspNetCore.Mvc;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.Departments;
using StockBridge.Application.Interfaces;
using System.Net;

namespace StockBridge.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartmentController(IDepartmentService service) : BaseController
{
    private readonly IDepartmentService _service = service;

    [HttpGet("GetAllDepartments")]
    [ProducesResponseType(typeof(ResponseInfo<List<DepartmentDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<DepartmentDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<DepartmentDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<DepartmentDto>?>>> GetAllDepartments(CancellationToken ct)
    {
        var result = await _service.GetAllDepartments(ct);
        return HandleResponse(result);
    }

    [HttpGet("GetDepartments/{pageNo}/{pageSize}")]
    [ProducesResponseType(typeof(ResponseInfo<List<DepartmentDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<DepartmentDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<DepartmentDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<DepartmentDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<DepartmentDto>?>>> GetAllDepartments(int pageNo, int pageSize, CancellationToken ct)
    {
        if (pageNo < 1 || pageSize < 1)
            return BadRequest(ResponseInfo<List<DepartmentDto>?>.Failure("Page and PageSize must be greater than 0.", HttpStatusCode.BadRequest));

        var result = await _service.GetAllDepartments(pageNo, pageSize, ct);
        return HandleResponse(result);
    }

    [HttpGet("GetDepartment/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<DepartmentDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<DepartmentDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<DepartmentDto?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<DepartmentDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<DepartmentDto?>>> GetDepartment(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<DepartmentDto?>.Failure("Invalid Department Id.", HttpStatusCode.BadRequest));

        var result = await _service.GetDepartmentById(id, ct);
        return HandleResponse(result);
    }

    [HttpGet("SearchDepartment/{fieldName}")]
    [ProducesResponseType(typeof(ResponseInfo<List<DepartmentDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<DepartmentDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<DepartmentDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<DepartmentDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<DepartmentDto>?>>> SearchDepartment(string fieldName, string searchString, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(fieldName) || string.IsNullOrWhiteSpace(searchString))
            return BadRequest(ResponseInfo<List<DepartmentDto>?>.Failure("Field name and search string cannot be empty.", HttpStatusCode.BadRequest));

        var result = await _service.SearchDepartment(fieldName, searchString, ct);
        return HandleResponse(result);
    }

    [HttpPost("CreateDepartment")]
    [ProducesResponseType(typeof(ResponseInfo<DepartmentDto?>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseInfo<DepartmentDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<DepartmentDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<DepartmentDto?>>> CreateDepartment([FromBody] DepartmentDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<DepartmentDto?>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.AddDepartment(dto, ct);

        if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Created)
            return CreatedAtAction(nameof(CreateDepartment), new { id = result.Data?.DepartmentId }, result);

        return HandleResponse(result);
    }

    [HttpPut("UpdateDepartment")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> UpdateDepartment([FromBody] DepartmentDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.UpdateDepartment(dto, ct);
        return HandleResponse(result);
    }

    [HttpDelete("DeleteDepartment/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> DeleteDepartment(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid Department Id.", HttpStatusCode.BadRequest));

        var result = await _service.DeleteDepartment(id, ct);
        return HandleResponse(result);
    }
}