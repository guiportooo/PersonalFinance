using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Moq.AutoMock;
using NUnit.Framework;
using PersonalFinance.Common;
using PersonalFinance.Users;
using PersonalFinance.Users.Domain;
using PersonalFinance.Users.Models;

namespace PersonalFinance.UnitTests.Users
{
    public class UsersServiceTests
    {
        private AutoMocker _mocker;
        private IUsersService _usersService;
        
        [SetUp]
        public void Setup()
        {
            _mocker = new AutoMocker();
            _usersService = _mocker.CreateInstance<UsersService>();
        }

        [Test]
        public async Task ShouldReturnOkResultWhenGettingUserSucceeded()
        {
            var id = Guid.NewGuid();
            var user = new User(id, "FirstName", "LastName");
            var userResult = Result.Ok(user);
            _mocker.GetMock<IUsersRepository>()
                .Setup(x => x.Get(id))
                .ReturnsAsync(userResult);
            var userResponse = new UserResponse(user);
            var result = await _usersService.GetUser(id);
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(userResponse);
        }

        [Test]
        public async Task ShouldReturnFailResultWhenGettingUserFailed()
        {
            var id = Guid.NewGuid();
            const string errorMessage = "message";
            var userResult = Result.Fail<User>(errorMessage);
            _mocker.GetMock<IUsersRepository>()
                .Setup(x => x.Get(id))
                .ReturnsAsync(userResult);
            var result = await _usersService.GetUser(id);
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be(errorMessage);
        }
    }
}