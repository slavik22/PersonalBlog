// ***********************************************************************
// Assembly         : BuisnessLogicLayer
// Author           : Slava
// Created          : 12-01-2022
//
// Last Modified By : Slava
// Last Modified On : 12-09-2022
// ***********************************************************************
// <copyright file="PostService.cs" company="BuisnessLogicLayer">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using PostStatus = DataAccessLayer.Enums.PostStatus;

namespace BuisnessLogicLayer.Services;

/// <summary>
/// Class PostService.
/// Implements the <see cref="IPostService" />
/// </summary>
/// <seealso cref="IPostService" />
public class PostService : IPostService
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
    /// Initializes a new instance of the <see cref="PostService"/> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work.</param>
    /// <param name="mapper">The mapper.</param>
    public PostService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    /// <summary>
    /// Get all as an asynchronous operation.
    /// </summary>
    /// <returns>A Task&lt;IEnumerable`1&gt; representing the asynchronous operation.</returns>
    public async Task<IEnumerable<PostModel>> GetAllAsync()
    {
        IEnumerable<Post> posts =  await _unitOfWork.PostRepository.GetAllAsync(null,null,"User");
        List<PostModel> postModels = new List<PostModel>();

        foreach (var item in posts)
            postModels.Add(_mapper.Map<PostModel>(item));

        return postModels;
    }

    /// <summary>
    /// Get all published as an asynchronous operation.
    /// </summary>
    /// <returns>A Task&lt;IEnumerable`1&gt; representing the asynchronous operation.</returns>
    public async Task<IEnumerable<PostModel>> GetAllPublishedAsync()
    {
        IEnumerable<Post> posts =  await _unitOfWork.PostRepository.GetAllAsync(p => p.PostStatus == PostStatus.Published,null,"User");
        List<PostModel> postModels = new List<PostModel>();

        foreach (var item in posts)
            postModels.Add(_mapper.Map<PostModel>(item));

        return postModels;
    }

    /// <summary>
    /// Get by category identifier as an asynchronous operation.
    /// </summary>
    /// <param name="categoryId">The category identifier.</param>
    /// <returns>A Task&lt;IEnumerable`1&gt; representing the asynchronous operation.</returns>
    public async Task<IEnumerable<PostModel>> GetByCategoryIdAsync(int categoryId)
    {
        IEnumerable<Post> posts =  await _unitOfWork.PostRepository.GetAllAsync(p => p.PostCategories.Any(pt => pt.CategoryId == categoryId ) && p.PostStatus == PostStatus.Published,null,"PostCategories,User");
        List<PostModel> postModels = new List<PostModel>();

        foreach (var item in posts)
            postModels.Add(_mapper.Map<PostModel>(item));

        return postModels;
    }

    /// <summary>
    /// Get by identifier as an asynchronous operation.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>A Task&lt;PostModel&gt; representing the asynchronous operation.</returns>
    public async Task<PostModel> GetByIdAsync(int id)
    {
        return _mapper.Map<PostModel>(await _unitOfWork.PostRepository.GetByIdAsync(id,"User"));
    }

    /// <summary>
    /// Add as an asynchronous operation.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task AddAsync(PostModel model)
    {
        model.CreatedAt = DateTime.Now;
        model.UpdatedAt = DateTime.Now;
        
        await _unitOfWork.PostRepository.AddAsync(_mapper.Map<Post>(model));
        await _unitOfWork.SaveAsync();
    }


    /// <summary>
    /// Update as an asynchronous operation.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task UpdateAsync(PostModel model)
    {
        model.UpdatedAt = DateTime.Now;
        
        _unitOfWork.PostRepository.Update(_mapper.Map<Post>(model));
        await _unitOfWork.SaveAsync();

    }

    /// <summary>
    /// Delete as an asynchronous operation.
    /// </summary>
    /// <param name="modelId">The model identifier.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task DeleteAsync(int modelId)
    {
        await _unitOfWork.PostRepository.Delete(modelId);
        await _unitOfWork.SaveAsync();

    }

    /// <summary>
    /// Gets the user posts asynchronous.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <returns>IEnumerable&lt;PostModel&gt;.</returns>
    public IEnumerable<PostModel> GetUserPostsAsync(int userId)
    {
        IEnumerable<Post> posts =  _unitOfWork.PostRepository.GetByValueAsync(post => post.UserId == userId,"User");

        var postModels = new List<PostModel>();

        foreach (var post in posts)
        {
            postModels.Add(_mapper.Map<PostModel>(post));
        }

        return postModels;
    }

    /// <summary>
    /// Gets the posts search.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>IEnumerable&lt;PostModel&gt;.</returns>
    public async Task<IEnumerable<PostModel>> GetPostsSearch(string text)
    {
        IEnumerable<Post> posts =  await _unitOfWork.PostRepository.GetAllAsync( p => 
            (p.Title.Contains(text) || p.Content.Contains(text)) && p.PostStatus == PostStatus.Published,null,"User");
        
        List<PostModel> postModels = new List<PostModel>();

        foreach (var item in posts)
            postModels.Add(_mapper.Map<PostModel>(item));
        
        return postModels;
    }
}
