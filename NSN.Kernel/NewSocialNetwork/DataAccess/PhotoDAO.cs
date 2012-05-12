﻿using System.Collections.Generic;
using NewSocialNetwork.Domain;
using NewSocialNetwork.Repositories;
using NHibernate;

namespace NewSocialNetwork.DataAccess
{
    public class PhotoDAO : DAO<Photo>, IPhotoRepository
    {
        public PhotoDAO(ISessionFactory sessionFactory) : base(sessionFactory)
        { }

        public IList<Photo> GetPhotosByUser(int userId)
        {
            return this.Session()
                .CreateQuery("from Photo p where p.User.UserId = :userId")
                .SetInt32("userId", userId)
                .List<Photo>();
        }
    }
}