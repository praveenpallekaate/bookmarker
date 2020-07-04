using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using URLCatalogue.API.Data;
using URLCatalogue.API.Model;
using URLCatalogue.API.Model.Data;
using URLCatalogue.API.Model.View;

namespace URLCatalogue.API.Service
{
    public class UserManagement : IUserManagement
    {
        private readonly IRepository<User> _userRepository = null;
        private IEnumerable<UserDetail> users = null;

        public UserManagement(IOptions<AppConfig> appConfigs)
        {
            _userRepository = new UserRepository(appConfigs);
        }

        public string AddUser(UserDetail item)
        {
            Guid userId = Guid.NewGuid();

            _userRepository.CreateItem(new User
            {
                Email = item.Email,
                Id = userId,
                IsActive = true
            });

            ResetGlobals();

            return userId.ToString();
        }

        public void DeleteUser(string id)
        {
            var user = _userRepository.GetItem(i => i.Id == new Guid(id));
            user.IsActive = false;

            _userRepository.UpdateItem(j => j.Id == new Guid(id), user);

            ResetGlobals();
        }

        public string EditUser(UserDetail item)
        {
            var user = _userRepository.GetItem(i => i.Id == item.Id);
            user.Email = item.Email;
            
            _userRepository.UpdateItem(j => j.Id == item.Id, user);

            ResetGlobals();

            return item.Id.ToString();
        }

        public IEnumerable<UserDetail> GetAllUsers()
        {
            return _userRepository.GetAllItems()
                .Select(i =>
                {
                    return new UserDetail
                    {
                        Email = i.Email,
                        Id = i.Id,
                        IsActive = i.IsActive
                    };
                });
        }

        public IEnumerable<UserDetail> GetActiveUsers()
        {
            return GetAllUsers().Where(i => i.IsActive);
        }

        public string CheckExists(string email)
        {
            var user = GetUsers()?.FirstOrDefault(i => i.Email.ToLower() == email.ToLower());

            return user?.Id.ToString();
        }

        public void ResetGlobals()
        {
            users = null;
        }

        private IEnumerable<UserDetail> GetUsers()
        {
            if (users == null) users = GetActiveUsers();

            return users;
        }
    }
}
