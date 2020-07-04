using System;
using System.Collections.Generic;
using URLCatalogue.API.Model.View;

namespace URLCatalogue.API.Service
{
    public interface IUserManagement
    {
        string AddUser(UserDetail item);
        string EditUser(UserDetail item);
        void DeleteUser(string id);
        IEnumerable<UserDetail> GetAllUsers();
        string CheckExists(string email);
        IEnumerable<UserDetail> GetActiveUsers();
        void ResetGlobals();
    }
}
