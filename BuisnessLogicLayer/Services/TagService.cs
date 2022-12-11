﻿// ***********************************************************************
// Assembly         : BuisnessLogicLayer
// Author           : Slava
// Created          : 12-01-2022
//
// Last Modified By : Slava
// Last Modified On : 12-11-2022
// ***********************************************************************
// <copyright file="TagService.cs" company="BuisnessLogicLayer">
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
/// Class TagService.
/// Implements the <see cref="ITagService" />
/// </summary>
/// <seealso cref="ITagService" />
public class TagService : ITagService
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
    /// Initializes a new instance of the <see cref="TagService" /> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work.</param>
    /// <param name="mapper">The mapper.</param>
    public TagService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    /// <summary>
    /// Get all as an asynchronous operation.
    /// </summary>
    /// <returns>A Task&lt;IEnumerable`1&gt; representing the asynchronous operation.</returns>
    public async Task<IEnumerable<TagModel>> GetAllAsync()
    {
        IEnumerable<Tag> tags =  await _unitOfWork.TagRepository.GetAllAsync();
        List<TagModel> tagModels = new List<TagModel>();

        foreach (var tag in tags)
        {
            tagModels.Add(_mapper.Map<TagModel>(tag));
        }

        return tagModels;

    }

    /// <summary>
    /// Get by identifier as an asynchronous operation.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>A Task&lt;TagModel&gt; representing the asynchronous operation.</returns>
    /// <exception cref="BuisnessLogicLayer.Validation.PersonalBlogException">Tag not found</exception>
    public async Task<TagModel?> GetByIdAsync(int id)
    {
        var tag = await _unitOfWork.TagRepository.GetByIdAsync(id);

        if (tag == null) throw new PersonalBlogException("Tag not found");
        
        return _mapper.Map<TagModel>(tag);
    }

    /// <summary>
    /// Add as an asynchronous operation.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task AddAsync(TagModel model)
    {
        await _unitOfWork.TagRepository.AddAsync(_mapper.Map<Tag>(model));
        await _unitOfWork.SaveAsync();
    }

    /// <summary>
    /// Update as an asynchronous operation.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task UpdateAsync(TagModel model)
    {
        _unitOfWork.TagRepository.Update(_mapper.Map<Tag>(model));
        await _unitOfWork.SaveAsync();

    }

    /// <summary>
    /// Delete as an asynchronous operation.
    /// </summary>
    /// <param name="modelId">The model identifier.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task DeleteAsync(int modelId)
    {
       await _unitOfWork.TagRepository.Delete(modelId);
       await _unitOfWork.SaveAsync();

    }

    /// <summary>
    /// Add tag as an asynchronous operation.
    /// </summary>
    /// <param name="postId">The post identifier.</param>
    /// <param name="tagModel">The tag model.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    /// <exception cref="BuisnessLogicLayer.Validation.PersonalBlogException">Post not found</exception>
    /// <exception cref="System.Exception">Post not found</exception>
    public async Task AddTagAsync(int postId, TagModel tagModel)
    {
        Post? post = await _unitOfWork.PostRepository.GetByIdAsync(postId, "PostTags");
        
        if ( post == null)
        {
            throw new PersonalBlogException("Post not found");
        }
        
        Tag? tag = (await _unitOfWork.TagRepository.GetAllAsync(tag => tag.Title == tagModel.Title)).FirstOrDefault();

         if (tag == null)
         {
             tag = _mapper.Map<Tag>(tagModel);
             await _unitOfWork.TagRepository.AddAsync(tag);

         }
        
        post.PostTags.Add(new PostTag()
        {
            Tag = tag,
            Post = post
        });
            
        await _unitOfWork.SaveAsync();

    }

    /// <summary>
    /// Get tags as an asynchronous operation.
    /// </summary>
    /// <param name="postId">The post identifier.</param>
    /// <returns>A Task&lt;IEnumerable`1&gt; representing the asynchronous operation.</returns>
    /// <exception cref="BuisnessLogicLayer.Validation.PersonalBlogException">Post not found</exception>
    /// <exception cref="System.Exception">Post not found</exception>
    public async Task<IEnumerable<TagModel>> GetTagsAsync(int postId)
    {
        Post? post = await _unitOfWork.PostRepository.GetByIdAsync(postId, "PostTags");

        if ( post == null)
        {
            throw new PersonalBlogException ("Post not found");
        }
        
        List<TagModel> tagModels = new List<TagModel>();

        foreach (var item in post.PostTags.Select(pt => pt.Tag))
            tagModels.Add(_mapper.Map<TagModel>(item));

        return tagModels;

    }
}