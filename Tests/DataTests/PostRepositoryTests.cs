// ***********************************************************************
// Assembly         : Tests
// Author           : Slava
// Created          : 12-11-2022
//
// Last Modified By : Slava
// Last Modified On : 12-11-2022
// ***********************************************************************
// <copyright file="PostRepositoryTests.cs" company="Tests">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using NUnit.Framework;

namespace Tests.DataTests;

/// <summary>
/// Defines test class PostRepositoryTests.
/// </summary>
[TestFixture]
public class PostRepositoryTests
{
    /// <summary>
    /// Defines the test method PostRepository_GetById_ReturnsSingleValue.
    /// </summary>
    /// <param name="id">The identifier.</param>
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    public void  PostRepository_GetById_ReturnsSingleValue(int id)
    {
        using var context = new PersonalBlogDbContext(UnitTestHelper.GetUnitTestDbOptions());

        var postRepository = new GenericRepository<Post>(context);

        var post =  postRepository.GetById(id);

        var expected = ExpectedPosts.FirstOrDefault(x => x.Id == id);

        Assert.That(post, Is.EqualTo(expected).Using(new PostEqualityComparer()), message: "GetById method works incorrect");
    }

    /// <summary>
    /// Defines the test method PostRepository_GetAll_ReturnsAllValues.
    /// </summary>
    [Test]
    public void  PostRepository_GetAll_ReturnsAllValues()
    {
        using var context = new PersonalBlogDbContext(UnitTestHelper.GetUnitTestDbOptions());
        
        var postRepository = new GenericRepository<Post>(context);


        var posts =  postRepository.GetAll();

        Assert.That(posts, Is.EqualTo(ExpectedPosts).Using(new PostEqualityComparer()), message: "GetAll method works incorrect");
    }
    /// <summary>
    /// Defines the test method PostRepository_Add_AddsValueToDatabase.
    /// </summary>
    [Test]
    public  void  PostRepository_Add_AddsValueToDatabase()
    {
        using var context = new PersonalBlogDbContext(UnitTestHelper.GetUnitTestDbOptions());

        var postRepository = new GenericRepository<Post>(context);


        var post = new Post() { Id = 4, Title = "New", Summary = "New", Content = "New", UserId = 1};

         postRepository.Add(post);
         context.SaveChanges();

        Assert.That(context.Posts.Count(), Is.EqualTo(4), message: "Add method works incorrect");
    }


    /// <summary>
    /// Defines the test method PostRepository_DeleteById_DeletesEntity.
    /// </summary>
    [Test]
    public void  PostRepository_DeleteById_DeletesEntity()
    {
        using var context = new PersonalBlogDbContext(UnitTestHelper.GetUnitTestDbOptions());

        var postRepository = new GenericRepository<Post>(context);
        
         postRepository.Delete(1);
         context.SaveChanges();

        Assert.That(context.Users.Count(), Is.EqualTo(2), message: "DeleteById works incorrect");
    }

    /// <summary>
    /// Defines the test method PostRepository_Update_UpdatesEntity.
    /// </summary>
    [Test]
    public  void PostRepository_Update_UpdatesEntity()
    {
        using var context = new PersonalBlogDbContext(UnitTestHelper.GetUnitTestDbOptions());
        var postRepository = new GenericRepository<Post>(context);
        
        var post = new Post
        {
            Id = 1, Title = "New", Summary = "New", Content = "New", UserId = 1
        };

        postRepository.Update(post);
         context.SaveChanges();

        Assert.That(post, Is.EqualTo(new Post
        {
            Id = 1, Title = "New", Summary = "New", Content = "New", UserId = 1
        }).Using(new PostEqualityComparer()), message: "Update method works incorrect");
    }


    /// <summary>
    /// Gets the expected posts.
    /// </summary>
    /// <value>The expected posts.</value>
    private static IEnumerable<Post> ExpectedPosts =>
        new[]
        {
            new Post { Id = 1, Title = "Hello world", Summary = ".net", Content = ".net", UserId = 1 },
            new Post { Id = 2, Title = "Hello world", Summary = "java", Content = "java", UserId = 2 },
            new Post { Id = 3, Title = "Hello world", Summary = "pop", Content = "pop music", UserId = 3 }
        };

}