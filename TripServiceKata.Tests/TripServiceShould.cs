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
            tripService.LoggedUser().Returns((User.User) null);

            Action action = () => tripService.GetTripsByUser(null);

            action.ShouldThrow<UserNotLoggedInException>();
        }


        [Test]
        public void not_throw_and_exception_when_the_user_is_logged()
        {
            tripService.LoggedUser().Returns(new User.User());

            Action action = () => tripService.GetTripsByUser(null);

            action.ShouldNotThrow<UserNotLoggedInException>();
        }


        [Test]
        public void not_get_frinend_trips_if_users_are_not_friends()
        {
            var bob = new User.User();
            tripService.LoggedUser().Returns(bob);

            tripService.GetTripsByUser(bob);

            tripService.DidNotReceive().FindTripsByUser(Arg.Is(bob));
        }


        [Test]
        public void find_friend_trips_if_users_are_friends()
        {
            var bob = new User.User();
            var alice = new User.User();
            bob.AddFriend(alice);
            tripService.LoggedUser().Returns(alice);
            tripService.When(x => x.FindTripsByUser(Arg.Any<User.User>()))
                .Do(x => { });

            tripService.GetTripsByUser(bob);

            tripService.Received().FindTripsByUser(Arg.Is(bob));
        }

    }
}
