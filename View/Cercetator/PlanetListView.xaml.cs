using System.Windows.Controls;

namespace Cosmos.View.Cercetator
{
    public partial class PlanetListView : UserControl
    {
        public PlanetListView()
        {
            InitializeComponent();
            DataContext = new ViewModel.Cercetator.PlanetListViewModel();
        }
    }
}
