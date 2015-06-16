using Catel.MVVM;
using System.Threading.Tasks;
using Catel.Data;
using System.Windows;
using Catel.IoC;
using Catel.Services;
using Data.Model;
using Data.Session;
using Data.Mapping;
using NHibernate.Criterion;
using System.Linq;
using System.ComponentModel;


namespace Asset_Management_System.ViewModels.Flyouts
{
    public class LoginViewModel : ViewModelBase
    {
        //public MainWindowViewModel _MainWindowViewModel;
        public LoginViewModel(User user)           
        {
            User = user;
            if (!User.UserIsAuthenticated) Title = "Login";

            System.Diagnostics.Debug.WriteLine(string.Format("value of user is {0}",user.Username));
            CommandLogin = new Command(OnCommandLoginExecute, OnCommandLoginCanExecute);          
            CommandAddNewUser = new Command(OnCommandAddNewUserExecute, OnCommandAddNewUserCanExecute);

            
          
        }
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
        public bool IsBusy
        {
            get { return GetValue<bool>(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }

        /// <summary>
        /// Register the IsBusy property so it is known in the class.
        /// </summary>
        public static readonly PropertyData IsBusyProperty = RegisterProperty("IsBusy", typeof(bool), null);

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        public string Title
        {
            get { return GetValue<string>(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        /// <summary>
        /// Register the Title property so it is known in the class.
        /// </summary>
        public static readonly PropertyData TitleProperty = RegisterProperty("Title", typeof(string), null);
        #endregion Properties
        #region Commands

  

        /// <summary>
        /// Gets the CommandLogin command.
        /// </summary>
        public Command CommandLogin { get; private set; }

        /// <summary>
        /// Method to check whether the CommandLogin command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnCommandLoginCanExecute()
        {
            return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
        }

        /// <summary>
        /// Method to invoke when the CommandLogin command is executed.
        /// </summary>

        private void OnCommandLoginExecute()
        {         
                     IsBusy = true;

            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = true;

            worker.DoWork += ((o, ea) =>
                {
                    if (ea.Argument != null)
                    {
                        User u = ea.Argument as User;

                        using (var session = NHibernateSession.OpenSession())
                        {
                            var r = session.CreateCriteria<User>()
                                 .Add(Restrictions.Conjunction()
                                     .Add(Restrictions.Eq("Username", u.Username))
                                     .Add(Restrictions.Eq("Password", u.Password))
                                     ).List<User>()
                                     .FirstOrDefault<User>();
                            ea.Result = r;
                        }
                    }
                });
                
            worker.RunWorkerCompleted+=((o,ea)=>
                {
                    var result = (User)ea.Result;
                    if(result!=null)
                    {
                        //UserIsAuthenticated = true; //add invalidlogin
                       // Title = "Logout";
                        var main = this.ParentViewModel as MainWindowViewModel;
                        main.UserIsAuthenticated = true;
                        main.Username = result.Username;
                        main.Password = result.Password;
                        main.CommandCloseFlyout.Execute();
                    }
                    IsBusy = false;
                }
                );

            IsBusy = true;
            
            worker.RunWorkerAsync(User);

            //var t = new Task<bool>(() => 
            //{

            //    User user = new User();
            //    user.Username = this.Username;
            //    user.Password = this.Password;
            //   using (var session = NHibernateSession.OpenSession())
            //   {
            //       var r = session.CreateCriteria<User>()
            //            .Add(Restrictions.Conjunction()
            //                .Add(Restrictions.Eq("Username", user.Username))
            //                .Add(Restrictions.Eq("Password", user.Password))
            //                ).List<User>()
            //                .FirstOrDefault<User>();

            //      if (r!=null)
            //      {
            //          return true;
            //      }
            //   }
            //   return false;
            //});

            //t.RunSynchronously();

            //var dep = this.GetDependencyResolver();
            //var viewLocator = dep.Resolve<IViewLocator>();
            //viewLocator.Register(typeof(Flyouts.BusyIndicatorViewModel), typeof(Views.Flyouts.BusyIndicator));

            //var viewModelLocator = dep.Resolve<IViewModelLocator>();
            //viewModelLocator.Register(typeof(Views.Flyouts.BusyIndicator), typeof(ViewModels.Flyouts.BusyIndicatorViewModel));

            //var waitService = dep.Resolve<ISplashScreenService>();

            //waitService.Enqueue(new Catel.MVVM.Tasks.ActionTask("Login user", tracker =>
            //    {
            //        tracker.UpdateStatus("Authenticating user", true);
            //        System.Threading.Thread.Sleep(2000);

            //    }));

            //waitService.Commit<ViewModels.Flyouts.BusyIndicatorViewModel>();


            ////MessageBoxResult result = MessageBox.Show("Login Button is Pressed!",null,MessageBoxButton.OK);    
            //var viewModel = new Windows.InvalidLoginViewModel();
            //var dependencyResolver =   this.GetDependencyResolver();
            //var uiVisualizerService = dependencyResolver.Resolve<IUIVisualizerService>();
            //if (!uiVisualizerService.IsRegistered(typeof(Windows.InvalidLoginViewModel)))
            //{
            //    uiVisualizerService.Register(typeof(Windows.InvalidLoginViewModel), typeof(Views.Windows.CSSplashScreen));
            //}

            //DataAccessLayer.Model.Role role = new DataAccessLayer.Model.Role();
            //role.Username = this.Username;
            //role.Password = this.Password;

            //DataAccessLayer.Model.Role r = new DataAccessLayer.Model.Role();
            //DataAccessLayer.Repository.RRole getr = new DataAccessLayer.Repository.RRole();
            //r = getr.GetRole(role);
            //if (r != null)
            //{
            //    var main = this.ParentViewModel as MainWindowViewModel;
            //    main.Role = new DataAccessLayer.Model.Role();

            //    main.Role.Username = r.Username;
            //    main.Role.Password = r.Password;

            //    System.Diagnostics.Debug.WriteLine("User is authenticated");
            //    main.ShowFlyout = false;
            //}

            //else
            //{
            //    uiVisualizerService.ShowDialog(viewModel);
            //}


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
            MainWindowViewModel main = this.ParentViewModel as MainWindowViewModel;
            main.CommandAddNewUser.Execute();
        }
        #endregion Commands



        protected override async Task Initialize()
        {
            await base.Initialize();

            // TODO: subscribe to events here
        }

        protected override async Task Close()
        {
            // TODO: unsubscribe from events here

            await base.Close();
        }
        



    }
}
