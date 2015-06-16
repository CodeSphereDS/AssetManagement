using CodeSphere.CSClasses;
using Asset_Management_System.Views.Windows;

namespace Asset_Management_System.Views
{
    public partial class MainWindow : CSWindow
    {
        public MainWindow()
            : base(null) { }
        public MainWindow(ViewModels.MainWindowViewModel viewModel)
            :base(viewModel)
        {
            
            InitializeComponent();
        }
        
    }
}
