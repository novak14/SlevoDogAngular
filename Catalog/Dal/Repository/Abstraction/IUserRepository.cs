using Catalog.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Dal.Repository.Abstraction
{
    public interface IUserRepository
    {
        User GetUserByCookie(string cookie);
        string InsertUser(string username, string uniqueString);
    }
}
