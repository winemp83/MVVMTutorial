using System.Windows;
using System.Windows.Controls;
using ViewModel;

namespace StartApplication
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetupBindings();
        }

        private void SetupBindings()
        {
            var viewModel = new PersonListViewModel();
            personListView.DataContext = viewModel;
        }
        

    }
}
