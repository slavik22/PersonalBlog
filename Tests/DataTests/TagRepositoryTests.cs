// ***********************************************************************
// Assembly         : Tests
// Author           : Slava
// Created          : 12-11-2022
//
// Last Modified By : Slava
// Last Modified On : 12-11-2022
// ***********************************************************************
// <copyright file="TagRepositoryTests.cs" company="Tests">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using NUnit.Framework;

namespace Tests.DataTests;

/// <summary>
/// Defines test class TagRepositoryTests.
/// </summary>
[TestFixture]
public class TagRepositoryTests
{
    /// <summary>
    /// Defines the test method TagRepository_GetById_ReturnsSingleValue.
    /// </summary>
    /// <param name="id">The identifier.</param>
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    public void  TagRepository_GetById_ReturnsSingleValue(int id)
    {
        using var context = new PersonalBlogDbContext(UnitTestHelper.GetUnitTestDbOptions());

        var tagRepository = new GenericRepository<Tag>(context);

        var tag =  tagRepository.GetById(id);

        var expected = ExpectedTags.FirstOrDefault(x => x.Id == id);

        Assert.That(tag, Is.EqualTo(expected).Using(new TagEqualityComparer()), message: "GetById method works incorrect");
    }

    /// <summary>
    /// Defines the test method CategoryRepository_GetAll_ReturnsAllValues.
    /// </summary>
    [Test]
    public void  CategoryRepository_GetAll_ReturnsAllValues()
    {
        using var context = new PersonalBlogDbContext(UnitTestHelper.GetUnitTestDbOptions());

        var tagRepository = new GenericRepository<Tag>(context);

        var tags =  tagRepository.GetAll();

        Assert.That(tags, Is.EqualTo(ExpectedTags).Using(new TagEqualityComparer()), message: "GetAll method works incorrect");
    }
    /// <summary>
    /// Defines the test method CategoryRepository_Add_AddsValueToDatabase.
    /// </summary>
    [Test]
    public void  CategoryRepository_Add_AddsValueToDatabase()
    {
        using var context = new PersonalBlogDbContext(UnitTestHelper.GetUnitTestDbOptions());

        var tagRepository = new GenericRepository<Tag>(context);
        var tag = new Tag { Id = 4, Title = "New"};

         tagRepository.Add(tag);
         context.SaveChanges();

        Assert.That(context.Tags.Count(), Is.EqualTo(4), message: "Add method works incorrect");
    }


    /// <summary>
    /// Defines the test method CategoryRepository_DeleteById_DeletesEntity.
    /// </summary>
    [Test]
    public void  CategoryRepository_DeleteById_DeletesEntity()
    {
        using var context = new PersonalBlogDbContext(UnitTestHelper.GetUnitTestDbOptions());

        var tagRepository = new GenericRepository<Tag>(context);

         tagRepository.Delete(1);
         context.SaveChanges();

        Assert.That(context.Tags.Count(), Is.EqualTo(2), message: "DeleteById works incorrect");
    }

    /// <summary>
    /// Defines the test method CategoryRepository_Update_UpdatesEntity.
    /// </summary>
    [Test]
    public void  CategoryRepository_Update_UpdatesEntity()
    {
        using var context = new PersonalBlogDbContext(UnitTestHelper.GetUnitTestDbOptions());
        var tagRepository = new GenericRepository<Tag>(context);

        var tag = new Tag
        {
            Id = 1,
            Title = "Updated"
        };

        tagRepository.Update(tag);
         context.SaveChanges();

        Assert.That(tag, Is.EqualTo(new Tag
        {
            Id = 1,
            Title = "Updated"
        }).Using(new TagEqualityComparer()), message: "Update method works incorrect");
    }


    /// <summary>
    /// Gets the expected tags.
    /// </summary>
    /// <value>The expected tags.</value>
    private static IEnumerable<Tag> ExpectedTags =>
        new[]
        {
            new Tag { Id = 1, Title = ".net" },
            new Tag { Id = 2, Title = "java" },
            new Tag { Id = 3, Title = "pop" }
        };

}