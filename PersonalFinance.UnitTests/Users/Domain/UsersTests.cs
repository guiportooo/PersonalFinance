using System;
using FluentAssertions;
using NUnit.Framework;
using PersonalFinance.Users.Domain;

namespace PersonalFinance.UnitTests.Users.Domain
{
    public class UsersTests
    {
        [Test]
        public void ShouldCreateUser()
        {
            var id = Guid.NewGuid();
            const string firstName = "FirstName";
            const string lastName = "LastName";
            var user = new User(id, firstName, lastName);
            user.Id.Should().Be(id);
            user.FirstName.Should().Be(firstName);
            user.LastName.Should().Be(lastName);
        }

        [Test]
        public void ShouldReturnUsersFullName()
        {
            var id = Guid.NewGuid();
            const string firstName = "FirstName";
            const string lastName = "LastName";
            var user = new User(id, firstName, lastName);
            user.FullName.Should().Be("FirstName LastName");
        }
    }
}