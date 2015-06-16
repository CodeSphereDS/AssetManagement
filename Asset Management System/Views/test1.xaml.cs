namespace Asset_Management_System.Views
{
    using Catel.Windows;
    using ViewModels;

    public partial class test1 : DataWindow
    {
        public test1()
            : this(null) { }

        public test1(test1viewmodel viewModel)
            : base(viewModel)
        {
            InitializeComponent();
        }
    }
}
