﻿using System.Collections.Generic;
using TripServiceKata.Exception;
using TripServiceKata.User;

namespace TripServiceKata.Trip
{
    public class TripService
    {
        public List<Trip> GetTripsByUser(User.User user)
        {
            User.User loggedUser = GetLoggedUser();
            if (loggedUser == null) throw new UserNotLoggedInException();
            if (user.GetFriends().Contains(loggedUser))
            {
                return FindTripsByUser(user);
            }
            return new List<Trip>();
        }

        public virtual List<Trip> FindTripsByUser(User.User user)
        {
            return TripDAO.FindTripsByUser(user);
        }

        public virtual User.User GetLoggedUser()
        {
            return UserSession.GetInstance().GetLoggedUser();
        }
    }
}
