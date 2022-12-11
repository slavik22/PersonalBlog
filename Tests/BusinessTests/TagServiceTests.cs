// ***********************************************************************
// Assembly         : Tests
// Author           : Slava
// Created          : 12-11-2022
//
// Last Modified By : Slava
// Last Modified On : 12-11-2022
// ***********************************************************************
// <copyright file="TagServiceTests.cs" company="Tests">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using BuisnessLogicLayer.Models;
using BuisnessLogicLayer.Services;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Tests.BusinessTests;

/// <summary>
/// Class TagServiceTest.
/// </summary>
public class TagServiceTest
{
    /// <summary>
    /// Defines the test method TagService_GetAll_ReturnsAllCustomers.
    /// </summary>
    [Test]
    public async Task TagService_GetAll_ReturnsAllCustomers()
    {
        //arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        mockUnitOfWork
            .Setup(x => x.TagRepository.GetAllAsync(null,""))
            .ReturnsAsync(GetTestTags.AsEnumerable());
        
        var tagService = new TagService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

        //act
        var actual = await tagService.GetAllAsync();

        //assert
        actual.Should().BeEquivalentTo(GetTestTagModels);
    }


    /// <summary>
    /// Defines the test method TagService_GetById_ReturnsCustomerModel.
    /// </summary>
    [Test]
    public async Task TagService_GetById_ReturnsCustomerModel()
    {
        //arrange
        var expected = GetTestTagModels.First();
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        mockUnitOfWork
            .Setup(m => m.TagRepository.GetByIdAsync(It.IsAny<int>(),""))
            .ReturnsAsync(GetTestTags.First());

        var tagService = new TagService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

        //act
        var actual = await tagService.GetByIdAsync(1);

        //assert
        actual.Should().BeEquivalentTo(expected);
    }

    /// <summary>
    /// Defines the test method TagService_AddAsync_AddsModel.
    /// </summary>
    [Test]
    public async Task TagService_AddAsync_AddsModel()
    {
        //arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(m => m.TagRepository.AddAsync(It.IsAny<Tag>()));

        var tagService = new TagService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
        var tag = GetTestTagModels.First();

        //act
        await tagService.AddAsync(tag);

        //assert
        mockUnitOfWork.Verify(x => x.TagRepository.AddAsync(It.Is<Tag>(t =>
            t.Id == tag.Id && t.Title == tag.Title)), Times.Once);
        mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
    }

    /// <summary>
    /// Defines the test method TagService_DeleteAsync_DeletesTag.
    /// </summary>
    /// <param name="id">The identifier.</param>
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(100)]
    public async Task TagService_DeleteAsync_DeletesTag(int id)
    {
        //arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(m => m.TagRepository.Delete(It.IsAny<int>()));
        var tagService = new TagService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

        //act
        await tagService.DeleteAsync(id);

        //assert
        mockUnitOfWork.Verify(x => x.TagRepository.Delete(id), Times.Once());
        mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once());
    }

    /// <summary>
    /// Defines the test method TagService_UpdateAsync_UpdatesTag.
    /// </summary>
    [Test]
    public async Task TagService_UpdateAsync_UpdatesTag()
    {
        //arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(m => m.TagRepository.Update(It.IsAny<Tag>()));

        var tagService = new TagService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
        var tag = GetTestTagModels.First();

        //act
        await tagService.UpdateAsync(tag);

        //assert
        mockUnitOfWork.Verify(x => x.TagRepository.Update(It.Is<Tag>(t =>
            t.Id == tag.Id && t.Title == tag.Title )), Times.Once);
        mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
    }


    #region TestData

    /// <summary>
    /// Gets the get test tags.
    /// </summary>
    /// <value>The get test tags.</value>
    private List<Tag> GetTestTags =>
        new()
        {
            new Tag { Id = 1, Title = ".net" },
            new Tag { Id = 2, Title = "java" },
            new Tag { Id = 3, Title = "pop" }
        };

    /// <summary>
    /// Gets the get test tag models.
    /// </summary>
    /// <value>The get test tag models.</value>
    private List<TagModel> GetTestTagModels =>
        new()
        {
            new TagModel { Id = 1, Title = ".net" },
            new TagModel { Id = 2, Title = "java" },
            new TagModel { Id = 3, Title = "pop" }
        };
    
    #endregion
}

