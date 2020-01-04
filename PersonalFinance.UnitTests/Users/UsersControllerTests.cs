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
        public async Task ShouldReturnUserResponseWhenGettingUserSucceeded()
        {
            var id = Guid.NewGuid();
            var user = new User(id, "FirstName", "LastName");
            var userResponse = new UserResponse(user);
            var ok = Result.Ok(userResponse);
            _mocker.GetMock<IUsersService>()
                .Setup(x => x.GetUser(id))
                .ReturnsAsync(ok);
            var result = await _controller.Get(id);
            (result as OkObjectResult)?.Value.Should().Be(userResponse);
        }

        [Test]
        public async Task ShouldReturnErrorMessageWhenGettingUserFailed()
        {
            var id = Guid.NewGuid();
            const string errorMessage = "message";
            var fail = Result.Fail<UserResponse>(errorMessage);
            _mocker.GetMock<IUsersService>()
                .Setup(x => x.GetUser(id))
                .ReturnsAsync(fail);
            var result = await _controller.Get(id);
            (result as BadRequestObjectResult)?.Value.Should().Be(errorMessage);
        }
    }
}