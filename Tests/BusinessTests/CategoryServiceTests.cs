// ***********************************************************************
// Assembly         : Tests
// Author           : Slava
// Created          : 12-11-2022
//
// Last Modified By : Slava
// Last Modified On : 12-11-2022
// ***********************************************************************
// <copyright file="CategoryServiceTests.cs" company="Tests">
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
/// Class CategoryServiceTest.
/// </summary>
public class CategoryServiceTest
{
    /// <summary>
    /// Defines the test method CategoryService_GetAll_ReturnsAllCategories.
    /// </summary>
    [Test]
    public void CategoryService_GetAll_ReturnsAllCategories()
    {
        //arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        mockUnitOfWork
            .Setup(x => x.CategoryRepository.GetAll(null,""))
            .Returns(GetTestCategories.AsEnumerable());
        
        var categoryService = new CategoryService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

        //act
        var actual =  categoryService.GetAll();

        //assert
        actual.Should().BeEquivalentTo(GetTestCategoryModels);
    }


    /// <summary>
    /// Defines the test method CategoryService_GetById_ReturnsCategoryModel.
    /// </summary>
    [Test]
    public void CategoryService_GetById_ReturnsCategoryModel()
    {
        //arrange
        var expected = GetTestCategoryModels.First();
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        mockUnitOfWork
            .Setup(m => m.CategoryRepository.GetById(It.IsAny<int>(),""))
            .Returns(GetTestCategories.First());

        var categoryService = new CategoryService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

        //act
        var actual =  categoryService.GetById(1);

        //assert
        actual.Should().BeEquivalentTo(expected);
    }

    /// <summary>
    /// Defines the test method CategoryService_Add_AddsModel.
    /// </summary>
    [Test]
    public void CategoryService_Add_AddsModel()
    {
        //arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(m => m.CategoryRepository.Add(It.IsAny<Category>()));

        var categoryService = new CategoryService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
        var category = GetTestCategoryModels.First();

        //act
         categoryService.Add(category);

        //assert
        mockUnitOfWork.Verify(x => x.CategoryRepository.Add(It.Is<Category>(t =>
            t.Id == category.Id && t.Title == category.Title)), Times.Once);
        mockUnitOfWork.Verify(x => x.Save(), Times.Once);
    }

    /// <summary>
    /// Defines the test method CategoryService_Delete_DeletesCategory.
    /// </summary>
    /// <param name="id">The identifier.</param>
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(100)]
    public void CategoryService_Delete_DeletesCategory(int id)
    {
        //arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(m => m.CategoryRepository.Delete(It.IsAny<int>()));
        var categoryService = new CategoryService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

        //act
        categoryService.Delete(id);

        //assert
        mockUnitOfWork.Verify(x => x.CategoryRepository.Delete(id), Times.Once());
        mockUnitOfWork.Verify(x => x.Save(), Times.Once());
    }

    /// <summary>
    /// Defines the test method CategoryService_Update_UpdatesCategory.
    /// </summary>
    [Test]
    public void CategoryService_UpdateAsync_UpdatesCategory()
    {
        //arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(m => m.CategoryRepository.Update(It.IsAny<Category>()));

        var categoryService = new CategoryService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
        var category = GetTestCategoryModels.First();

        //act
        categoryService.Update(category);

        //assert
        mockUnitOfWork.Verify(x => x.CategoryRepository.Update(It.Is<Category>(t =>
            t.Id == category.Id && t.Title == category.Title )), Times.Once);
        mockUnitOfWork.Verify(x => x.Save(), Times.Once);
    }


    #region TestData

    /// <summary>
    /// Gets the get test categories.
    /// </summary>
    /// <value>The get test categories.</value>
    private List<Category> GetTestCategories =>
        new()
        {
            new Category { Id = 1, Title = "Programing" },
            new Category { Id = 2, Title = "Music" }
        };

    /// <summary>
    /// Gets the get test category models.
    /// </summary>
    /// <value>The get test category models.</value>
    private List<CategoryModel> GetTestCategoryModels =>
        new()
        {
            new CategoryModel { Id = 1, Title = "Programing" },
            new CategoryModel { Id = 2, Title = "Music" }
        };
    
    #endregion
}

