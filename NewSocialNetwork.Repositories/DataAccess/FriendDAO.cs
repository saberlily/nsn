﻿using NewSocialNetwork.Entities;
using NewSocialNetwork.Repositories;

namespace NewSocialNetwork.DataAccess
{
    public class FriendDAO : DAO<Friend>, IFriendRepository
    {
        public FriendDAO() { }
    }
}