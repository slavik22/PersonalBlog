// ***********************************************************************
// Assembly         : Tests
// Author           : Slava
// Created          : 12-11-2022
//
// Last Modified By : Slava
// Last Modified On : 12-11-2022
// ***********************************************************************
// <copyright file="PostServiceTests.cs" company="Tests">
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
using PostStatus = BuisnessLogicLayer.Models.Enums.PostStatus;

namespace Tests.BusinessTests;

/// <summary>
/// Class PostServiceTest.
/// </summary>
public class PostServiceTest
{
    /// <summary>
    /// Defines the test method PostService_GetAll_ReturnsAllPosts.
    /// </summary>
    [Test]
    public async Task PostService_GetAll_ReturnsAllPosts()
    {
        //arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        mockUnitOfWork
            .Setup(x => x.PostRepository.GetAllAsync(null, It.IsAny<string>()))
            .ReturnsAsync(GetTestPosts.AsEnumerable());
        
        var postService = new PostService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

        //act
        var actual = await postService.GetAllAsync();

        //assert
        actual.Should().BeEquivalentTo(GetTestPostModels);
    }

    /// <summary>
    /// Defines the test method PostService_GetAllPublished_ReturnsAllPublishedPosts.
    /// </summary>
    [Test]
    public async Task PostService_GetAllPublished_ReturnsAllPublishedPosts()
    {
        //arrange
        var expected = GetTestPosts.Where(p => p.PostStatus == DataAccessLayer.Enums.PostStatus.Published);
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        mockUnitOfWork
            .Setup(x => x.PostRepository.GetAllAsync(null, It.IsAny<string>()))
            .ReturnsAsync(GetTestPosts.Where(p =>  p.PostStatus == DataAccessLayer.Enums.PostStatus.Published));
        
        var postService = new PostService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

        //act
        var actual = await postService.GetAllAsync();

        //assert
        actual.Should().BeEquivalentTo(expected);
    }


    /// <summary>
    /// Post service get user posts asynchronous returns user posts as an asynchronous operation.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    [TestCase(1)]
    [TestCase(2)]
    public async Task PostService_GetUserPostsAsync_ReturnsUserPostsAsync(int userId)
    {
        //arrange
        var expected = GetTestPostModels.Where(p => p.UserId == userId);
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        mockUnitOfWork
            .Setup(x => x.PostRepository.GetAllAsync( It.IsAny<Expression<Func<Post,bool>>>(), It.IsAny<string>()))
            .ReturnsAsync(GetTestPosts.Where(p =>  p.UserId == userId));
        
        var postService = new PostService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

        //act
        var actual = await postService.GetUserPostsAsync(userId);

        //assert
        actual.Should().BeEquivalentTo(expected);
    }

    /// <summary>
    /// Post service get posts search returns posts search as an asynchronous operation.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    [TestCase("pop")]
    [TestCase("java")]
    [TestCase("music")]
    [TestCase("world")]
    [TestCase("hello")]
    public async Task PostService_GetPostsSearch_ReturnsPostsSearchAsync(string text)
    {
        //arrange
        var expected = GetTestPostModels.Where(p => 
            (p.Title.Contains(text) || p.Content.Contains(text)) && p.PostStatus == PostStatus.Published);
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        mockUnitOfWork
            .Setup(x => x.PostRepository.GetAllAsync( It.IsAny<Expression<Func<Post,bool>>>(), It.IsAny<string>()))
            .ReturnsAsync(GetTestPosts.Where(p => 
                (p.Title.Contains(text) || p.Content.Contains(text)) && p.PostStatus == DataAccessLayer.Enums.PostStatus.Published));
        
        var postService = new PostService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

        //act
        var actual = await postService.GetPostsSearch(text);

        //assert
        actual.Should().BeEquivalentTo(expected);
    }



    /// <summary>
    /// Defines the test method PostService_GetById_ReturnsPostModel.
    /// </summary>
    [Test]
    public async Task PostService_GetById_ReturnsPostModel()
    {
        //arrange
        var expected = GetTestPostModels.First();
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        mockUnitOfWork
            .Setup(m => m.PostRepository.GetByIdAsync(It.IsAny<int>(),It.IsAny<string>()))
            .ReturnsAsync(GetTestPosts.First());

        var postService = new PostService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

        //act
        var actual = await postService.GetByIdAsync(1);

        //assert
        actual.Should().BeEquivalentTo(expected);
    }



