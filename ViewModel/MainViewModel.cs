using Cosmos.Model;
using Cosmos.Repositories;
using Cosmos.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Cosmos.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        // Fields
        private UserAccountModel _currentUserAccount;
        private IUserRepository userRepository;

        public UserAccountModel CurrentUserAccount
        {
            get => _currentUserAccount;
            set
            {
                _currentUserAccount = value;
                OnPropertyChanged(nameof(CurrentUserAccount));
            }
        }

        public MainViewModel()
        {
            userRepository = new UserRepository();
            CurrentUserAccount = new UserAccountModel();
            LoadCurrentUserData();
        }

        private void LoadCurrentUserData()
        {
            // Get the user by username from the repository
            var user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);

            if (user != null)
            {
                // Map the UserModel properties to UserAccountModel properties
                CurrentUserAccount.Username = user.Username;
                CurrentUserAccount.DisplayName = $"Welcome {user.Username}!"; // Modify this if "Name" and "LastName" properties are added to UserModel.
                CurrentUserAccount.ProfilePicture = null;

                // Display role information if available
                if (user.Role != null)
                {
                    CurrentUserAccount.DisplayName += $" (Role: {user.Role.RoleName})";
                }
            }
            else
            {
                // Handle invalid user scenario
                CurrentUserAccount.DisplayName = "Invalid user, not logged in";
                // Hide child views, if necessary.
            }
        }
    }
}
