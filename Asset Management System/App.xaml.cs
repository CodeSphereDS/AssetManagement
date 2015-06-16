using System.Windows;
using Catel.ApiCop;
using Catel.ApiCop.Listeners;
using Catel.IoC;
using Catel.Logging;
using Catel.Reflection;
using Catel.Windows;
using Catel.Services;
using System.Threading.Tasks;
using Catel.MVVM.Tasks;
using Catel.MVVM;
using Asset_Management_System.ViewModels.Windows;
using Asset_Management_System.Views.Windows;
using Asset_Management_System.ViewModels.File;
using Asset_Management_System.Views.File;
using Asset_Management_System.ViewModels.Flyouts;
using Asset_Management_System.Views.Flyouts;
using Asset_Management_System.ViewModels.Reports;
using Asset_Management_System.Views.Reports;
using Asset_Management_System.ViewModels;
using Asset_Management_System.Views;

namespace Asset_Management_System
{

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        protected override void OnStartup(StartupEventArgs e)
        {
#if DEBUG
            LogManager.AddDebugListener();
#endif


            ServiceLocator.Default.RegisterTypeIfNotYetRegistered<ISplashScreenService, SplashScreenService>(RegistrationType.Transient);
            var viewModelLocator = ServiceLocator.Default.ResolveType<IViewModelLocator>();
            var viewLocator = ServiceLocator.Default.ResolveType<IViewLocator>();

            viewModelLocator.Register(typeof(Views.Windows.CSSplashScreen), typeof(ViewModels.Windows.CSSplashScreenViewModel));
            viewLocator.Register(typeof(CSSplashScreenViewModel), typeof(CSSplashScreen));
            viewModelLocator.Register(typeof(MainWindow), typeof(MainWindowViewModel));
            viewLocator.Register(typeof(MainWindowViewModel), typeof(MainWindow));
            viewModelLocator.Register(typeof(AssetManagementSystemBlank), typeof(AssetManagementSystemBlankViewModel));
            viewLocator.Register(typeof(AssetManagementSystemBlankViewModel), typeof(AssetManagementSystemBlank));

            //viewLocator.Register(typeof(ViewModels.Adorner.CSAdornerViewModel), typeof(Views.Adorner.CSAdorner));


            Log.Info("Start of SplashScreen");
            var splashScreenService = ServiceLocator.Default.ResolveType<ISplashScreenService>();
            splashScreenService.Enqueue(new ActionTask("Preloading Assemblies", (x) => PreloadAssemblies(x)));
            splashScreenService.Enqueue(new ActionTask("Improving Application's Performance", (x) => ImprovePerformance(x)));
            splashScreenService.Enqueue(new ActionTask("Creating Style Helpers", x => CreateStyleHelpers(x)));
            splashScreenService.Enqueue(new ActionTask("Registering ViewModels", (x) => RegisterViewModels(x)));

            splashScreenService.CommitAsync<ViewModels.Windows.CSSplashScreenViewModel>();

            Log.Info("Calling base.OnStartup");

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            // Get advisory report in console
            ApiCopManager.AddListener(new ConsoleApiCopListener());
            ApiCopManager.WriteResults();
            base.OnExit(e);
        }

        private void PreloadAssemblies(ITaskProgressTracker tracker)
        {
            tracker.UpdateStatus("Preloading Assemblies", 25);
            Log.Info("Preloading Assemblies");
            System.AppDomain.CurrentDomain.PreloadAssemblies();
        }

        private void ImprovePerformance(ITaskProgressTracker tracker)
        {
            tracker.UpdateStatus("Improving performance", 50);
            Log.Info("Improving performance");
            Catel.Data.ModelBase.DefaultSuspendValidationValue = true;
            
            Catel.Windows.Controls.UserControl.DefaultCreateWarningAndErrorValidatorForViewModelValue = false;
            Catel.Windows.Controls.UserControl.DefaultSkipSearchingForInfoBarMessageControlValue = true;
        }

        private void CreateStyleHelpers(ITaskProgressTracker tracker)
        {
            tracker.UpdateStatus("Creating StyleHelpers", 75);
            Log.Info("Creating StyleHelpers");
            StyleHelper.CreateStyleForwardersForDefaultStyles();
        }

        private void RegisterViewModels(ITaskProgressTracker tracker)
        {
            tracker.UpdateStatus("Registering View Models", 100);
            Log.Info("Registering ViewModels");
            var viewLocator = ServiceLocator.Default.ResolveType<IViewLocator>();
            var viewModelLocator = ServiceLocator.Default.ResolveType<IViewModelLocator>();



            viewModelLocator.Register(typeof(About), typeof(AboutViewModel));
            viewLocator.Register(typeof(AboutViewModel), typeof(About));

            viewModelLocator.Register(typeof(Login), typeof(LoginViewModel));
            viewLocator.Register(typeof(LoginViewModel), typeof(Login));

            viewModelLocator.Register(typeof(CSSplashScreen), typeof(CSSplashScreenViewModel));
            viewLocator.Register(typeof(CSSplashScreenViewModel), typeof(CSSplashScreen));

            viewModelLocator.Register(typeof(AddNewUser), typeof(AddNewUserViewModel));
            viewLocator.Register(typeof(AddNewUserViewModel), typeof(AddNewUser));

            viewModelLocator.Register(typeof(AppSettings), typeof(AppSettingsViewModel));
            viewLocator.Register(typeof(AppSettingsViewModel), typeof(AppSettings));

            //viewModelLocator.Register(typeof(Views.Adorner.CSAdorner), typeof(ViewModels.Adorner.CSAdornerViewModel));
            //viewLocator.Register(typeof(ViewModels.Adorner.CSAdornerViewModel), typeof(Views.Adorner.CSAdorner));

            viewModelLocator.Register(typeof(SignOut), typeof(SignOutViewModel));
            viewLocator.Register(typeof(SignOutViewModel), typeof(SignOut));

            viewModelLocator.Register(typeof(AssetManagementSystemBlank), typeof(AssetManagementSystemBlankViewModel));
            viewLocator.Register(typeof(AssetManagementSystemBlankViewModel), typeof(AssetManagementSystemBlank));

            viewModelLocator.Register(typeof(AssetMasterList), typeof(AssetMasterListViewModel));
            viewLocator.Register(typeof(AssetMasterListViewModel), typeof(AssetMasterList));

            viewModelLocator.Register(typeof(AddEditAssetWindow), typeof(AddEditAssetWindowViewModel));
            viewLocator.Register(typeof(AddEditAssetWindowViewModel), typeof(AddEditAssetWindow));

            viewModelLocator.Register(typeof(AssetMasterListItem), typeof(AssetMasterListItemViewModel));
            viewLocator.Register(typeof(AssetMasterListItemViewModel), typeof(AssetMasterListItem));

            viewModelLocator.Register(typeof(test),typeof(testviewmodel));
            viewLocator.Register(typeof(testviewmodel),typeof(test));

            viewModelLocator.Register(typeof(test1),typeof(test1viewmodel));
            viewLocator.Register(typeof(test1viewmodel),typeof(test1));

            viewModelLocator.Register(typeof(AddEditCategory), typeof(AddEditCategoryViewModel));
            viewLocator.Register(typeof(AddEditCategoryViewModel), typeof(AddEditCategory));

        }
    }
}