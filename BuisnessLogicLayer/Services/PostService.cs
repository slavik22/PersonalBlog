// ***********************************************************************
// Assembly         : BuisnessLogicLayer
// Author           : Slava
// Created          : 12-01-2022
//
// Last Modified By : Slava
// Last Modified On : 12-11-2022
// ***********************************************************************
// <copyright file="PostService.cs" company="BuisnessLogicLayer">
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
    /// Initializes a new instance of the <see cref="PostService" /> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work.</param>
    /// <param name="mapper">The mapper.</param>
    public PostService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    /// <summary>
    /// Get all postmodels
    /// </summary>
    /// <returns></returns>
    public  IEnumerable<PostModel> GetAll()
    {
        IEnumerable<Post> posts =   _unitOfWork.PostRepository.GetAll(null,"User");
        List<PostModel> postModels = new List<PostModel>();

        foreach (var item in posts)
            postModels.Add(_mapper.Map<PostModel>(item));

        return postModels;
    }

    /// <summary>
    /// Get all published posts
    /// </summary>
    /// <returns></returns>
    public  IEnumerable<PostModel> GetAllPublished()
    {
        IEnumerable<Post> posts =   _unitOfWork.PostRepository.GetAll(p =>  p.PostStatus == PostStatus.Published,"User");
        List<PostModel> postModels = new List<PostModel>();

        foreach (var item in posts)
            postModels.Add(_mapper.Map<PostModel>(item));

        return postModels;
    }

    /// <summary>
    /// Get category by id
    /// </summary>
    /// <param name="categoryId"></param>
    /// <returns></returns>
    public IEnumerable<PostModel> GetByCategoryId(int categoryId)
    {
        IEnumerable<Post> posts =   _unitOfWork.PostRepository.GetAll(p => p.PostCategories.Any(pt => pt.CategoryId == categoryId ) && p.PostStatus == PostStatus.Published,"PostCategories,User");
        List<PostModel> postModels = new List<PostModel>();

        foreach (var item in posts)
            postModels.Add(_mapper.Map<PostModel>(item));

        return postModels;
    }

    /// <summary>
    /// Get post by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="PersonalBlogException"></exception>
    public  PostModel? GetById(int id)
    {
        var post =  _unitOfWork.PostRepository.GetById(id, "User");

        if (post == null) throw new PersonalBlogException("Post not Fount");
        
        return _mapper.Map<PostModel>(post);
    }

    /// <summary>
    /// Add post
    /// </summary>
    /// <param name="model"></param>
    public void Add(PostModel model)
    {
        model.CreatedAt = DateTime.Now;
        model.UpdatedAt = DateTime.Now;
        
         _unitOfWork.PostRepository.Add(_mapper.Map<Post>(model));
         _unitOfWork.SaveAsync();
    }

    /// <summary>
    /// Update post
    /// </summary>
    /// <param name="model"></param>
    public void Update(PostModel model)
    {
        model.UpdatedAt = DateTime.Now;
        
        _unitOfWork.PostRepository.Update(_mapper.Map<Post>(model));
         _unitOfWork.SaveAsync();

    }
    /// <summary>
    /// Delete post
    /// </summary>
    /// <param name="modelId"></param>
    public void Delete(int modelId)
    {
         _unitOfWork.PostRepository.Delete(modelId);
         _unitOfWork.SaveAsync();

    }
    /// <summary>
    /// Get user's posts
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public  IEnumerable<PostModel> GetUserPosts(int userId)
    {
        IEnumerable<Post> posts =  _unitOfWork.PostRepository.GetAll(post => post.UserId == userId, "User");

        var postModels = new List<PostModel>();

        foreach (var post in posts)
        {
            postModels.Add(_mapper.Map<PostModel>(post));
        }

        return postModels;
    }
    /// <summary>
    /// Get posts by search
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public  IEnumerable<PostModel> GetPostsSearch(string text)
    {
        IEnumerable<Post> posts =   _unitOfWork.PostRepository.GetAll( p => 
            (p.Title.Contains(text) || p.Content.Contains(text)) && p.PostStatus == PostStatus.Published,"User");
        
        List<PostModel> postModels = new List<PostModel>();

        foreach (var item in posts)
            postModels.Add(_mapper.Map<PostModel>(item));
        
        return postModels;
    }
}
