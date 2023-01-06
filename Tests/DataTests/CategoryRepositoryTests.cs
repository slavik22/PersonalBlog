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
    /// Defines the test method CategoryRepository_GetById_ReturnsSingleValue.
    /// </summary>
    /// <param name="id">The identifier.</param>
    [TestCase(1)]
    [TestCase(2)]
    public void  CategoryRepository_GetById_ReturnsSingleValue(int id)
    {
        using var context = new PersonalBlogDbContext(UnitTestHelper.GetUnitTestDbOptions());

        var categoryRepository = new GenericRepository<Category>(context);

        var category =  categoryRepository.GetById(id);

        var expected = ExpectedCategories.FirstOrDefault(x => x.Id == id);

        Assert.That(category, Is.EqualTo(expected).Using(new CategoryEqualityComparer()), message: "GetById method works incorrect");
    }

    /// <summary>
    /// Defines the test method CategoryRepository_GetAll_ReturnsAllValues.
    /// </summary>
    [Test]
    public  void  CategoryRepository_GetAll_ReturnsAllValues()
    {
        using var context = new PersonalBlogDbContext(UnitTestHelper.GetUnitTestDbOptions());

        var categoryRepository = new GenericRepository<Category>(context);

        var categories =  categoryRepository.GetAll();

        Assert.That(categories, Is.EqualTo(ExpectedCategories).Using(new CategoryEqualityComparer()), message: "GetAll method works incorrect");
    }
    /// <summary>
    /// Defines the test method CategoryRepository_Add_AddsValueToDatabase.
    /// </summary>
    [Test]
    public void  CategoryRepository_Add_AddsValueToDatabase()
    {
        using var context = new PersonalBlogDbContext(UnitTestHelper.GetUnitTestDbOptions());

        var categoryRepository = new GenericRepository<Category>(context);
        var category = new Category { Id = 3, Title = "New"};

         categoryRepository.Add(category);
         context.SaveChanges();

        Assert.That(context.Categories.Count(), Is.EqualTo(3), message: "Add method works incorrect");
    }


    /// <summary>
    /// Defines the test method CategoryRepository_DeleteById_DeletesEntity.
    /// </summary>
    [Test]
    public void  CategoryRepository_DeleteById_DeletesEntity()
    {
        using var context = new PersonalBlogDbContext(UnitTestHelper.GetUnitTestDbOptions());

        var categoryRepository = new GenericRepository<Category>(context);

         categoryRepository.Delete(1);
         context.SaveChanges();

        Assert.That(context.Categories.Count(), Is.EqualTo(1), message: "DeleteById works incorrect");
    }

    /// <summary>
    /// Defines the test method CategoryRepository_Update_UpdatesEntity.
    /// </summary>
    [Test]
    public void  CategoryRepository_Update_UpdatesEntity()
    {
        using var context = new PersonalBlogDbContext(UnitTestHelper.GetUnitTestDbOptions());
        var categoryRepository = new GenericRepository<Category>(context);

        var category = new Category
        {
            Id = 1,
            Title = "Updated"
        };

        categoryRepository.Update(category);
         context.SaveChanges();

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