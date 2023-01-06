// ***********************************************************************
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
    /// Get all tags
    /// </summary>
    /// <returns></returns>
    public  IEnumerable<TagModel> GetAll()
    {
        IEnumerable<Tag> tags =   _unitOfWork.TagRepository.GetAll();
        List<TagModel> tagModels = new List<TagModel>();

        foreach (var tag in tags)
        {
            tagModels.Add(_mapper.Map<TagModel>(tag));
        }

        return tagModels;

    }

    /// <summary>
    /// Get tag by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="PersonalBlogException"></exception>
    public TagModel? GetById(int id)
    {
        var tag =  _unitOfWork.TagRepository.GetById(id);

        if (tag == null) throw new PersonalBlogException("Tag not found");
        
        return _mapper.Map<TagModel>(tag);
    }
    /// <summary>
    /// Add tag
    /// </summary>
    /// <param name="model"></param>

    public void Add(TagModel model)
    {
         _unitOfWork.TagRepository.Add(_mapper.Map<Tag>(model));
         _unitOfWork.Save();
    }

    /// <summary>
    /// Update tag
    /// </summary>
    /// <param name="model"></param>
    public void Update(TagModel model)
    {
        _unitOfWork.TagRepository.Update(_mapper.Map<Tag>(model));
         _unitOfWork.Save();

    }
    /// <summary>
    /// Delete tag
    /// </summary>
    /// <param name="modelId"></param>
    public void Delete(int modelId)
    {
        _unitOfWork.TagRepository.Delete(modelId);
        _unitOfWork.Save();

    }

    /// <summary>
    /// Add tag
    /// </summary>
    /// <param name="postId"></param>
    /// <param name="tagModel"></param>
    /// <exception cref="PersonalBlogException"></exception>
    public void AddTag(int postId, TagModel tagModel)
    {
        Post? post =  _unitOfWork.PostRepository.GetById(postId, "PostTags");
        
        if ( post == null)
        {
            throw new PersonalBlogException("Post not found");
        }
        
        Tag? tag =  _unitOfWork.TagRepository.GetAll(tag => tag.Title == tagModel.Title).FirstOrDefault();

         if (tag == null)
         {
             tag = _mapper.Map<Tag>(tagModel);
              _unitOfWork.TagRepository.Add(tag);

         }
        
        post.PostTags.Add(new PostTag()
        {
            Tag = tag,
            Post = post
        });
            
         _unitOfWork.Save();

    }

    /// <summary>
    /// Get post's tags
    /// </summary>
    /// <param name="postId"></param>
    /// <returns></returns>
    /// <exception cref="PersonalBlogException"></exception>
    public IEnumerable<TagModel> GetTags(int postId)
    {
        Post? post =  _unitOfWork.PostRepository.GetById(postId, "PostTags");

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