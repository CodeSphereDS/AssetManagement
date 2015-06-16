using Catel.Windows;
using Asset_Management_System.ViewModels.Reports;
using CodeSphere.CSClasses;


namespace Asset_Management_System.Views.Reports
{
  
    public partial class AddEditCategory : CSWindow
    {
        public AddEditCategory()
            : this(null) { }

        public AddEditCategory(AddEditCategoryViewModel viewModel)
            : base(viewModel)
        {
            InitializeComponent();
        }
    }
}