    /// <summary>
    /// Defines the test method PostService_AddAsync_AddsModel.
    /// </summary>
    [Test]
    public async Task PostService_AddAsync_AddsModel()
    {
        //arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(m => m.PostRepository.AddAsync(It.IsAny<Post>()));

        var postService = new PostService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
        var post = GetTestPostModels.First();

        //act
        await postService.AddAsync(post);

        //assert
        mockUnitOfWork.Verify(x => x.PostRepository.AddAsync(It.Is<Post>(t =>
            t.Id == post.Id && t.Title == post.Title && t.Summary == post.Summary && t.Content == post.Content && t.UserId == post.UserId)), Times.Once);
        mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
    }

    /// <summary>
    /// Defines the test method PostService_DeleteAsync_DeletesPost.
    /// </summary>
    /// <param name="id">The identifier.</param>
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(100)]
    public async Task PostService_DeleteAsync_DeletesPost(int id)
    {
        //arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(m => m.PostRepository.Delete(It.IsAny<int>()));
        var postService = new PostService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

        //act
        await postService.DeleteAsync(id);

        //assert
        mockUnitOfWork.Verify(x => x.PostRepository.Delete(id), Times.Once());
        mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once());
    }

    /// <summary>
    /// Defines the test method PostService_UpdateAsync_UpdatesPost.
    /// </summary>
    [Test]
    public async Task PostService_UpdateAsync_UpdatesPost()
    {
        //arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(m => m.PostRepository.Update(It.IsAny<Post>()));

        var postService = new PostService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
        var post = GetTestPostModels.First();

        //act
        await postService.UpdateAsync(post);

        //assert
        mockUnitOfWork.Verify(x => x.PostRepository.Update(It.Is<Post>(t =>
            t.Id == post.Id && t.Title == post.Title && t.Summary == post.Summary && t.Content == post.Content && t.UserId == post.UserId)), Times.Once);
        mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
    }


    #region TestData

    /// <summary>
    /// Gets the get test users.
    /// </summary>
    /// <value>The get test users.</value>
    private List<User> GetTestUsers =>
        new()
        {
            new User
            {
                Id = 1, Email = "email1@gmaill.com", Mobile = "+380502411234", Name = "Slava", Surname = "Fedyna",
                Password = "password", BirthDate = new DateTime(1990, 12, 12)
            },
            new User
            {
                Id = 2, Email = "email2@gmaill.com", Mobile = "+90502411234", Name = "Tom", Surname = "Shelby",
                Password = "password", BirthDate = new DateTime(1991, 10, 12)
            }
        };



    /// <summary>
    /// Gets the get test posts.
    /// </summary>
    /// <value>The get test posts.</value>
    private List<Post> GetTestPosts =>
        new()
        {
            new Post { Id = 1, Title = "Hello world", Summary = ".net", Content = ".net", UserId = 1, User = GetTestUsers[0]},
            new Post { Id = 2, Title = "Hello world", Summary = "java", Content = "java", UserId = 2, User = GetTestUsers[1] },
            new Post { Id = 3, Title = "Hello world", Summary = "pop", Content = "pop music", UserId = 2, User = GetTestUsers[1] }
        };

    /// <summary>
    /// Gets the get test post models.
    /// </summary>
    /// <value>The get test post models.</value>
    private List<PostModel> GetTestPostModels =>
        new()
        {
            new PostModel { Id = 1, Title = "Hello world", Summary = ".net", Content = ".net", UserId = 1, AuthorName = "Slava Fedyna"},
            new PostModel { Id = 2, Title = "Hello world", Summary = "java", Content = "java", UserId = 2, AuthorName = "Tom Shelby" },
            new PostModel { Id = 3, Title = "Hello world", Summary = "pop", Content = "pop music", UserId = 2, AuthorName = "Tom Shelby" }
        };
    
    #endregion
}

