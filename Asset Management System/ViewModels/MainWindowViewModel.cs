using Catel.MVVM;
using Catel.Data;
using Catel.IoC;
using Catel.Services;
using System.Threading;
using System.Threading.Tasks;
using Catel.MVVM.Tasks;
using Asset_Management_System.ViewModels;
using Asset_Management_System.ViewModels.Reports;
using Data.Model;
using Catel.Messaging;

namespace Asset_Management_System.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        //private Flyouts.LoginViewModel _loginViewModel;
        //private Flyouts.AddNewUserViewModel _addNewUserViewModel;
        //private Flyouts.AppSettingsViewModel _appSettingsViewModel;
        private Reports.AssetMasterListViewModel _AssetMasterListViewModel;
        private IMessageMediator _messagemediator;
        private IMessageService _messageservice;
        private IUIVisualizerService _uivisualizer;

        #region constructor
        public MainWindowViewModel()
            : base(false, false, false)          
            
        {
            CommandShowLogin = new Command(OnCommandShowLoginExecute, OnCommandShowLoginCanExecute);
            CommandOpenSettings = new Command(OnCommandOpenSettingsExecute, OnCommandOpenSettingsCanExecute);
            CommandShowAbout = new Command(OnCommandShowAboutExecute, OnCommandShowAboutCanExecute);
            CommandAddNewUser = new Command(OnCommandAddNewUserExecute, OnCommandAddNewUserCanExecute);
            CommandCloseFlyout = new Command(OnCommandCloseFlyoutExecute, OnCommandCloseFlyoutCanExecute);
            CommandRefreshMainContent = new Command(OnCommandRefreshMainContentExecute, OnCommandRefreshMainContentCanExecute);
            CommandShowAssetList = new Command(OnCommandShowAssetListExecute, OnCommandShowAssetListCanExecute);

            CommandCloseWindow = new Command(OnCommandCloseWindowExecute);

            var dependencyResolver = this.GetDependencyResolver();
            _messageservice= dependencyResolver.Resolve<IMessageService>();
            _messagemediator = dependencyResolver.Resolve<IMessageMediator>();
            _uivisualizer = dependencyResolver.Resolve<IUIVisualizerService>();

            _messagemediator.Register<bool>(this, MainWindowIsBusy, "MainWindowIsBusy");
            
            MainContent = new Reports.AssetManagementSystemBlankViewModel();
        }

        #endregion constructor

        #region Properties
        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        [Model]
        public User User
        {
            get { return GetValue<User>(UserProperty); }
            private set { SetValue(UserProperty, value); }
        }

        /// <summary>
        /// Register the User property so it is known in the class.
        /// </summary>
        public static readonly PropertyData UserProperty = RegisterProperty("User", typeof(User));

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        [ViewModelToModel("User")]
        public string Username
        {
            get { return GetValue<string>(UsernameProperty); }
            set { SetValue(UsernameProperty, value); }
        }

        /// <summary>
        /// Register the Username property so it is known in the class.
        /// </summary>
        public static readonly PropertyData UsernameProperty = RegisterProperty("Username", typeof(string));

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        [ViewModelToModel("User")]
        public bool UserIsAuthenticated
        {
            get { return GetValue<bool>(UserIsAuthenticatedProperty); }
            set { SetValue(UserIsAuthenticatedProperty, value); }
        }

        /// <summary>
        /// Register the UserIsAuthenticated property so it is known in the class.
        /// </summary>
        public static readonly PropertyData UserIsAuthenticatedProperty = RegisterProperty("UserIsAuthenticated", typeof(bool));

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        [ViewModelToModel("User")]
        public string Password
        {
            get { return GetValue<string>(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        /// <summary>
        /// Register the Password property so it is known in the class.
        /// </summary>
        public static readonly PropertyData PasswordProperty = RegisterProperty("Password", typeof(string));

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        public ViewModelBase MainContent
        {
            get { return GetValue<ViewModelBase>(MainContentProperty); }
            set { SetValue(MainContentProperty, value); }
        }

        /// <summary>
        /// Register the MainContent property so it is known in the class.
        /// </summary>
        public static readonly PropertyData MainContentProperty = RegisterProperty("MainContent", typeof(ViewModelBase), null, (sender, e) => ((MainWindowViewModel)sender).OnMainContentChanged());

        /// <summary>
        /// Called when the MainContent property has changed.
        /// </summary>
        private void OnMainContentChanged()
        {
            // TODO: Implement logic          
        }

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        public IViewModel FlyoutContent
        {
            get { return GetValue<IViewModel>(FlyoutContentProperty); }
            set { SetValue(FlyoutContentProperty, value); }
        }

        /// <summary>
        /// Register the FlyoutContent property so it is known in the class.
        /// </summary>
        public static readonly PropertyData FlyoutContentProperty = RegisterProperty("FlyoutContent", typeof(IViewModel), null, (sender, e) => ((MainWindowViewModel)sender).OnFlyoutContentChanged());

        /// <summary>
        /// Called when the FlyoutContent property has changed.
        /// </summary>
        private void OnFlyoutContentChanged()
        {
            // TODO: Implement logic
        }

        public override string Title { get { return "RAK & ACO Asset Management System"; } }
        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        public bool ShowFlyout
        {
            get { return GetValue<bool>(ShowFlyoutProperty); }
            set { SetValue(ShowFlyoutProperty, value); }
        }

        /// <summary>
        /// Register the ShowFlyout property so it is known in the class.
        /// </summary>
        public static readonly PropertyData ShowFlyoutProperty = RegisterProperty("ShowFlyout", typeof(bool), null, (sender, e) => ((MainWindowViewModel)sender).OnShowFlyoutChanged());

        /// <summary>
        /// Called when the ShowFlyout property has changed.
        /// </summary>
        private void OnShowFlyoutChanged()
        {
            // TODO: Implement logic
        }

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        public bool IsBusy
        {
            get { return GetValue<bool>(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }

        /// <summary>
        /// Register the IsBusy property so it is known in the class.
        /// </summary>
        public static readonly PropertyData IsBusyProperty = RegisterProperty("IsBusy", typeof(bool), null);

        #endregion Properties

        #region Commands


        /// <summary>
        /// Gets the  CommandShowAssetList command.
        /// </summary>
        public Command CommandShowAssetList { get; private set; }
      

        /// <summary>
        /// Method to check whether the  CommandShowAssetList command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnCommandShowAssetListCanExecute()
        {
            return true;
        }

        /// <summary>
        /// Method to invoke when the  CommandShowAssetList command is executed.
        /// </summary>
        private void OnCommandShowAssetListExecute()
        {
            if (_AssetMasterListViewModel == null)
            {
                _AssetMasterListViewModel = new AssetMasterListViewModel();
            }
            MainContent = _AssetMasterListViewModel;
        }

        /// <summary>
        /// Gets the CommandRefreshMainContent command.
        /// </summary>
        public Command CommandRefreshMainContent { get; private set; }

        // TODO: Move code below to constructor

        // TODO: Move code above to constructor

        /// <summary>
        /// Method to check whether the CommandRefreshMainContent command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnCommandRefreshMainContentCanExecute()
        {
            return !IsBusy;
        }

        /// <summary>
        /// Method to invoke when the CommandRefreshMainContent command is executed.
        /// </summary>
        private void OnCommandRefreshMainContentExecute()
        {
            //System.ComponentModel.BackgroundWorker worker = new System.ComponentModel.BackgroundWorker();

            //worker.DoWork+=((o,ea) =>
            //    {
            //        System.Threading.Thread.Sleep(5000);

            //    });
            //worker.RunWorkerCompleted += ((o,ea) =>
            //    {
            //        IsBusy = false;
            //    });
            //IsBusy = true;
            //worker.RunWorkerAsync();                
        }

        /// <summary>
        /// Gets the CommandCloseFlyout command.
        /// </summary>
        public Command CommandCloseFlyout { get; private set; }

        /// <summary>
        /// Method to check whether the CommandCloseFlyout command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnCommandCloseFlyoutCanExecute()
        {
            return true;
        }

        /// <summary>
        /// Method to invoke when the CommandCloseFlyout command is executed.
        /// </summary>
        private async void OnCommandCloseFlyoutExecute()
        {
            //System.Diagnostics.Debug.WriteLine(string.Format("flyout closed result = {0}", FlyoutContent.CancelAndCloseViewModel()));
            ShowFlyout = !await FlyoutContent.CancelAndCloseViewModel();

        }

        /// <summary>
        /// Gets the CommandShowLogin command.
        /// </summary>
        public Command CommandShowLogin { get; private set; }

        /// <summary>
        /// Method to check whether the CommandShowLogin command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnCommandShowLoginCanExecute()
        {
            return true;
        }

        /// <summary>
        /// Method to invoke when the CommandShowLogin command is executed.
        /// </summary>     
        private void OnCommandShowLoginExecute()
        {
            if (User == null) User = new User { Firstname = "", Lastname = "", Password = "" };
            if (UserIsAuthenticated)
            {
                var viewModel = new Windows.SignOutViewModel();
                var dependencyResolver = this.GetDependencyResolver();
                var uiVisualizerService = dependencyResolver.Resolve<IUIVisualizerService>();
                UserIsAuthenticated = (bool)!uiVisualizerService.ShowDialog(viewModel);
                if (!UserIsAuthenticated) User = null;
            }
            else if (!UserIsAuthenticated)
            {
                ShowFlyout = false;
                System.Diagnostics.Debug.WriteLine(string.Format("User is Authenticated = {0}", User.UserIsAuthenticated.ToString()));
                var _loginViewModel = new Flyouts.LoginViewModel(User);
                FlyoutContent = _loginViewModel;
                ShowFlyout = true;
            }

        }
        /// <summary>
        /// Gets the CommandOpenSettings command.
        /// </summary>
        public Command CommandOpenSettings { get; private set; }

        // TODO: Move code below to constructor

        // TODO: Move code above to constructor

        /// <summary>
        /// Method to check whether the CommandOpenSettings command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnCommandOpenSettingsCanExecute()
        {
            return true;

        }

        /// <summary>
        /// Method to invoke when the CommandOpenSettings command is executed.
        /// </summary>
        private void OnCommandOpenSettingsExecute()
        {
            //var s = Data.Session.NHibernateSession.DbFile;
            //Data.Session.NHibernateSession.test();
            ShowFlyout = false;

            var _appSettingsViewModel = new Flyouts.AppSettingsViewModel();
            FlyoutContent = _appSettingsViewModel;
            ShowFlyout = true;

        }

        /// <summary>
        /// Gets the CommandShowAbout command.
        /// </summary>
        public Command CommandShowAbout { get; private set; }

        // TODO: Move code below to constructor

        // TODO: Move code above to constructor

        /// <summary>
        /// Method to check whether the CommandShowAbout command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnCommandShowAboutCanExecute()
        {
            return true;
        }

        /// <summary>
        /// Method to invoke when the CommandShowAbout command is executed.
        /// </summary>
        private void OnCommandShowAboutExecute()
        {
            var viewModel = new Windows.AboutViewModel();
            var dependencyResolver = this.GetDependencyResolver();
            var uiVisualizerService = dependencyResolver.Resolve<IUIVisualizerService>();
            if (!uiVisualizerService.IsRegistered(typeof(ViewModels.Windows.AboutViewModel)))
            {
                uiVisualizerService.Register(typeof(ViewModels.Windows.AboutViewModel), typeof(Views.Windows.About));
            }

            uiVisualizerService.ShowDialog(viewModel);
        }
        /// <summary>
        /// Gets the CommandAddNewUser command.
        /// </summary>
        public Command CommandAddNewUser { get; private set; }

        // TODO: Move code below to constructor

        // TODO: Move code above to constructor

        /// <summary>
        /// Method to check whether the CommandAddNewUser command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnCommandAddNewUserCanExecute()
        {
            return true;
        }

        /// <summary>
        /// Method to invoke when the CommandAddNewUser command is executed.
        /// </summary>
        private void OnCommandAddNewUserExecute()
        {
            ShowFlyout = false;

            var _addNewUserViewModel = new Flyouts.AddNewUserViewModel();

            FlyoutContent = _addNewUserViewModel;
            ShowFlyout = true;
        }
      

        /// <summary>
/// Gets the CommandCloseWindow command.
/// </summary>
public Command CommandCloseWindow { get; private set; }

/// <summary>
/// Method to invoke when the CommandCloseWindow command is executed.
/// </summary>
private async void OnCommandCloseWindowExecute()
{
   
    //if (await _messageservice.Show("Are you sure you want to do this?", "Are you sure?", MessageButton.YesNo) == MessageResult.Yes)
    //{
    //    this.CloseViewModel(true);
    //}

    if(await _uivisualizer.ShowDialogAsync<ViewModels.Windows.ConfirmWindowViewModel>()==true)
    {
        await this.Close();
    }
   
}
        #endregion Commands

        #region Methods
        protected override async Task Initialize()
        {
            await base.Initialize();

            // TODO: subscribe to events here
        }

        protected override async Task Close()
        {
         
            await base.Close();
        }

        public void LoadContent(IViewModel Content)
        {
            var r = this.GetDependencyResolver();
            var c = r.Resolve(Content.GetType());
            System.Diagnostics.Debug.WriteLine(c.GetType());
        }

        [MessageRecipient(Tag = "MainWindowIsBusy")]
        public void MainWindowIsBusy(bool busy)
        {
                  
            IsBusy = busy;
        }
        #endregion Methods    
    }
}
