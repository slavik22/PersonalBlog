// ***********************************************************************
// Assembly         : Tests
// Author           : Slava
// Created          : 12-11-2022
//
// Last Modified By : Slava
// Last Modified On : 12-11-2022
// ***********************************************************************
// <copyright file="UserServiceTests.cs" company="Tests">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Linq.Expressions;
using BuisnessLogicLayer.Models;
using BuisnessLogicLayer.Services;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Tests.BusinessTests;

/// <summary>
/// Class UserServiceTest.
/// </summary>
public class UserServiceTest
{
    /// <summary>
    /// Defines the test method UserService_GetAll_ReturnsAllUserModels.
    /// </summary>
    [Test]
    public void  UserService_GetAll_ReturnsAllUserModels()
    {
        //arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        mockUnitOfWork
            .Setup(x => x.UserRepository.GetAll(null,""))
            .Returns(GetTestUsers.AsEnumerable());
        
        var userService = new UserService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

        //act
        var actual =  userService.GetAll();

        //assert
        actual.Should().BeEquivalentTo(GetTestUserModels);
    }


    [TestCase("email1@gmaill.com")]
    [TestCase("email2@gmaill.com")]
    public void  UserService_GetByEmail_ReturnsByEmail(string email)
    {
        //arrange
        var expected = GetTestUserModels.FirstOrDefault(u => u.Email == email);
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        mockUnitOfWork
            .Setup(x => x.UserRepository.GetAll(It.IsAny<Expression<Func<User,bool>>>(),It.IsAny<string>()))
            .Returns(GetTestUsers.Where(u => u.Email == email));
        
        var userService = new UserService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

        //act
        var actual =  userService.GetByEmail(email);

        //assert
        actual.Should().BeEquivalentTo(expected);
    }



    [TestCase("email1@gmaill.com")]
    [TestCase("email2@gmaill.com")]
    [TestCase("incorrect")]
    public void  UserService_CheckUserEmailExist_ReturnsIfUserEmailExist(string email)
    {
        //arrange
        bool expected = GetTestUserModels.Any(u => u.Email == email);
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        mockUnitOfWork
            .Setup(x => x.UserRepository.GetAll(It.IsAny<Expression<Func<User,bool>>>(),It.IsAny<string>()))
            .Returns(GetTestUsers.Where(u => u.Email == email));
        
        var userService = new UserService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

        //act
        bool actual =  userService.CheckUserEmailExist(email);

        //assert
        actual.Should().Be(expected);
    }

    /// <summary>
    /// Defines the test method UserService_CheckUserPasswordAndEmail_ReturnsIfUserPasswordAndEmailAreCorrect.
    /// </summary>
    /// <param name="email">The email.</param>
    /// <param name="password">The password.</param>
    /// <param name="expectedResult">The expected result.</param>
    [TestCase("email1gmaill.com","Slava11111!","Email is incorrect.")]
    [TestCase("email2@gmaill.com","Slava1!","Minimum password length should be 9.")]
    [TestCase("email2@gmaill.com","Slavaaaaa!","Password should be Alphanumeric.")]
    [TestCase("email2@gmaill.com","Slavaaaaa!","Password should be Alphanumeric.")]
    [TestCase("email2@gmaill.com","Slavaaaaa1","Password should contain special chars.")]
    public void UserService_CheckUserPasswordAndEmail_ReturnsIfUserPasswordAndEmailAreCorrect(string email,string password, string expectedResult)
    {
        //arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var userService = new UserService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

        //act
        string actual = userService.CheckUserPasswordAndEmail(email,password).Split(".")[0] + ".";

        //assert
        actual.Should().Be(expectedResult);
    }

    /// <summary>
    /// Defines the test method UserService_GetById_ReturnsUserModel.
    /// </summary>
    [Test]
    public void  UserService_GetById_ReturnsUserModel()
    {
        //arrange
        var expected = GetTestUserModels.First();
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        mockUnitOfWork
            .Setup(m => m.UserRepository.GetById(It.IsAny<int>(),""))
            .Returns(GetTestUsers.First());

        var userService = new UserService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

        //act
        var actual =  userService.GetById(1);

        //assert
        actual.Should().BeEquivalentTo(expected);
    }

    /// <summary>
    /// Defines the test method UserService_Add_AddsUserModel.
    /// </summary>
    [Test]
    [Obsolete("Obsolete")]
    public void  UserService_Add_AddsUserModel()
    {
        //arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(m => m.UserRepository.Add(It.IsAny<User>()));

        var userService = new UserService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
        var user = GetTestUserModels.First();

        //act
         userService.Add(user);

        //assert
        mockUnitOfWork.Verify(x => x.UserRepository.Add(It.Is<User>(t =>
            t.Id == user.Id && t.Name == user.Name && t.Surname == user.Surname && t.Email == user.Email && t.Mobile == user.Mobile
            && t.Password == user.Password && t.BirthDate == user.BirthDate)), Times.Once);
        mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
    }

    /// <summary>
    /// Defines the test method UserService_Delete_DeletesUser.
    /// </summary>
    /// <param name="id">The identifier.</param>
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(100)]
    public void  UserService_Delete_DeletesUser(int id)
    {
        //arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(m => m.UserRepository.Delete(It.IsAny<int>()));
        var userService = new UserService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

        //act
         userService.Delete(id);

        //assert
        mockUnitOfWork.Verify(x => x.UserRepository.Delete(id), Times.Once());
        mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once());
    }

    /// <summary>
    /// Defines the test method UserService_Update_UpdatesUser.
    /// </summary>
    [Test]
    public void  UserService_Update_UpdatesUser()
    {
        //arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(m => m.UserRepository.Update(It.IsAny<User>()));

        var userService = new UserService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
        var user = GetTestUserModels.First();

        //act
         userService.Update(user);

        //assert
        mockUnitOfWork.Verify(x => x.UserRepository.Update(It.Is<User>(t =>
            t.Id == user.Id && t.Name == user.Name && t.Surname == user.Surname && t.Email == user.Email && t.Mobile == user.Mobile
            && t.Password == user.Password && t.BirthDate == user.BirthDate )), Times.Once);
        mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
    }


    #region TestData

    /// <summary>
    /// Gets the get test users.
    /// </summary>
    /// <value>The get test users.</value>
    private List<User> GetTestUsers =>
        new()
        {
            new User{Id = 1, Email = "email1@gmaill.com", Mobile = "+380502411234", Name = "Slava", Surname = "Fedyna", Password = "password", BirthDate = new DateTime(1990,12,12)},
            new User{Id = 2, Email = "email2@gmaill.com", Mobile = "+90502411234", Name = "Tom", Surname = "Shelby", Password = "password", BirthDate = new DateTime(1991,10,12)}
        };

    /// <summary>
    /// Gets the get test user models.
    /// </summary>
    /// <value>The get test user models.</value>
    private List<UserModel> GetTestUserModels =>
        new()
        {
            new UserModel{Id = 1, Email = "email1@gmaill.com",Mobile = "+380502411234", Name = "Slava", Surname = "Fedyna", Password = "password", BirthDate = new DateTime(1990,12,12)},
            new UserModel{Id = 2, Email = "email2@gmaill.com",Mobile = "+90502411234", Name = "Tom", Surname = "Shelby", Password = "password", BirthDate = new DateTime(1991,10,12)}
        };
    
    #endregion
}

