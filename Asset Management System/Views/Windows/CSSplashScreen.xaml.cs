using Asset_Management_System.ViewModels.Windows;

namespace Asset_Management_System.Views.Windows
{
    public partial class CSSplashScreen : CodeSphere.CSClasses.CSWindow
    {
        public CSSplashScreen(CSSplashScreenViewModel viewmodel)
            :base(viewmodel)
            
        {
            InitializeComponent();
        }
    }
}
