using Cosmos.View.Cercetator;
using System.Windows.Input;
using YourProject.ViewModels;

namespace Cosmos.ViewModel.Cercetator
{
    public class CercetatorViewModel : BaseViewModel
    {
        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public ICommand ShowSolarSystemCommand { get; }
        public ICommand ShowPlanetListCommand { get; }

        public CercetatorViewModel()
        {
            ShowSolarSystemCommand = new RelayCommand(_ => ShowSolarSystem());
            ShowPlanetListCommand = new RelayCommand(_ => ShowPlanetList());

            ShowSolarSystem(); // Default view
        }

        private void ShowSolarSystem()
        {
            CurrentView = new SolarSystemView();
        }

        private void ShowPlanetList()
        {
            CurrentView = new PlanetListView();
        }
    }
}
