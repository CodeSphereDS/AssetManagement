using Catel.Data;
using Catel.MVVM;
using CodeSphere.CSClasses;
using Asset_Management_System.ViewModels.Windows;

namespace Asset_Management_System.Views.Windows 
    
{
    public partial class About : CSWindow
    {
        public About()
            :base(new AboutViewModel())
        {
            InitializeComponent();             
        }
        
             
    }
}
