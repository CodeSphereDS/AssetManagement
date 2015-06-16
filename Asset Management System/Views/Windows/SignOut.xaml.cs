using Catel.Windows;
using Asset_Management_System.ViewModels.Windows;

namespace Asset_Management_System.Views.Windows
{
    public partial class SignOut
    {
        public SignOut()
            : this(null) { }

        public SignOut(SignOutViewModel viewModel)
            : base(viewModel)
        {
            InitializeComponent();
        }
    }
}
