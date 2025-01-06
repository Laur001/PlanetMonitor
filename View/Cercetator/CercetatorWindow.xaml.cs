using System.Windows;
using System.Windows.Input;
using Cosmos.ViewModel.Cercetator; // Ensure the namespace matches your project

namespace Cosmos.View.Cercetator
{
    public partial class CercetatorWindow : Window
    {
        public CercetatorWindow()
        {
            InitializeComponent();
            DataContext = new CercetatorViewModel();
        }

        // Close Button Click Event
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Minimize Button Click Event
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        // Allow Window Dragging
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
    }
}
