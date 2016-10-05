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
        [Test]
        public void throw_and_exception_when_the_user_is_not_logged()
        {
            var tripService = Substitute.For<TripService>();
            tripService.GetLoggedUser().Returns((User.User) null);

            Action action = () => tripService.GetTripsByUser(null);

            action.ShouldThrow<UserNotLoggedInException>();



        }
    }
}
