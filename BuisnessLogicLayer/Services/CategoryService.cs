// ***********************************************************************
// Assembly         : BuisnessLogicLayer
// Author           : Slava
// Created          : 12-05-2022
//
// Last Modified By : Slava
// Last Modified On : 12-11-2022
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
    /// Initializes a new instance of the <see cref="CategoryService" /> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work.</param>
    /// <param name="mapper">The mapper.</param>
    public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    /// <summary>
    /// Get all categorymodels
    /// </summary>
    /// <returns></returns>
    public  IEnumerable<CategoryModel> GetAll()
    {
        IEnumerable<Category> categories =   _unitOfWork.CategoryRepository.GetAll();
        List<CategoryModel> categoryModels = new List<CategoryModel>();

        foreach (var c in categories)
        {
            categoryModels.Add(_mapper.Map<CategoryModel>(c));
        }

        return categoryModels;

    }
    /// <summary>
    /// Get categorymodel by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="PersonalBlogException"></exception>

    public  CategoryModel? GetById(int id)
    {
        var category =  _unitOfWork.CategoryRepository.GetById(id);
        if (category == null) throw new PersonalBlogException("Category not found");
        return _mapper.Map<CategoryModel>(category);
    }

    /// <summary>
    /// Add categorymodel
    /// </summary>
    /// <param name="model"></param>
    public void Add(CategoryModel model)
    {
         _unitOfWork.CategoryRepository.Add(_mapper.Map<Category>(model));
         _unitOfWork.SaveAsync();
    }

    /// <summary>
    /// Update categorymodel
    /// </summary>
    /// <param name="model"></param>
    public void Update(CategoryModel model)
    {
        _unitOfWork.CategoryRepository.Update(_mapper.Map<Category>(model));
         _unitOfWork.SaveAsync();

    }
    /// <summary>
    /// Delete categorymodel
    /// </summary>
    /// <param name="modelId"></param>

    public void Delete(int modelId)
    {
        _unitOfWork.CategoryRepository.Delete(modelId);
        _unitOfWork.SaveAsync();

    }
    /// <summary>
    /// Acc category
    /// </summary>
    /// <param name="postId"></param>
    /// <param name="categoryModel"></param>
    /// <exception cref="PersonalBlogException"></exception>
    
    public void AddCategory(int postId, CategoryModel categoryModel)
    {
        Post? post =  _unitOfWork.PostRepository.GetById(postId, "PostCategories");
        
        if ( post == null)
        {
            throw new PersonalBlogException("Post not found");
        }
        
        Category? category =  _unitOfWork.CategoryRepository.GetAll(category => category.Title == categoryModel.Title).FirstOrDefault();
        
        if(category == null )
        {
            category = _mapper.Map<Category>(categoryModel);
             _unitOfWork.CategoryRepository.Add(category);
        }

        post.PostCategories.Add(new PostCategory()
        {
            Category = category,
            Post = post
        });
            
         _unitOfWork.SaveAsync();
    }

    /// <summary>
    /// Get post's categories
    /// </summary>
    /// <param name="postId"></param>
    /// <returns></returns>
    /// <exception cref="PersonalBlogException"></exception>
    public IEnumerable<CategoryModel> GetCategories(int postId)
    {
        Post? post =  _unitOfWork.PostRepository.GetById(postId, "PostCategories");

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