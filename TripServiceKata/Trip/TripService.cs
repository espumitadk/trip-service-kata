using System.Collections.Generic;
using TripServiceKata.Exception;
using TripServiceKata.User;

namespace TripServiceKata.Trip
{
    public class TripService
    {
        public List<Trip> GetTripsByUser(User.User user)
        {
            var loggedUser = LoggedUser();
            CheckIfUserIsLogged(loggedUser);
            return user.Friends().Contains(loggedUser) ?
                FindTripsByUser(user) : new List<Trip>();
        }

        private static void CheckIfUserIsLogged(User.User loggedUser)
        {
            if (loggedUser == null) throw new UserNotLoggedInException();
        }

        public virtual List<Trip> FindTripsByUser(User.User user)
        {
            return TripDAO.FindTripsByUser(user);
        }

        public virtual User.User LoggedUser()
        {
            return UserSession.GetInstance().GetLoggedUser();
        }
    }
}
