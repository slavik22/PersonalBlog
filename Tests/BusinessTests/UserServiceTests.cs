using System.Linq.Expressions;
using BuisnessLogicLayer.Models;
using BuisnessLogicLayer.Services;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Tests.BusinessTests;

public class UserServiceTest
{
    [Test]
    public async Task UserService_GetAll_ReturnsAllUserModels()
    {
        //arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        mockUnitOfWork
            .Setup(x => x.UserRepository.GetAllAsync(null,null,""))
            .ReturnsAsync(GetTestUsers.AsEnumerable());
        
        var userService = new UserService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

        //act
        var actual = await userService.GetAllAsync();

        //assert
        actual.Should().BeEquivalentTo(GetTestUserModels);
    }

    [TestCase("email1@gmaill.com")]
    [TestCase("email2@gmaill.com")]
    public async Task UserService_GetByEmailAsync_ReturnsByEmailAsync(string email)
    {
        //arrange
        var expected = GetTestUserModels.FirstOrDefault(u => u.Email == email);
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        mockUnitOfWork
            .Setup(x => x.UserRepository.GetByValueOneAsync(It.IsAny<Expression<Func<User,bool>>>(),It.IsAny<string>()))
            .ReturnsAsync(GetTestUsers.FirstOrDefault(u => u.Email == email));
        
        var userService = new UserService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

        //act
        var actual = await userService.GetByEmailAsync(email);

        //assert
        actual.Should().BeEquivalentTo(expected);
    }
    
    
    [TestCase("email1@gmaill.com")]
    [TestCase("email2@gmaill.com")]
    [TestCase("incorrect")]
    public async Task UserService_CheckUserEmailExistAsync_ReturnsIfUserEmailExistAsync(string email)
    {
        //arrange
        bool expected = GetTestUserModels.Any(u => u.Email == email);
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        mockUnitOfWork
            .Setup(x => x.UserRepository.GetByValueOneAsync(It.IsAny<Expression<Func<User,bool>>>(),It.IsAny<string>()))
            .ReturnsAsync(GetTestUsers.FirstOrDefault(u => u.Email == email));
        
        var userService = new UserService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

        //act
        bool actual = await userService.CheckUserEmailExistAsync(email);

        //assert
        actual.Should().Be(expected);
    }

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
    
    [Test]
    public async Task UserService_GetById_ReturnsUserModel()
    {
        //arrange
        var expected = GetTestUserModels.First();
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        mockUnitOfWork
            .Setup(m => m.UserRepository.GetByIdAsync(It.IsAny<int>(),""))
            .ReturnsAsync(GetTestUsers.First());

        var userService = new UserService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

        //act
        var actual = await userService.GetByIdAsync(1);

        //assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Test]
    [Obsolete("Obsolete")]
    public async Task UserService_AddAsync_AddsUserModel()
    {
        //arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(m => m.UserRepository.AddAsync(It.IsAny<User>()));

        var userService = new UserService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
        var user = GetTestUserModels.First();

        //act
        await userService.AddAsync(user);

        //assert
        mockUnitOfWork.Verify(x => x.UserRepository.AddAsync(It.Is<User>(t =>
            t.Id == user.Id && t.Name == user.Name && t.Surname == user.Surname && t.Email == user.Email && t.Mobile == user.Mobile
            && t.Password == user.Password && t.BirthDate == user.BirthDate)), Times.Once);
        mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
    }
    
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(100)]
    public async Task UserService_DeleteAsync_DeletesUser(int id)
    {
        //arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(m => m.UserRepository.Delete(It.IsAny<int>()));
        var userService = new UserService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

        //act
        await userService.DeleteAsync(id);

        //assert
        mockUnitOfWork.Verify(x => x.UserRepository.Delete(id), Times.Once());
        mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once());
    }

    [Test]
    public async Task UserService_UpdateAsync_UpdatesUser()
    {
        //arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(m => m.UserRepository.Update(It.IsAny<User>()));

        var userService = new UserService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
        var user = GetTestUserModels.First();

        //act
        await userService.UpdateAsync(user);

        //assert
        mockUnitOfWork.Verify(x => x.UserRepository.Update(It.Is<User>(t =>
            t.Id == user.Id && t.Name == user.Name && t.Surname == user.Surname && t.Email == user.Email && t.Mobile == user.Mobile
            && t.Password == user.Password && t.BirthDate == user.BirthDate )), Times.Once);
        mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
    }

    
    #region TestData

    private List<User> GetTestUsers =>
        new()
        {
            new User{Id = 1, Email = "email1@gmaill.com", Mobile = "+380502411234", Name = "Slava", Surname = "Fedyna", Password = "password", BirthDate = new DateTime(1990,12,12)},
            new User{Id = 2, Email = "email2@gmaill.com", Mobile = "+90502411234", Name = "Tom", Surname = "Shelby", Password = "password", BirthDate = new DateTime(1991,10,12)}
        };

    private List<UserModel> GetTestUserModels =>
        new()
        {
            new UserModel{Id = 1, Email = "email1@gmaill.com",Mobile = "+380502411234", Name = "Slava", Surname = "Fedyna", Password = "password", BirthDate = new DateTime(1990,12,12)},
            new UserModel{Id = 2, Email = "email2@gmaill.com",Mobile = "+90502411234", Name = "Tom", Surname = "Shelby", Password = "password", BirthDate = new DateTime(1991,10,12)}
        };
    
    #endregion
}

