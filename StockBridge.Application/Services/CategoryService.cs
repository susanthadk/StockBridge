using AutoMapper;
using Microsoft.Extensions.Logging;
using StockBridge.Application.Common;
using StockBridge.Application.DTOs.Categories;
using StockBridge.Application.Interfaces;
using StockBridge.Domain.Entities;
using StockBridge.Domain.Interfaces;
using System.Net;

namespace StockBridge.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly IRepository<Category> _categoryRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly ILogger<CategoryService> _logger;

    public CategoryService(
        IRepository<Category> categoryRepository,
        ICurrentUserService currentUserService,
        IMapper mapper,
        ILogger<CategoryService> logger)
    {
        _categoryRepository = categoryRepository;
        _currentUserService = currentUserService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ResponseInfo<List<CategoryDto>?>> GetAllCategories(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all Categories.");

        var result = await _categoryRepository.GetAllAsync();

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No Categories found.", nameof(CategoryService), nameof(GetAllCategories));
            return ResponseInfo<List<CategoryDto>?>.Success(new List<CategoryDto>(), HttpStatusCode.NoContent, "No Categories found.");
        }

        var dtos = _mapper.Map<List<CategoryDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} Categories.", nameof(CategoryService), nameof(GetAllCategories), dtos.Count);

        return ResponseInfo<List<CategoryDto>?>.Success(dtos, HttpStatusCode.OK, "Categories retrieved successfully.");
    }

    public async Task<ResponseInfo<List<CategoryDto>?>> GetAllCategories(int pageNo, int pageSize, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all Categories. Page: {PageNo}, Size: {PageSize}", pageNo, pageSize);

        var result = await _categoryRepository.GetPagedAsync(pageNo, pageSize);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No Categories found.", nameof(CategoryService), nameof(GetAllCategories));
            return ResponseInfo<List<CategoryDto>?>.Success(new List<CategoryDto>(), HttpStatusCode.NoContent, "No Categories found.");
        }

        var dtos = _mapper.Map<List<CategoryDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} Categories.", nameof(CategoryService), nameof(GetAllCategories), dtos.Count);

        return ResponseInfo<List<CategoryDto>?>.Success(dtos, HttpStatusCode.OK, "Categories retrieved successfully.");
    }

    public async Task<ResponseInfo<CategoryDto?>> GetCategoryById(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching Category Id: {CategoryId}", id);

        var result = await _categoryRepository.GetByIdAsync(id, nameof(Category.CategoryId));

        if (result == null)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: Category not found Id: {CategoryId}.", nameof(CategoryService), nameof(GetCategoryById), id);
            return ResponseInfo<CategoryDto?>.Success(null, HttpStatusCode.NoContent, "Category not found.");
        }

        var dto = _mapper.Map<CategoryDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved Category Id: {CategoryId}.", nameof(CategoryService), nameof(GetCategoryById), id);

        return ResponseInfo<CategoryDto?>.Success(dto, HttpStatusCode.OK, "Category retrieved successfully.");
    }

    public async Task<ResponseInfo<List<CategoryDto>?>> SearchCategory(string fieldName, string searchString, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Searching Categories by {FieldName}: {SearchString}", fieldName, searchString);

        var result = await _categoryRepository.GetByFieldAsync(fieldName, searchString);

        if (result == null || !result.Any())
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: No Categories found.", nameof(CategoryService), nameof(SearchCategory));
            return ResponseInfo<List<CategoryDto>?>.Success(new List<CategoryDto>(), HttpStatusCode.NoContent, "No Categories found.");
        }

        var dtos = _mapper.Map<List<CategoryDto>>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Retrieved {Count} Categories.", nameof(CategoryService), nameof(SearchCategory), dtos.Count);

        return ResponseInfo<List<CategoryDto>?>.Success(dtos, HttpStatusCode.OK, "Categories retrieved successfully.");
    }

    public async Task<ResponseInfo<CategoryDto?>> AddCategory(CategoryDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Adding Category.");

        var existing = await _categoryRepository.GetByFieldAsync("CategoryCode", dto.CategoryCode);
        if (existing?.Any(x => x.DepartmentCode == dto.DepartmentCode) == true)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: Category already exists with the same Category Code and Department Code.", nameof(CategoryService), nameof(AddCategory));
            return ResponseInfo<CategoryDto?>.Failure("Category already exists with the same Category Code and Department Code.", HttpStatusCode.BadRequest);
        }

        var entity = _mapper.Map<Category>(dto);
        entity.CreatedBy = _currentUserService.UserId ?? 0;
        entity.CreatedOn = DateTime.UtcNow;
        entity.IsActive = true;

        var result = await _categoryRepository.AddAsync(entity);

        var resultDto = _mapper.Map<CategoryDto>(result);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Category added successfully CategoryId: {CategoryId}.", nameof(CategoryService), nameof(AddCategory), result.CategoryId);

        return ResponseInfo<CategoryDto?>.Success(resultDto, HttpStatusCode.Created, "Category added successfully.");
    }

    public async Task<ResponseInfo<bool>> UpdateCategory(CategoryDto dto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating Category Id: {CategoryId}", dto.CategoryId);

        var isExists = await _categoryRepository.IsExistByIdAsync(dto.CategoryId, nameof(Category.CategoryId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: Category not found Id: {CategoryId}.", nameof(CategoryService), nameof(UpdateCategory), dto.CategoryId);
            return ResponseInfo<bool>.Failure("Category not found.", HttpStatusCode.NotFound);
        }

        var entity = _mapper.Map<Category>(dto);
        entity.ModifiedBy = _currentUserService.UserId;
        entity.ModifiedOn = DateTime.UtcNow;

        await _categoryRepository.UpdateAsync(entity);

        _logger.LogInformation("{ClassName} - {MethodName} Information: Category updated Id: {CategoryId}.", nameof(CategoryService), nameof(UpdateCategory), dto.CategoryId);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "Category updated successfully.");
    }

    public async Task<ResponseInfo<bool>> DeleteCategory(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting Category Id: {CategoryId}", id);

        var isExists = await _categoryRepository.IsExistByIdAsync(id, nameof(Category.CategoryId));

        if (!isExists)
        {
            _logger.LogInformation("{ClassName} - {MethodName} Information: Category not found Id: {CategoryId}.", nameof(CategoryService), nameof(DeleteCategory), id);
            return ResponseInfo<bool>.Failure("Category not found.", HttpStatusCode.NotFound);
        }

        await _categoryRepository.DeleteAsync(id, nameof(Category.CategoryId));

        _logger.LogInformation("{ClassName} - {MethodName} Information: Category deleted Id: {CategoryId}.", nameof(CategoryService), nameof(DeleteCategory), id);

        return ResponseInfo<bool>.Success(true, HttpStatusCode.OK, "Category deleted successfully.");
    }
}