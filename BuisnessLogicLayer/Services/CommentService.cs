// ***********************************************************************
// Assembly         : BuisnessLogicLayer
// Author           : Slava
// Created          : 12-01-2022
//
// Last Modified By : Slava
// Last Modified On : 12-11-2022
// ***********************************************************************
// <copyright file="CommentService.cs" company="BuisnessLogicLayer">
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
/// Class CommentService.
/// Implements the <see cref="ICommentService" />
/// </summary>
/// <seealso cref="ICommentService" />
public class CommentService : ICommentService
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
    /// Initializes a new instance of the <see cref="CommentService" /> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work.</param>
    /// <param name="mapper">The mapper.</param>
    public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    /// <summary>
    /// Get post's comments
    /// </summary>
    /// <param name="postId"></param>
    /// <returns></returns>
    public  IEnumerable<CommentModel> GetPostComments(int postId)
    {
        IEnumerable<Comment> comments =   _unitOfWork.CommentRepository.GetAll(c => c.PostId == postId);

        var commentModels = new List<CommentModel>();

        foreach (var comment in comments)
        {
            commentModels.Add(_mapper.Map<CommentModel>(comment));
        }

        return commentModels;

    }
    /// <summary>
    /// Get all comments
    /// </summary>
    /// <returns></returns>
    public IEnumerable<CommentModel> GetAll()
    {
        IEnumerable<Comment> comments =   _unitOfWork.CommentRepository.GetAll();
        List<CommentModel> commentModels = new List<CommentModel>();

        foreach (var item in comments)
            commentModels.Add(_mapper.Map<CommentModel>(item));

        return commentModels;

    }
    /// <summary>
    /// Get commentmodel by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="PersonalBlogException"></exception>
    public  CommentModel? GetById(int id)
    {
        var comment =  _unitOfWork.CommentRepository.GetById(id);

        if (comment == null) throw new PersonalBlogException("Comment not found");
        
        return _mapper.Map<CommentModel>(comment);
    }

    /// <summary>
    /// Add comment
    /// </summary>
    /// <param name="model"></param>
    public void Add(CommentModel model)
    {
         _unitOfWork.CommentRepository.Add(_mapper.Map<Comment>(model));
         _unitOfWork.SaveAsync();
    }
    /// <summary>
    /// Update comment
    /// </summary>
    /// <param name="model"></param>
    public void Update(CommentModel model)
    {
        _unitOfWork.CommentRepository.Update(_mapper.Map<Comment>(model)); 
        _unitOfWork.SaveAsync();

    }
    /// <summary>
    /// Delete comment
    /// </summary>
    /// <param name="modelId"></param>
    public void Delete(int modelId)
    {
         _unitOfWork.CommentRepository.Delete(modelId);
         _unitOfWork.SaveAsync();

    }
}