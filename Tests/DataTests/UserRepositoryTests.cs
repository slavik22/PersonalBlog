﻿using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using NUnit.Framework;

namespace Tests.DataTests;

[TestFixture]
public class UserRepositoryTests
{
    [TestCase(1)]
    [TestCase(2)]
    public async Task UserRepository_GetByIdAsync_ReturnsSingleValue(int id)
    {
        using var context = new PersonalBlogDbContext(UnitTestHelper.GetUnitTestDbOptions());

        var userRepository = new GenericRepository<User>(context);

        var user = await userRepository.GetByIdAsync(id);

        var expected = ExpectedUsers.FirstOrDefault(x => x.Id == id);

        Assert.That(user, Is.EqualTo(expected).Using(new UserEqualityComparer()), message: "GetByIdAsync method works incorrect");
    }
    
    [Test]
    public async Task UserRepository_GetAllAsync_ReturnsAllValues()
    {
        using var context = new PersonalBlogDbContext(UnitTestHelper.GetUnitTestDbOptions());
        
        var userRepository = new GenericRepository<User>(context);


        var users = await userRepository.GetAllAsync();

        Assert.That(users, Is.EqualTo(ExpectedUsers).Using(new UserEqualityComparer()), message: "GetAllAsync method works incorrect");
    }
    [Test]
    public async Task UserRepository_AddAsync_AddsValueToDatabase()
    {
        using var context = new PersonalBlogDbContext(UnitTestHelper.GetUnitTestDbOptions());

        var userRepository = new GenericRepository<User>(context);


        var user = new User() { Id = 3, Name = "Bill", Surname = "Gates", BirthDate = new DateTime(1990,12,12), Email = "email3@ukr.net", Mobile = "233", Password = "password"};

        await userRepository.AddAsync(user);
        await context.SaveChangesAsync();

        Assert.That(context.Users.Count(), Is.EqualTo(3), message: "AddAsync method works incorrect");
    }

    
    [Test]
    public async Task UserRepository_DeleteByIdAsync_DeletesEntity()
    {
        using var context = new PersonalBlogDbContext(UnitTestHelper.GetUnitTestDbOptions());

        var userRepository = new GenericRepository<User>(context);
        
        await userRepository.Delete(1);
        await context.SaveChangesAsync();

        Assert.That(context.Users.Count(), Is.EqualTo(1), message: "DeleteByIdAsync works incorrect");
    }

    [Test]
    public async Task UserRepository_Update_UpdatesEntity()
    {
        using var context = new PersonalBlogDbContext(UnitTestHelper.GetUnitTestDbOptions());
        var userRepository = new GenericRepository<User>(context);


       var user = new User
        {
            Id = 1, Name = "Bill", Surname = "Gates", BirthDate = new DateTime(1990,12,12), Email = "email3@ukr.net", Mobile = "233", Password = "password"
        };

        userRepository.Update(user);
        await context.SaveChangesAsync();

        Assert.That(user, Is.EqualTo(new User
        {
            Id = 1, Name = "Bill", Surname = "Gates", BirthDate = new DateTime(1990,12,12), Email = "email3@ukr.net", Mobile = "233", Password = "password"
        }).Using(new UserEqualityComparer()), message: "Update method works incorrect");
    }

    
    private static IEnumerable<User> ExpectedUsers =>
        new[]
        {
            new User{Id = 1, Email = "email1@gmaill.com", Mobile = "+380502411234", Name = "Slava", Surname = "Fedyna", Password = "password", BirthDate = new DateTime(1990,12,12)},
            new User{Id = 2, Email = "email2@gmaill.com", Mobile = "+90502411234", Name = "Tom", Surname = "Shelby", Password = "password", BirthDate = new DateTime(1991,10,12)}
        };

}