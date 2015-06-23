using Catel.MVVM;
using System.Threading.Tasks;
using Catel.Data;
using Data.Model;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Data.Repository;
using Data.Model;
using Data.Session;
using NHibernate;
using System;


namespace Asset_Management_System.ViewModels.Flyouts
{

    
    public class AddNewUserViewModel : ViewModelBase
    {
        Repository<User> _repository;
        public AddNewUserViewModel()
        {          
            User = new User();
            ButtonSave = new Command(OnButtonSaveExecute, OnButtonSaveCanExecute);
            ButtonCancel = new Command(OnButtonCancelExecute, OnButtonCancelCanExecute);
            FlyoutClose = new Command(OnFlyoutCloseExecute);

            if (Userlevels == null)
                Userlevels = new List<string> { "User", "Supervisor" , "Admin" };         
          
        }

        public override string Title { get { return "New User"; } }

        protected override async Task Initialize()
        {
            await base.Initialize();

            // TODO: subscribe to events here
        }

        protected override async Task Close()
        {           
            await base.Close();
        }

        #region properties

        public List<string> Userlevels { get; set; }

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
        /// Gets or sets the property value Username.
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
        [ViewModelToModel("User")]
        public int Userlevel
        {
            get { return GetValue<int>(UserlevelProperty); }
            set { SetValue(UserlevelProperty, value); }
        }

        /// <summary>
        /// Register the Userlevel property so it is known in the class.
        /// </summary>
        public static readonly PropertyData UserlevelProperty = RegisterProperty("Userlevel", typeof(int),null,
            (sender,e)=>((AddNewUserViewModel)sender).OnUserlevelChanged());
       
        private void OnUserlevelChanged()
        {

        }

        #endregion properties


        #region commands
        /// <summary>
        /// Gets the ButtonSave command.
        /// </summary>
        public Command ButtonSave { get; private set; }
        
        /// <summary>
        /// Method to check whether the ButtonSave command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnButtonSaveCanExecute()
        {
            return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
        }

        /// <summary>
        /// Method to invoke when the ButtonSave command is executed.
        /// </summary>
        private void OnButtonSaveExecute()
        {
            try
            {
                using(ISession session = NHibernateSession.OpenSession())
                {
                    _repository = new Repository<User>(session);
                    _repository.SaveOrUpdate(User);
                }
            }

            catch (Exception ex) { }
          
            var main = this.ParentViewModel as MainWindowViewModel;
            main.ShowFlyout = false;
        }
        /// <summary>
        /// Gets the ButtonCancel command.
        /// </summary>
        public Command ButtonCancel { get; private set; }

        /// <summary>
        /// Method to check whether the ButtonCancel command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnButtonCancelCanExecute()
        {
            return true;
        }

        /// <summary>
        /// Method to invoke when the ButtonCancel command is executed.
        /// </summary>
        private void OnButtonCancelExecute()
        {
            var mainwindow = this.ParentViewModel as MainWindowViewModel;
            mainwindow.ShowFlyout = false;
        }

        /// <summary>
        /// Gets the FlyoutClose command.
        /// </summary>
        public Command FlyoutClose { get; private set; }

        /// <summary>
        /// Method to invoke when the FlyoutClose command is executed.
        /// </summary>
        private void OnFlyoutCloseExecute()
        {
            this.Close();
        }

        #endregion
    }
}
