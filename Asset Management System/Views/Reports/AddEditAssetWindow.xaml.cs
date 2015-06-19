using Catel.Windows;
using Asset_Management_System.ViewModels.Reports;

namespace Asset_Management_System.Views.Reports
{
   

    public partial class AddEditAssetWindow
    {
        public AddEditAssetWindow()
            : this(null) { }

        public AddEditAssetWindow(AddEditAssetWindowViewModel viewModel)
            : base(viewModel)
        {
            InitializeComponent();
        }
    }
}
