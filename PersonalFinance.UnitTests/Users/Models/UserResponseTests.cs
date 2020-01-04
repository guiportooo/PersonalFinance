using System;
using FluentAssertions;
using NUnit.Framework;
using PersonalFinance.Users.Domain;
using PersonalFinance.Users.Models;

namespace PersonalFinance.UnitTests.Users.Models
{
    public class UserResponseTests
    {
        [Test]
        public void ShouldCreateUserResponse()
        {
            var id = Guid.NewGuid();
            const string firstName = "FirstName";
            const string lastName = "LastName";
            const string fullName = "FirstName LastName";
            var user = new User(id, firstName, lastName);
            var userResponse = new UserResponse(user);
            userResponse.Id.Should().Be(id);
            userResponse.FirstName.Should().Be(firstName);
            userResponse.LastName.Should().Be(lastName);
            userResponse.FullName.Should().Be(fullName);
        }
    }
}