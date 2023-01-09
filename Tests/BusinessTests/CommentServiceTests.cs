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
    public void  CommentService_GetAll_ReturnsAllComments()
    {
        //arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        mockUnitOfWork
            .Setup(x => x.CommentRepository.GetAll(null,""))
            .Returns(GetTestComments.AsEnumerable());
        
        var commentService = new CommentService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

        //act
        var actual =  commentService.GetAll();

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
    public void  PostService_GetPostComments_ReturnsPostComments(int postId)
    {
        //arrange
        var expected = GetTestCommentModels.Where(x => x.PostId == postId);
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        mockUnitOfWork
            .Setup(x => x.CommentRepository.GetAll(It.IsAny<Expression<Func<Comment,bool>>>(),""))
            .Returns(GetTestComments.Where(x => x.PostId == postId));
        
        var commentService = new CommentService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

        //act
        var actual =  commentService.GetPostComments(postId);

        //assert
        actual.Should().BeEquivalentTo(expected);
    }


    /// <summary>
    /// Defines the test method CommentService_GetById_ReturnsCommentModel.
    /// </summary>
    [Test]
    public void  CommentService_GetById_ReturnsCommentModel()
    {
        //arrange
        var expected = GetTestCommentModels.First();
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        mockUnitOfWork
            .Setup(m => m.CommentRepository.GetById(It.IsAny<int>(),""))
            .Returns(GetTestComments.First());

        var commentService = new CommentService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

        //act
        var actual =  commentService.GetById(1);

        //assert
        actual.Should().BeEquivalentTo(expected);
    }

    /// <summary>
    /// Defines the test method CommentService_Add_AddsModel.
    /// </summary>
    [Test]
    public void  CommentService_Add_AddsModel()
    {
        //arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(m => m.CommentRepository.Add(It.IsAny<Comment>()));

        var commentService = new CommentService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
        var comment = GetTestCommentModels.First();

        //act
         commentService.Add(comment);

        //assert
        mockUnitOfWork.Verify(x => x.CommentRepository.Add(It.Is<Comment>(t =>
            t.Id == comment.Id && t.Title == comment.Title && t.Content == comment.Content && t.PostId == comment.PostId)), Times.Once);
        mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
    }

    /// <summary>
    /// Defines the test method CommentService_Delete_DeletesComment.
    /// </summary>
    /// <param name="id">The identifier.</param>
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(100)]
    public void  CommentService_Delete_DeletesComment(int id)
    {
        //arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(m => m.CommentRepository.Delete(It.IsAny<int>()));
        var commentService = new CommentService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

        //act
         commentService.Delete(id);

        //assert
        mockUnitOfWork.Verify(x => x.CommentRepository.Delete(id), Times.Once());
        mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once());
    }

    /// <summary>
    /// Defines the test method CommentService_Update_UpdatesComment.
    /// </summary>
    [Test]
    public void  CommentService_Update_UpdatesComment()
    {
        //arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(m => m.CommentRepository.Update(It.IsAny<Comment>()));

        var commentService = new CommentService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
        var comment = GetTestCommentModels.First();

        //act
         commentService.Update(comment);

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

