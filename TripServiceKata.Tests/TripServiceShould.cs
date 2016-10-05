using System;
using FluentAssertions;
using NUnit.Framework;
using NSubstitute;
using TripServiceKata.Exception;
using TripServiceKata.Trip;

namespace TripServiceKata.Tests
{
    [TestFixture]
    public class TripServiceShould
    {
        private TripService tripService;

        [SetUp]
        public void SetUp()
        {
            tripService = Substitute.For<TripService>();
        }

        [Test]
        public void throw_and_exception_when_the_user_is_not_logged()
        {
            tripService.GetLoggedUser().Returns((User.User) null);

            Action action = () => tripService.GetTripsByUser(null);

            action.ShouldThrow<UserNotLoggedInException>();
        }

        [Test]
        public void not_throw_and_exception_when_the_user_is_logged()
        {
            tripService.GetLoggedUser().Returns(new User.User());

            Action action = () => tripService.GetTripsByUser(null);

            action.ShouldNotThrow<UserNotLoggedInException>();
        }
    }
}
