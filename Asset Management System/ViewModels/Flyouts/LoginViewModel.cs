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
using Catel.Messaging;

namespace Asset_Management_System.ViewModels.Flyouts
{
    public class LoginViewModel : ViewModelBase
    {
        private IMessageMediator _messagemediator;
        private IUIVisualizerService _uivisualizer;
        public LoginViewModel(User user)           
        {
            User = user;
            if (!User.UserIsAuthenticated) Title = "Login";
           
            CommandLogin = new Command(OnCommandLoginExecute, OnCommandLoginCanExecute);

            var dep = this.GetDependencyResolver();
            _messagemediator = dep.Resolve<IMessageMediator>();
            _uivisualizer = dep.Resolve<IUIVisualizerService>();
          
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
                        var main = this.ParentViewModel as MainWindowViewModel;
                        main.UserIsAuthenticated = true;
                        main.Username = result.Username;
                        main.Password = result.Password;
                        main.CommandCloseFlyout.Execute();
                       
                    }
                    else
                    {
                        var viewmodel = new ViewModels.Windows.InvalidLoginViewModel();
                        _uivisualizer.ShowDialog(viewmodel);
                    }
                    IsBusy = false;
                    _messagemediator.SendMessage<bool>(false, "MainWindowFlyoutIsBusy");
                }
                );

            IsBusy = true;
            _messagemediator.SendMessage<bool>(true,"MainWindowFlyoutIsBusy");
            
            worker.RunWorkerAsync(User);          

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
