// ***********************************************************************
// Assembly         : BuisnessLogicLayer
// Author           : Slava
// Created          : 12-05-2022
//
// Last Modified By : Slava
// Last Modified On : 12-05-2022
// ***********************************************************************
// <copyright file="CategoryService.cs" company="BuisnessLogicLayer">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using BuisnessLogicLayer.Validation;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace BuisnessLogicLayer.Services;

/// <summary>
/// Class CategoryService.
/// Implements the <see cref="ICategoryService" />
/// </summary>
/// <seealso cref="ICategoryService" />
public class CategoryService : ICategoryService
{
    /// <summary>
    /// The unit of work
    /// </summary>
    private readonly IUnitOfWork _unitOfWork;
    /// <summary>
    /// The mapper
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="CategoryService"/> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work.</param>
    /// <param name="mapper">The mapper.</param>
    public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    /// <summary>
    /// Get all as an asynchronous operation.
    /// </summary>
    /// <returns>A Task&lt;IEnumerable`1&gt; representing the asynchronous operation.</returns>
    public async Task<IEnumerable<CategoryModel>> GetAllAsync()
    {
        IEnumerable<Category?> categories =  await _unitOfWork.CategoryRepository.GetAllAsync();
        List<CategoryModel> categoryModels = new List<CategoryModel>();

        foreach (var c in categories)
        {
            categoryModels.Add(_mapper.Map<CategoryModel>(c));
        }

        return categoryModels;

    }

    /// <summary>
    /// Get by identifier as an asynchronous operation.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>A Task&lt;CategoryModel&gt; representing the asynchronous operation.</returns>
    public async Task<CategoryModel?> GetByIdAsync(int id)
    {
        return _mapper.Map<CategoryModel>(await _unitOfWork.CategoryRepository.GetByIdAsync(id));
    }

    /// <summary>
    /// Add as an asynchronous operation.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task AddAsync(CategoryModel model)
    {
        await _unitOfWork.CategoryRepository.AddAsync(_mapper.Map<Category>(model));
        await _unitOfWork.SaveAsync();
    }

    /// <summary>
    /// Update as an asynchronous operation.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task UpdateAsync(CategoryModel model)
    {
        _unitOfWork.CategoryRepository.Update(_mapper.Map<Category>(model));
        await _unitOfWork.SaveAsync();

    }

    /// <summary>
    /// Delete as an asynchronous operation.
    /// </summary>
    /// <param name="modelId">The model identifier.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task DeleteAsync(int modelId)
    {
       await _unitOfWork.CategoryRepository.Delete(modelId);
       await _unitOfWork.SaveAsync();

    }

    
    public async Task AddCategoryAsync(int postId, CategoryModel categoryModel)
    {
        Post? post = await _unitOfWork.PostRepository.GetByIdAsync(postId, "PostCategories");
        
        if ( post == null)
        {
            throw new PersonalBlogException("Post not found");
        }
        
        Category? category = await _unitOfWork.CategoryRepository.GetByValueOneAsync(category => category.Title == categoryModel.Title);
        
        if(category == null )
        {
            category = _mapper.Map<Category>(categoryModel);
            await _unitOfWork.CategoryRepository.AddAsync(category);
        }

        post.PostCategories.Add(new PostCategory()
        {
            Category = category,
            Post = post
        });
            
        await _unitOfWork.SaveAsync();
    }
    
    public async Task<IEnumerable<CategoryModel>> GetCategoriesAsync(int postId)
    {
        Post? post = (await _unitOfWork.PostRepository.GetByIdAsync(postId, "PostCategories"));

        if (post == null)
        {
            throw new PersonalBlogException ("Post not found");
        }
        
        List<CategoryModel> categoryModels = new List<CategoryModel>();

        foreach (var item in post.PostCategories.Select(pt => pt.Category))
            categoryModels.Add(_mapper.Map<CategoryModel>(item));

        return categoryModels;

    }
}