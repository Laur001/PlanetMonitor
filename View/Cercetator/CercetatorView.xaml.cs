using System.Windows.Controls;
using Cosmos.ViewModel.Cercetator; // Correct namespace for CercetatorViewModel

namespace Cosmos.View.Cercetator
{
    public partial class CercetatorView : UserControl
    {
        public CercetatorView()
        {
            InitializeComponent();
            DataContext = new CercetatorViewModel(); // Bind the ViewModel
        }
    }
}
