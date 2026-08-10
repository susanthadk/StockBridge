using Microsoft.AspNetCore.Mvc;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.Companies;
using StockBridge.Application.Interfaces;
using System.Net;

namespace StockBridge.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompanyController(ICompanyService service) : BaseController
{
    private readonly ICompanyService _service = service;

    [HttpGet("GetAllCompanies")]
    [ProducesResponseType(typeof(ResponseInfo<List<CompanyDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<CompanyDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<CompanyDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<CompanyDto>?>>> GetAllCompanies(CancellationToken ct)
    {
        var result = await _service.GetAllCompanies(ct);
        return HandleResponse(result);
    }

    [HttpGet("GetCompanies/{pageNo}/{pageSize}")]
    [ProducesResponseType(typeof(ResponseInfo<List<CompanyDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<CompanyDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<CompanyDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<CompanyDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<CompanyDto>?>>> GetAllCompanies(int pageNo, int pageSize, CancellationToken ct)
    {
        if (pageNo < 1 || pageSize < 1)
            return BadRequest(ResponseInfo<List<CompanyDto>?>.Failure("Page and PageSize must be greater than 0.", HttpStatusCode.BadRequest));

        var result = await _service.GetAllCompanies(pageNo, pageSize, ct);
        return HandleResponse(result);
    }

    [HttpGet("GetCompany/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<CompanyDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<CompanyDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<CompanyDto?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<CompanyDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<CompanyDto?>>> GetCompany(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<CompanyDto?>.Failure("Invalid Company Id.", HttpStatusCode.BadRequest));

        var result = await _service.GetCompanyById(id, ct);
        return HandleResponse(result);
    }

    [HttpGet("SearchCompany/{fieldName}")]
    [ProducesResponseType(typeof(ResponseInfo<List<CompanyDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<CompanyDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<CompanyDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<CompanyDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<CompanyDto>?>>> SearchCompany(string fieldName, string searchString, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(fieldName) || string.IsNullOrWhiteSpace(searchString))
            return BadRequest(ResponseInfo<List<CompanyDto>?>.Failure("Field name and search string cannot be empty.", HttpStatusCode.BadRequest));

        var result = await _service.SearchCompany(fieldName, searchString, ct);
        return HandleResponse(result);
    }

    [HttpPost("CreateCompany")]
    [ProducesResponseType(typeof(ResponseInfo<CompanyDto?>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseInfo<CompanyDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<CompanyDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<CompanyDto?>>> CreateCompany([FromBody] CompanyDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<CompanyDto?>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.AddCompany(dto, ct);

        if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Created)
            return CreatedAtAction(nameof(CreateCompany), new { id = result.Data?.CompanyId }, result);

        return HandleResponse(result);
    }

    [HttpPut("UpdateCompany")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> UpdateCompany([FromBody] CompanyDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.UpdateCompany(dto, ct);
        return HandleResponse(result);
    }

    [HttpDelete("DeleteCompany/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> DeleteCompany(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid Company Id.", HttpStatusCode.BadRequest));

        var result = await _service.DeleteCompany(id, ct);
        return HandleResponse(result);
    }
}