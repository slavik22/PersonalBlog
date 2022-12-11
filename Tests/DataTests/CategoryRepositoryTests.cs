// ***********************************************************************
// Assembly         : Tests
// Author           : Slava
// Created          : 12-11-2022
//
// Last Modified By : Slava
// Last Modified On : 12-11-2022
// ***********************************************************************
// <copyright file="CategoryRepositoryTests.cs" company="Tests">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using NUnit.Framework;

namespace Tests.DataTests;

/// <summary>
/// Defines test class CategoryRepositoryTests.
/// </summary>
[TestFixture]
public class CategoryRepositoryTests
{
    /// <summary>
    /// Defines the test method CategoryRepository_GetByIdAsync_ReturnsSingleValue.
    /// </summary>
    /// <param name="id">The identifier.</param>
    [TestCase(1)]
    [TestCase(2)]
    public async Task CategoryRepository_GetByIdAsync_ReturnsSingleValue(int id)
    {
        using var context = new PersonalBlogDbContext(UnitTestHelper.GetUnitTestDbOptions());

        var categoryRepository = new GenericRepository<Category>(context);

        var category = await categoryRepository.GetByIdAsync(id);

        var expected = ExpectedCategories.FirstOrDefault(x => x.Id == id);

        Assert.That(category, Is.EqualTo(expected).Using(new CategoryEqualityComparer()), message: "GetByIdAsync method works incorrect");
    }

    /// <summary>
    /// Defines the test method CategoryRepository_GetAllAsync_ReturnsAllValues.
    /// </summary>
    [Test]
    public async Task CategoryRepository_GetAllAsync_ReturnsAllValues()
    {
        using var context = new PersonalBlogDbContext(UnitTestHelper.GetUnitTestDbOptions());

        var categoryRepository = new GenericRepository<Category>(context);

        var categories = await categoryRepository.GetAllAsync();

        Assert.That(categories, Is.EqualTo(ExpectedCategories).Using(new CategoryEqualityComparer()), message: "GetAllAsync method works incorrect");
    }
    /// <summary>
    /// Defines the test method CategoryRepository_AddAsync_AddsValueToDatabase.
    /// </summary>
    [Test]
    public async Task CategoryRepository_AddAsync_AddsValueToDatabase()
    {
        using var context = new PersonalBlogDbContext(UnitTestHelper.GetUnitTestDbOptions());

        var categoryRepository = new GenericRepository<Category?>(context);
        var category = new Category { Id = 3, Title = "New"};

        await categoryRepository.AddAsync(category);
        await context.SaveChangesAsync();

        Assert.That(context.Categories.Count(), Is.EqualTo(3), message: "AddAsync method works incorrect");
    }


    /// <summary>
    /// Defines the test method CategoryRepository_DeleteByIdAsync_DeletesEntity.
    /// </summary>
    [Test]
    public async Task CategoryRepository_DeleteByIdAsync_DeletesEntity()
    {
        using var context = new PersonalBlogDbContext(UnitTestHelper.GetUnitTestDbOptions());

        var categoryRepository = new GenericRepository<Category>(context);

        await categoryRepository.Delete(1);
        await context.SaveChangesAsync();

        Assert.That(context.Categories.Count(), Is.EqualTo(1), message: "DeleteByIdAsync works incorrect");
    }

    /// <summary>
    /// Defines the test method CategoryRepository_Update_UpdatesEntity.
    /// </summary>
    [Test]
    public async Task CategoryRepository_Update_UpdatesEntity()
    {
        using var context = new PersonalBlogDbContext(UnitTestHelper.GetUnitTestDbOptions());
        var categoryRepository = new GenericRepository<Category>(context);

        var category = new Category
        {
            Id = 1,
            Title = "Updated"
        };

        categoryRepository.Update(category);
        await context.SaveChangesAsync();

        Assert.That(category, Is.EqualTo(new Category
        {
            Id = 1,
            Title = "Updated"
        }).Using(new CategoryEqualityComparer()), message: "Update method works incorrect");
    }


    /// <summary>
    /// Gets the expected categories.
    /// </summary>
    /// <value>The expected categories.</value>
    private static IEnumerable<Category> ExpectedCategories =>
        new[]
        {
            new Category { Id = 1, Title = "Programing" },
            new Category { Id = 2, Title = "Music" }
        };

}