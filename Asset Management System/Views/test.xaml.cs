namespace Asset_Management_System.Views
{
    using Catel.Windows;
    using ViewModels;

    public partial class test : DataWindow
    {
        public test()
            : this(null) { }

        public test(testviewmodel viewModel)
            : base(viewModel)
        {
            InitializeComponent();
        }
    }
}
