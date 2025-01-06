using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using BCrypt.Net;
using Cosmos.Model;
using Cosmos.Data;   // <-- Where your CosmosDataContext is

namespace Cosmos.Repositories
{
    public class UserRepository : IUserRepository
    {
        // 1) Authenticate user by comparing plain-text password with stored BCrypt hash
        public bool AuthenticateUser(NetworkCredential credential)
        {
            // Provide a connection string or rely on the parameterless constructor
            // if you enabled it in the .dbml designer properties
            using (var db = new CosmosDataContext())
            {
                // 1a) Find the user record by username
                var userEntity = db.Users.FirstOrDefault(u => u.Username == credential.UserName);

                // 1b) If no user found, authentication fails
                if (userEntity == null)
                    return false;

                // 1c) Compare the entered plaintext password with the stored BCrypt hash
                bool isValid = BCrypt.Net.BCrypt.Verify(credential.Password, userEntity.PasswordHash);

                return isValid;
            }
        }





        // 2) Create a new user
        public void Add(UserModel userModel)
        {
            using (var db = new CosmosDataContext())
            {
                // Convert UserModel to DB entity
                var newUser = new User
                {
                    Username = userModel.Username,
                    // Hash the plaintext password
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(userModel.Password),
                    RoleId = userModel.RoleId ?? default(int) // Handle nullable RoleId
                };

                db.Users.InsertOnSubmit(newUser);
                db.SubmitChanges();
            }
        }


        // 3) Update existing user
        public void Edit(UserModel userModel)
        {
            using (var db = new CosmosDataContext())
            {
                int userId = int.Parse(userModel.Id);

                var existingUser = db.Users.FirstOrDefault(u => u.UserId == userId);
                if (existingUser == null)
                    return;

                existingUser.Username = userModel.Username;

                // If a new password was provided, re-hash it
                if (!string.IsNullOrWhiteSpace(userModel.Password))
                {
                    existingUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userModel.Password);
                }

                // Update role if needed
                existingUser.RoleId = userModel.RoleId ?? default(int);

                db.SubmitChanges();
            }
        }


        // 4) Remove a user by ID
        public void Remove(int id)
        {
            using (var db = new CosmosDataContext())
            {
                var userEntity = db.Users.FirstOrDefault(u => u.UserId == id);
                if (userEntity == null)
                    return;

                db.Users.DeleteOnSubmit(userEntity);
                db.SubmitChanges();
            }
        }

        // 5) Get a user by ID
        public UserModel GetById(int id)
        {
            using (var db = new CosmosDataContext())
            {
                var userEntity = db.Users.FirstOrDefault(u => u.UserId == id);
                if (userEntity == null)
                    return null;

                // Map DB entity to UserModel
                return new UserModel
                {
                    Id = userEntity.UserId.ToString(),
                    Username = userEntity.Username,
                    Password = string.Empty,
                    RoleId = userEntity.RoleId,
                    RoleName = userEntity.Role?.RoleName
                };
            }
        }

        // 6) Get a user by username
        public UserModel GetByUsername(string username)
        {
            using (var db = new CosmosDataContext())
            {
                var userEntity = db.Users.FirstOrDefault(u => u.Username == username);
                if (userEntity == null) return null;

                var roleEntity = userEntity.Role; // Legătura cu tabela Role
                return new UserModel
                {
                    Id = userEntity.UserId.ToString(),
                    Username = userEntity.Username,
                    Password = string.Empty, // Omitem hash-ul pentru securitate
                    RoleId = userEntity.RoleId, // Nu mai facem conversie aici, deoarece RoleId e int?
                    RoleName = roleEntity?.RoleName, // Null-safe pentru RoleName
                    Role = roleEntity == null ? null : new RoleModel
                    {
                        RoleId = roleEntity.RoleId,
                        RoleName = roleEntity.RoleName
                    }
                };
            }
        }






        // 7) Get all users
        public IEnumerable<UserModel> GetAll()
        {
            using (var db = new CosmosDataContext())
            {
                var allUsers = db.Users
                                 .Select(u => new UserModel
                                 {
                                     Id = u.UserId.ToString(),
                                     Username = u.Username,
                                     Password = string.Empty,
                                     RoleId = u.RoleId,
                                     RoleName = u.Role.RoleName
                                 })
                                 .ToList();

                return allUsers;
            }
        }
    }
}
