// ***********************************************************************
// Assembly         : Tests
// Author           : Slava
// Created          : 12-11-2022
//
// Last Modified By : Slava
// Last Modified On : 12-11-2022
// ***********************************************************************
// <copyright file="UserRepositoryTests.cs" company="Tests">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using NUnit.Framework;

namespace Tests.DataTests;

/// <summary>
/// Defines test class UserRepositoryTests.
/// </summary>
[TestFixture]
public class UserRepositoryTests
{
    /// <summary>
    /// Defines the test method UserRepository_GetById_ReturnsSingleValue.
    /// </summary>
    /// <param name="id">The identifier.</param>
    [TestCase(1)]
    [TestCase(2)]
    public void  UserRepository_GetById_ReturnsSingleValue(int id)
    {
        using var context = new PersonalBlogDbContext(UnitTestHelper.GetUnitTestDbOptions());

        var userRepository = new GenericRepository<User>(context);

        var user =  userRepository.GetById(id);

        var expected = ExpectedUsers.FirstOrDefault(x => x.Id == id);

        Assert.That(user, Is.EqualTo(expected).Using(new UserEqualityComparer()), message: "GetById method works incorrect");
    }

    /// <summary>
    /// Defines the test method UserRepository_GetAll_ReturnsAllValues.
    /// </summary>
    [Test]
    public void  UserRepository_GetAll_ReturnsAllValues()
    {
        using var context = new PersonalBlogDbContext(UnitTestHelper.GetUnitTestDbOptions());
        
        var userRepository = new GenericRepository<User>(context);


        var users =  userRepository.GetAll();

        Assert.That(users, Is.EqualTo(ExpectedUsers).Using(new UserEqualityComparer()), message: "GetAll method works incorrect");
    }
    /// <summary>
    /// Defines the test method UserRepository_Add_AddsValueToDatabase.
    /// </summary>
    [Test]
    public void UserRepository_Add_AddsValueToDatabase()
    {
        using var context = new PersonalBlogDbContext(UnitTestHelper.GetUnitTestDbOptions());

        var userRepository = new GenericRepository<User>(context);


        var user = new User() { Id = 3, Name = "Bill", Surname = "Gates", BirthDate = new DateTime(1990,12,12), Email = "email3@ukr.net", Mobile = "233", Password = "password"};

         userRepository.Add(user);
         context.SaveChanges();

        Assert.That(context.Users.Count(), Is.EqualTo(3), message: "Add method works incorrect");
    }


    /// <summary>
    /// Defines the test method UserRepository_DeleteById_DeletesEntity.
    /// </summary>
    [Test]
    public  void UserRepository_DeleteById_DeletesEntity()
    {
        using var context = new PersonalBlogDbContext(UnitTestHelper.GetUnitTestDbOptions());

        var userRepository = new GenericRepository<User>(context);
        
         userRepository.Delete(1);
         context.SaveChanges();

        Assert.That(context.Users.Count(), Is.EqualTo(1), message: "DeleteById works incorrect");
    }

    /// <summary>
    /// Defines the test method UserRepository_Update_UpdatesEntity.
    /// </summary>
    [Test]
    public void  UserRepository_Update_UpdatesEntity()
    {
        using var context = new PersonalBlogDbContext(UnitTestHelper.GetUnitTestDbOptions());
        var userRepository = new GenericRepository<User>(context);


       var user = new User
        {
            Id = 1, Name = "Bill", Surname = "Gates", BirthDate = new DateTime(1990,12,12), Email = "email3@ukr.net", Mobile = "233", Password = "password"
        };

        userRepository.Update(user);
         context.SaveChanges();

        Assert.That(user, Is.EqualTo(new User
        {
            Id = 1, Name = "Bill", Surname = "Gates", BirthDate = new DateTime(1990,12,12), Email = "email3@ukr.net", Mobile = "233", Password = "password"
        }).Using(new UserEqualityComparer()), message: "Update method works incorrect");
    }


    /// <summary>
    /// Gets the expected users.
    /// </summary>
    /// <value>The expected users.</value>
    private static IEnumerable<User> ExpectedUsers =>
        new[]
        {
            new User{Id = 1, Email = "email1@gmaill.com", Mobile = "+380502411234", Name = "Slava", Surname = "Fedyna", Password = "password", BirthDate = new DateTime(1990,12,12)},
            new User{Id = 2, Email = "email2@gmaill.com", Mobile = "+90502411234", Name = "Tom", Surname = "Shelby", Password = "password", BirthDate = new DateTime(1991,10,12)}
        };

}