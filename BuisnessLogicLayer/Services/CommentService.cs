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
    /// Gets the post comments.
    /// </summary>
    /// <param name="postId">The post identifier.</param>
    /// <returns>IEnumerable&lt;CommentModel&gt;.</returns>
    public async Task<IEnumerable<CommentModel>> GetPostComments(int postId)
    {
        IEnumerable<Comment> comments =  await _unitOfWork.CommentRepository.GetAllAsync(c => c.PostId == postId);

        var commentModels = new List<CommentModel>();

        foreach (var comment in comments)
        {
            commentModels.Add(_mapper.Map<CommentModel>(comment));
        }

        return commentModels;

    }

    /// <summary>
    /// Get all as an asynchronous operation.
    /// </summary>
    /// <returns>A Task&lt;IEnumerable`1&gt; representing the asynchronous operation.</returns>
    public async Task<IEnumerable<CommentModel>> GetAllAsync()
    {
        IEnumerable<Comment> comments =  await _unitOfWork.CommentRepository.GetAllAsync();
        List<CommentModel> commentModels = new List<CommentModel>();

        foreach (var item in comments)
            commentModels.Add(_mapper.Map<CommentModel>(item));

        return commentModels;

    }

    /// <summary>
    /// Get by identifier as an asynchronous operation.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>A Task&lt;CommentModel&gt; representing the asynchronous operation.</returns>
    /// <exception cref="BuisnessLogicLayer.Validation.PersonalBlogException">Comment not found</exception>
    public async Task<CommentModel?> GetByIdAsync(int id)
    {
        var comment = await _unitOfWork.CommentRepository.GetByIdAsync(id);

        if (comment == null) throw new PersonalBlogException("Comment not found");
        
        return _mapper.Map<CommentModel>(comment);
    }

    /// <summary>
    /// Add as an asynchronous operation.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task AddAsync(CommentModel model)
    {
        await _unitOfWork.CommentRepository.AddAsync(_mapper.Map<Comment>(model));
        await _unitOfWork.SaveAsync();
    }

    /// <summary>
    /// Update as an asynchronous operation.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task UpdateAsync(CommentModel model)
    {
        _unitOfWork.CommentRepository.Update(_mapper.Map<Comment>(model));
        await _unitOfWork.SaveAsync();

    }

    /// <summary>
    /// Delete as an asynchronous operation.
    /// </summary>
    /// <param name="modelId">The model identifier.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task DeleteAsync(int modelId)
    {
        await _unitOfWork.CommentRepository.Delete(modelId);
        await _unitOfWork.SaveAsync();

    }
}