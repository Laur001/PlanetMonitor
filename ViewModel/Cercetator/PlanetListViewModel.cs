using Cosmos.Data;
using System.Collections.ObjectModel;
using System.Linq;
using YourProject.ViewModels;

namespace Cosmos.ViewModel.Cercetator
{
    public class PlanetListViewModel : BaseViewModel
    {
        public ObservableCollection<CelestialBody> Planets { get; set; }

        public PlanetListViewModel()
        {
            LoadPlanets();
        }

        private void LoadPlanets()
        {
            using (var db = new CosmosDataContext())
            {
                Planets = new ObservableCollection<CelestialBody>(
                    db.CelestialBodies.Where(cb => cb.Type == "Planet").ToList()
                );
            }
        }


    }
}
