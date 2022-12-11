// ***********************************************************************
// Assembly         : Tests
// Author           : Slava
// Created          : 12-11-2022
//
// Last Modified By : Slava
// Last Modified On : 12-11-2022
// ***********************************************************************
// <copyright file="CommentServiceTests.cs" company="Tests">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Linq.Expressions;
using BuisnessLogicLayer.Models;
using BuisnessLogicLayer.Services;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Tests.BusinessTests;

/// <summary>
/// Class CommentServiceTest.
/// </summary>
public class CommentServiceTest
{
    /// <summary>
    /// Defines the test method CommentService_GetAll_ReturnsAllComments.
    /// </summary>
    [Test]
    public async Task CommentService_GetAll_ReturnsAllComments()
    {
        //arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        mockUnitOfWork
            .Setup(x => x.CommentRepository.GetAllAsync(null,""))
            .ReturnsAsync(GetTestComments.AsEnumerable());
        
        var commentService = new CommentService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

        //act
        var actual = await commentService.GetAllAsync();

        //assert
        actual.Should().BeEquivalentTo(GetTestCommentModels);
    }

    /// <summary>
    /// Defines the test method PostService_GetPostComments_ReturnsPostComments.
    /// </summary>
    /// <param name="postId">The post identifier.</param>
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(100)]
    public async Task PostService_GetPostComments_ReturnsPostComments(int postId)
    {
        //arrange
        var expected = GetTestCommentModels.Where(x => x.PostId == postId);
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        mockUnitOfWork
            .Setup(x => x.CommentRepository.GetAllAsync(It.IsAny<Expression<Func<Comment,bool>>>(),""))
            .ReturnsAsync(GetTestComments.Where(x => x.PostId == postId));
        
        var commentService = new CommentService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

        //act
        var actual = await commentService.GetPostComments(postId);

        //assert
        actual.Should().BeEquivalentTo(expected);
    }


    /// <summary>
    /// Defines the test method CommentService_GetById_ReturnsCommentModel.
    /// </summary>
    [Test]
    public async Task CommentService_GetById_ReturnsCommentModel()
    {
        //arrange
        var expected = GetTestCommentModels.First();
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        mockUnitOfWork
            .Setup(m => m.CommentRepository.GetByIdAsync(It.IsAny<int>(),""))
            .ReturnsAsync(GetTestComments.First());

        var commentService = new CommentService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

        //act
        var actual = await commentService.GetByIdAsync(1);

        //assert
        actual.Should().BeEquivalentTo(expected);
    }

    /// <summary>
    /// Defines the test method CommentService_AddAsync_AddsModel.
    /// </summary>
    [Test]
    public async Task CommentService_AddAsync_AddsModel()
    {
        //arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(m => m.CommentRepository.AddAsync(It.IsAny<Comment>()));

        var commentService = new CommentService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
        var comment = GetTestCommentModels.First();

        //act
        await commentService.AddAsync(comment);

        //assert
        mockUnitOfWork.Verify(x => x.CommentRepository.AddAsync(It.Is<Comment>(t =>
            t.Id == comment.Id && t.Title == comment.Title && t.Content == comment.Content && t.PostId == comment.PostId)), Times.Once);
        mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
    }

    /// <summary>
    /// Defines the test method CommentService_DeleteAsync_DeletesComment.
    /// </summary>
    /// <param name="id">The identifier.</param>
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(100)]
    public async Task CommentService_DeleteAsync_DeletesComment(int id)
    {
        //arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(m => m.CommentRepository.Delete(It.IsAny<int>()));
        var commentService = new CommentService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

        //act
        await commentService.DeleteAsync(id);

        //assert
        mockUnitOfWork.Verify(x => x.CommentRepository.Delete(id), Times.Once());
        mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once());
    }

    /// <summary>
    /// Defines the test method CommentService_UpdateAsync_UpdatesComment.
    /// </summary>
    [Test]
    public async Task CommentService_UpdateAsync_UpdatesComment()
    {
        //arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(m => m.CommentRepository.Update(It.IsAny<Comment>()));

        var commentService = new CommentService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
        var comment = GetTestCommentModels.First();

        //act
        await commentService.UpdateAsync(comment);

        //assert
        mockUnitOfWork.Verify(x => x.CommentRepository.Update(It.Is<Comment>(t =>
            t.Id == comment.Id && t.Title == comment.Title && t.Content == comment.Content && t.PostId == comment.PostId )), Times.Once);
        mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
    }


    #region TestData

    /// <summary>
    /// Gets the get test comments.
    /// </summary>
    /// <value>The get test comments.</value>
    private List<Comment> GetTestComments =>
        new()
        {
            new Comment {Id = 1, Title = "Hello from Slava", Content = "I completely agree with author's opinion", PostId = 1},
            new Comment {Id = 2, Title = "Hello from Tom", Content = "I completely agree with author's opinion", PostId = 2}
        };

    /// <summary>
    /// Gets the get test comment models.
    /// </summary>
    /// <value>The get test comment models.</value>
    private List<CommentModel> GetTestCommentModels =>
        new()
        {
            new CommentModel {Id = 1, Title = "Hello from Slava", Content = "I completely agree with author's opinion", PostId = 1},
            new CommentModel {Id = 2, Title = "Hello from Tom", Content = "I completely agree with author's opinion", PostId = 2}
        };
    
    #endregion
}

