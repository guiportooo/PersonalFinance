using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.AutoMock;
using NUnit.Framework;
using PersonalFinance.Common;
using PersonalFinance.Users;
using PersonalFinance.Users.Domain;
using PersonalFinance.Users.Models;

namespace PersonalFinance.UnitTests.Users
{
    public class UsersControllerTests
    {
        private AutoMocker _mocker;
        private UsersController _controller;

        [SetUp]
        public void Setup()
        {
            _mocker = new AutoMocker();
            _controller = _mocker.CreateInstance<UsersController>();
        }

        [Test]
        public async Task ShouldReturnOkWhenGettingUserSucceeded()
        {
            var id = Guid.NewGuid();
            const string firstName = "FirstName";
            const string lastName = "LastName";
            var user = new User(id, firstName, lastName);
            var userResponse = new UserResponse(user);
            var ok = Result.Ok(userResponse);
            _mocker.GetMock<IUsersService>()
                .Setup(x => x.GetUser(id))
                .ReturnsAsync(ok);
            var result = await _controller.Get(id);
            var expectedResult = new OkObjectResult(userResponse);
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task ShouldReturnBadRequestWhenGettingUserFailed()
        {
            var id = Guid.NewGuid();
            const string errorMessage = "message";
            var fail = Result.Fail<UserResponse>(errorMessage);
            _mocker.GetMock<IUsersService>()
                .Setup(x => x.GetUser(id))
                .ReturnsAsync(fail);
            var result = await _controller.Get(id);
            var expectedResult = new BadRequestObjectResult(errorMessage);
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task ShouldReturnUserCreatedAtRouteWhenCreatingUserSucceeded()
        {
            var id = Guid.NewGuid();
            const string firstName = "FirstName";
            const string lastName = "LastName";
            var createUserRequest = new CreateUserRequest {FirstName = firstName, LastName = lastName};
            var userCreatedResponse = new UserCreatedResponse(id);
            var ok = Result.Ok(userCreatedResponse);
            _mocker.GetMock<IUsersService>()
                .Setup(x => x.CreateUser(createUserRequest))
                .ReturnsAsync(ok);
            var result = await _controller.Create(createUserRequest);
            var expectedResult = new CreatedAtRouteResult("Get", new {id}, userCreatedResponse);
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task ShouldReturnBadRequestWhenCreatingUserFailed()
        {
            const string firstName = "FirstName";
            const string lastName = "LastName";
            const string errorMessage = "message";
            var createUserRequest = new CreateUserRequest {FirstName = firstName, LastName = lastName};
            var fail = Result.Fail<UserCreatedResponse>(errorMessage);
            _mocker.GetMock<IUsersService>()
                .Setup(x => x.CreateUser(createUserRequest))
                .ReturnsAsync(fail);
            var result = await _controller.Create(createUserRequest);
            var expectedResult = new BadRequestObjectResult(errorMessage);
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}