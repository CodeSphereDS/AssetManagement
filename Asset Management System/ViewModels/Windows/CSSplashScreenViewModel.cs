using Catel.MVVM;
using System.Threading.Tasks;
using Catel.Data;
using System;

namespace Asset_Management_System.ViewModels.Windows
{
    

    public class CSSplashScreenViewModel  : ProgressNotifyableViewModel
    {
        public CSSplashScreenViewModel()           
        {           
        }
        
        protected override async Task Initialize()
        {
            await base.Initialize();           
        }

        protected override async Task Close()
        {          
            await base.Close();
        }

    }
}
