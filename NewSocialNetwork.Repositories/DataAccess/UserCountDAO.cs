﻿using NewSocialNetwork.Entities;
using NewSocialNetwork.Repositories;

namespace NewSocialNetwork.DataAccess
{
    public class UserCountDAO : DAO<UserCount>, IUserCountRepository
    {
        public UserCountDAO() { }
    }
}