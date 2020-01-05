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
            _mocker.GetMock<IUsersRepository>()
                .Setup(x => x.Get(id))
                .ReturnsAsync(Result.Ok(user));
            var result = await _usersService.GetUser(id);
            var expectedResult = Result.Ok(new UserResponse(user));
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task ShouldReturnFailResultWhenGettingUserFailed()
        {
            var id = Guid.NewGuid();
            const string errorMessage = "message";
            _mocker.GetMock<IUsersRepository>()
                .Setup(x => x.Get(id))
                .ReturnsAsync(Result.Fail<User>(errorMessage));
            var result = await _usersService.GetUser(id);
            var expectedResult = Result.Fail<UserResponse>(errorMessage);
            result.Should().BeEquivalentTo(expectedResult, 
                opt => opt.Excluding(x => x.Value));
        }

        [Test]
        public async Task ShouldReturnOkResultWhenCreatingUserSucceeded()
        {
            const string firstName = "FirstName";
            const string lastName = "LastName";
            var id = new Guid();
            _mocker.GetMock<IUsersRepository>()
                .Setup(x => x.Add(It.IsAny<User>()))
                .Callback<User>(user =>
                {
                    user.FirstName.Should().Be(firstName);
                    user.LastName.Should().Be(lastName);
                    id = user.Id;
                })
                .ReturnsAsync(Result.Ok());
            var createUserRequest = new CreateUserRequest{FirstName = firstName, LastName = lastName};
            var result = await _usersService.CreateUser(createUserRequest);
            var expectedResult = Result.Ok(new UserCreatedResponse(id));
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task ShouldReturnFailResultWhenCreatingUserFailed()
        {
            const string firstName = "FirstName";
            const string lastName = "LastName";
            const string errorMessage = "message";
            _mocker.GetMock<IUsersRepository>()
                .Setup(x => x.Add(It.IsAny<User>()))
                .Callback<User>(user =>
                {
                    user.FirstName.Should().Be(firstName);
                    user.LastName.Should().Be(lastName);
                })
                .ReturnsAsync(Result.Fail(errorMessage));
            var createUserRequest = new CreateUserRequest{FirstName = firstName, LastName = lastName};
            var result = await _usersService.CreateUser(createUserRequest);
            var expectedResult = Result.Fail(errorMessage);
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}