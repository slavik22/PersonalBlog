// ***********************************************************************
// Assembly         : Tests
// Author           : Slava
// Created          : 12-11-2022
//
// Last Modified By : Slava
// Last Modified On : 12-11-2022
// ***********************************************************************
// <copyright file="CommentRepositoryTests.cs" company="Tests">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using NUnit.Framework;

namespace Tests.DataTests;

/// <summary>
/// Defines test class CommentRepositoryTests.
/// </summary>
[TestFixture]
public class CommentRepositoryTests
{
    /// <summary>
    /// Defines the test method CommentRepository_GetById_ReturnsSingleValue.
    /// </summary>
    /// <param name="id">The identifier.</param>
    [TestCase(1)]
    [TestCase(2)]
    public void  CommentRepository_GetById_ReturnsSingleValue(int id)
    {
        using var context = new PersonalBlogDbContext(UnitTestHelper.GetUnitTestDbOptions());

        var commentRepository = new GenericRepository<Comment>(context);

        var comment =  commentRepository.GetById(id);

        var expected = ExpectedComments.FirstOrDefault(x => x.Id == id);

        Assert.That(comment, Is.EqualTo(expected).Using(new CommentEqualityComparer()), message: "GetById method works incorrect");
    }

    /// <summary>
    /// Defines the test method CommentRepository_GetAll_ReturnsAllValues.
    /// </summary>
    [Test]
    public void  CommentRepository_GetAll_ReturnsAllValues()
    {
        using var context = new PersonalBlogDbContext(UnitTestHelper.GetUnitTestDbOptions());
        
        var commentRepository = new GenericRepository<Comment>(context);

        var comments =  commentRepository.GetAll();

        Assert.That(comments, Is.EqualTo(ExpectedComments).Using(new CommentEqualityComparer()), message: "GetAll method works incorrect");
    }
    /// <summary>
    /// Defines the test method CommentRepository_Add_AddsValueToDatabase.
    /// </summary>
    [Test]
    public void  CommentRepository_Add_AddsValueToDatabase()
    {
        using var context = new PersonalBlogDbContext(UnitTestHelper.GetUnitTestDbOptions());

        var commentRepository = new GenericRepository<Comment>(context);

        var comment = new Comment() { Id = 3, Title = "New", Content = "New", PostId = 1};

         commentRepository.Add(comment);
         context.SaveChanges();

        Assert.That(context.Tags.Count(), Is.EqualTo(3), message: "Add method works incorrect");
    }


    /// <summary>
    /// Defines the test method CommentRepository_DeleteById_DeletesEntity.
    /// </summary>
    [Test]
    public void  CommentRepository_DeleteById_DeletesEntity()
    {
        using var context = new PersonalBlogDbContext(UnitTestHelper.GetUnitTestDbOptions());

        var commentRepository = new GenericRepository<Comment>(context);


         commentRepository.Delete(1);
         context.SaveChanges();

        Assert.That(context.Comments.Count(), Is.EqualTo(1), message: "DeleteById works incorrect");
    }

    /// <summary>
    /// Defines the test method CommentRepository_Update_UpdatesEntity.
    /// </summary>
    [Test]
    public void  CommentRepository_Update_UpdatesEntity()
    {
        using var context = new PersonalBlogDbContext(UnitTestHelper.GetUnitTestDbOptions());
        var commentRepository = new GenericRepository<Comment>(context);


        var comment = new Comment
        {
            Id = 1, Title = "New", Content = "New", PostId = 1
        };

        commentRepository.Update(comment);
         context.SaveChanges();

        Assert.That(comment, Is.EqualTo(new Comment
        {
            Id = 1, Title = "New", Content = "New", PostId = 1
        }).Using(new CommentEqualityComparer()), message: "Update method works incorrect");
    }


    /// <summary>
    /// Gets the expected comments.
    /// </summary>
    /// <value>The expected comments.</value>
    private static IEnumerable<Comment> ExpectedComments =>
        new[]
        {
            new Comment {Id = 1, Title = "Hello from Slava", Content = "I completely agree with author's opinion", PostId = 1},
            new Comment {Id = 2, Title = "Hello from Tom", Content = "I completely agree with author's opinion", PostId = 2}
        };

}