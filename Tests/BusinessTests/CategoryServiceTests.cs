using BuisnessLogicLayer.Models;
using BuisnessLogicLayer.Services;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Tests.BusinessTests;

public class CategoryServiceTest
{
    [Test]
    public async Task CategoryService_GetAll_ReturnsAllCategories()
    {
        //arrange
        var expected = GetTestCategoryModels;
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        mockUnitOfWork
            .Setup(x => x.CategoryRepository.GetAllAsync(null,null,""))
            .ReturnsAsync(GetTestCategories.AsEnumerable());
        
        var categoryService = new CategoryService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

        //act
        var actual = await categoryService.GetAllAsync();

        //assert
        actual.Should().BeEquivalentTo(GetTestCategoryModels);
    }

    
    [Test]
    public async Task CategoryService_GetById_ReturnsCategoryModel()
    {
        //arrange
        var expected = GetTestCategoryModels.First();
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        mockUnitOfWork
            .Setup(m => m.CategoryRepository.GetByIdAsync(It.IsAny<int>(),""))
            .ReturnsAsync(GetTestCategories.First());

        var categoryService = new CategoryService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

        //act
        var actual = await categoryService.GetByIdAsync(1);

        //assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Test]
    public async Task CategoryService_AddAsync_AddsModel()
    {
        //arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(m => m.CategoryRepository.AddAsync(It.IsAny<Category>()));

        var categoryService = new CategoryService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
        var category = GetTestCategoryModels.First();

        //act
        await categoryService.AddAsync(category);

        //assert
        mockUnitOfWork.Verify(x => x.CategoryRepository.AddAsync(It.Is<Category>(t =>
            t.Id == category.Id && t.Title == category.Title)), Times.Once);
        mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
    }
    
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(100)]
    public async Task CategoryService_DeleteAsync_DeletesCategory(int id)
    {
        //arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(m => m.CategoryRepository.Delete(It.IsAny<int>()));
        var categoryService = new CategoryService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

        //act
        await categoryService.DeleteAsync(id);

        //assert
        mockUnitOfWork.Verify(x => x.CategoryRepository.Delete(id), Times.Once());
        mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once());
    }

    [Test]
    public async Task CategoryService_UpdateAsync_UpdatesCategory()
    {
        //arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(m => m.CategoryRepository.Update(It.IsAny<Category>()));

        var categoryService = new CategoryService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
        var category = GetTestCategoryModels.First();

        //act
        await categoryService.UpdateAsync(category);

        //assert
        mockUnitOfWork.Verify(x => x.CategoryRepository.Update(It.Is<Category>(t =>
            t.Id == category.Id && t.Title == category.Title )), Times.Once);
        mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
    }

    
    #region TestData

    private List<Category> GetTestCategories =>
        new()
        {
            new Category { Id = 1, Title = "Programing" },
            new Category { Id = 2, Title = "Music" }
        };

    private List<CategoryModel> GetTestCategoryModels =>
        new()
        {
            new CategoryModel { Id = 1, Title = "Programing" },
            new CategoryModel { Id = 2, Title = "Music" }
        };
    
    #endregion
}

