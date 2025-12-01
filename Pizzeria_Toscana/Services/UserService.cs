using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Pizzeria_Toscana.Models;
using Pizzeria_Toscana.Repositories;
using Pizzeria_Toscana.Repositories.Interfaces;
using Pizzeria_Toscana.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pizzeria_Toscana.Services
{
    public class UserService : IUserService
    {  
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccesor;


        public UserService(IRepositoryWrapper repositoryWrapper, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _repositoryWrapper = repositoryWrapper;
            _userManager = userManager;
            _httpContextAccesor = httpContextAccessor;
        }
        public User GetCurrentUser()
        {
            var userId = _userManager.GetUserId(_httpContextAccesor.HttpContext.User);
            if (userId.IsNullOrEmpty())
            {
                return null;
            }
            return (User)_userManager.FindByIdAsync(userId).Result;
        }
        public List<User> GetAllUsers()
        {
            return _repositoryWrapper.UserRepository.FindAll().ToList();
        }

        public User GetUserById(int idUser)
        {
            return _repositoryWrapper.UserRepository.FindByCondition(u => u.Id.Equals(idUser)).FirstOrDefault();
        }

        public void AddUser(User user)
        {
            _repositoryWrapper.UserRepository.Create(user);
            _repositoryWrapper.Save();
        }

        public void UpdateUser(User user)
        {
            _repositoryWrapper.UserRepository.Update(user);
            _repositoryWrapper.Save();
        }

        public void DeleteUser(int idUser)
        {
            var user = _repositoryWrapper.UserRepository.FindByCondition(u => u.Id.Equals(idUser)).FirstOrDefault();
            if (user != null)
            {
                _repositoryWrapper.UserRepository.Delete(user);
                _repositoryWrapper.Save();
            }
            else
            {
                throw new ArgumentException($"User with ID {idUser} not found.");
            }
        }

    }
}