using Catel.Windows;
using Asset_Management_System.ViewModels.Windows;

namespace Asset_Management_System.Views.Windows
{
    

    public partial class ConfirmWindow
    {
        public ConfirmWindow()
            : this(null) { }

        public ConfirmWindow(ConfirmWindowViewModel viewModel)
            : base(viewModel)
        {
            InitializeComponent();
        }
    }
}
