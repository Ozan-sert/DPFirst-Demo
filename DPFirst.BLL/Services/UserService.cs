using DPFirst.BLL.ViewModel;
using DPFirst.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPFirst.BLL.Services
{
    public class UserService
    {
        private UsersEntities db = new UsersEntities();

        public UserService() { }
    
        // Method to get all users as UserViewModel
        public IEnumerable<UserViewModel> GetAllUsers()
        {
            var users = db.NewUsers.ToList();
            
            var userViewModels = new List<UserViewModel>();
            foreach (var user in users)
            {
                userViewModels.Add(new UserViewModel
                {
                    ID = user.ID,
                    Username = user.Username,
                    Email = user.Email,
                    Password = user.Password
                });
            }
            return userViewModels;
        }
        // Method to get specific user as UserViewModel
        public UserViewModel GetUserById(int? id)
        {
            NewUser user = db.NewUsers.Find(id);
            if (user == null)
            {
                return null;
            }

            var userViewModel = new UserViewModel
            {
                ID = user.ID,
                Username = user.Username,
                Email = user.Email,
                Password = user.Password
            };

            return userViewModel;
        }

        // Method to create a new user
        public void CreateUser(UserViewModel userViewModel)
        {
            // Map the UserViewModel to NewUser (database model)
            var newUser = new NewUser
            {
                ID = userViewModel.ID,
                Username = userViewModel.Username,
                Email = userViewModel.Email,
                Password = userViewModel.Password
            };

           db.NewUsers.Add(newUser);
            db.SaveChanges();
        }

        // Method to update an existing user
        public void UpdateUser(UserViewModel userViewModel)
        {
            // Map the UserViewModel to NewUser (database model)
            var updatedUser = new NewUser
            {
                ID = userViewModel.ID,
                Username = userViewModel.Username,
                Email = userViewModel.Email,
                Password = userViewModel.Password
            };

            db.Entry(updatedUser).State = EntityState.Modified;
            db.SaveChanges();
        }


      public void DeleteUser(int id)
        {
            NewUser newUser = db.NewUsers.Find(id);
            db.NewUsers.Remove(newUser);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}

