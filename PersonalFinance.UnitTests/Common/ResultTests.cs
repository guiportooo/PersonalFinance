using System;
using FluentAssertions;
using NUnit.Framework;
using PersonalFinance.Common;

namespace PersonalFinance.UnitTests.Common
{
    public class ResultTests
    {
        [Test]
        public void IsSuccessShouldBeTrueForOkResult()
        {
            var ok = Result.Ok();
            ok.IsSuccess.Should().BeTrue();
        }

        [Test]
        public void IsSuccessShouldBeFalseForFailResult()
        {
            var fail = Result.Fail("message");
            fail.IsSuccess.Should().BeFalse();
        }

        [Test]
        public void ShouldSetErrorMessageForFailResult()
        {
            const string errorMessage = "message";
            var fail = Result.Fail(errorMessage);
            fail.Error.Should().Be(errorMessage);
        }

        [Test]
        public void ShouldThrowInvalidOperationWhenErrorMessageIsEmptyForFailResult()
        {
            Action createFail = () => Result.Fail(string.Empty);
            createFail.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void ShouldSetValueForGenericOkResult()
        {
            var value = new ValueResult {Value = "valueResult"};
            var ok = Result.Ok(value);
            ok.Value.Should().BeEquivalentTo(value);
        }

        [Test]
        public void ShouldSetErrorMessageForGenericFailResult()
        {
            const string errorMessage = "message";
            var fail = Result<ValueResult>.Fail(errorMessage);
            fail.Error.Should().Be(errorMessage);
        }
    }

    public class ValueResult
    {
        public string Value { get; set; }
    }
}