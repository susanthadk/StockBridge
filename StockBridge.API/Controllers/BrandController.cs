using Microsoft.AspNetCore.Mvc;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.Brands;
using StockBridge.Application.Interfaces;
using System.Net;

namespace StockBridge.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BrandController(IBrandService service) : BaseController
{
    private readonly IBrandService _service = service;

    [HttpGet("GetAllBrands")]
    [ProducesResponseType(typeof(ResponseInfo<List<BrandDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<BrandDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<BrandDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<BrandDto>?>>> GetAllBrands(CancellationToken ct)
    {
        var result = await _service.GetAllBrands(ct);
        return HandleResponse(result);
    }

    [HttpGet("GetBrands/{pageNo}/{pageSize}")]
    [ProducesResponseType(typeof(ResponseInfo<List<BrandDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<BrandDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<BrandDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<BrandDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<BrandDto>?>>> GetAllBrands(int pageNo, int pageSize, CancellationToken ct)
    {
        if (pageNo < 1 || pageSize < 1)
            return BadRequest(ResponseInfo<List<BrandDto>?>.Failure("Page and PageSize must be greater than 0.", HttpStatusCode.BadRequest));

        var result = await _service.GetAllBrands(pageNo, pageSize, ct);
        return HandleResponse(result);
    }

    [HttpGet("GetBrand/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<BrandDto?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<BrandDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<BrandDto?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<BrandDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<BrandDto?>>> GetBrand(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<BrandDto?>.Failure("Invalid Brand Id.", HttpStatusCode.BadRequest));

        var result = await _service.GetBrandById(id, ct);
        return HandleResponse(result);
    }

    [HttpGet("SearchBrand/{fieldName}")]
    [ProducesResponseType(typeof(ResponseInfo<List<BrandDto>?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<List<BrandDto>?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<List<BrandDto>?>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseInfo<List<BrandDto>?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<List<BrandDto>?>>> SearchBrand(string fieldName, string searchString, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(fieldName) || string.IsNullOrWhiteSpace(searchString))
            return BadRequest(ResponseInfo<List<BrandDto>?>.Failure("Field name and search string cannot be empty.", HttpStatusCode.BadRequest));

        var result = await _service.SearchBrand(fieldName, searchString, ct);
        return HandleResponse(result);
    }

    [HttpPost("CreateBrand")]
    [ProducesResponseType(typeof(ResponseInfo<BrandDto?>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseInfo<BrandDto?>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<BrandDto?>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<BrandDto?>>> CreateBrand([FromBody] BrandDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<BrandDto?>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.AddBrand(dto, ct);

        if (result.StatusCode == HttpStatusCode.OK || result.StatusCode == HttpStatusCode.Created)
            return CreatedAtAction(nameof(CreateBrand), new { id = result.Data?.BrandId }, result);

        return HandleResponse(result);
    }

    [HttpPut("UpdateBrand")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> UpdateBrand([FromBody] BrandDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid model.", HttpStatusCode.BadRequest));

        var result = await _service.UpdateBrand(dto, ct);
        return HandleResponse(result);
    }

    [HttpDelete("DeleteBrand/{id}")]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseInfo<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ResponseInfo<bool>>> DeleteBrand(int id, CancellationToken ct)
    {
        if (id <= 0)
            return BadRequest(ResponseInfo<bool>.Failure("Invalid Brand Id.", HttpStatusCode.BadRequest));

        var result = await _service.DeleteBrand(id, ct);
        return HandleResponse(result);
    }
}