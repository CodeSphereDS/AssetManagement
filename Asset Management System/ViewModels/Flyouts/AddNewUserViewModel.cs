using Catel.MVVM;
using System.Threading.Tasks;
using Catel.Data;


namespace Asset_Management_System.ViewModels.Flyouts
{


    public class AddNewUserViewModel : ViewModelBase
    {
        public AddNewUserViewModel()
        {
            ButtonSave = new Command(OnButtonSaveExecute, OnButtonSaveCanExecute);
            ButtonCancel = new Command(OnButtonCancelExecute, OnButtonCancelCanExecute);
            FlyoutClose = new Command(OnFlyoutCloseExecute);

        }

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        public string Username
        {
            get { return GetValue<string>(UsernameProperty); }
            set { SetValue(UsernameProperty, value); }
        }

        /// <summary>
        /// Register the Username property so it is known in the class.
        /// </summary>
        public static readonly PropertyData UsernameProperty = RegisterProperty("Username", typeof(string), null, (sender, e) => ((AddNewUserViewModel)sender).OnUsernameChanged());

        /// <summary>
        /// Called when the Username property has changed.
        /// </summary>
        private void OnUsernameChanged()
        {
            // TODO: Implement logic
        }

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        public string Password
        {
            get { return GetValue<string>(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        /// <summary>
        /// Register the Password property so it is known in the class.
        /// </summary>
        public static readonly PropertyData PasswordProperty = RegisterProperty("Password", typeof(string), null, (sender, e) => ((AddNewUserViewModel)sender).OnPasswordChanged());

        /// <summary>
        /// Called when the Password property has changed.
        /// </summary>
        private void OnPasswordChanged()
        {
            // TODO: Implement logic
        }
        public override string Title { get { return "New User"; } }

        protected override async Task Initialize()
        {
            await base.Initialize();

            // TODO: subscribe to events here
        }

        protected override async Task Close()
        {
            // TODO: unsubscribe from events here

            // MainWindowViewModel main = this.ParentViewModel as MainWindowViewModel;
            // main.CommandShowLogin.Execute();
            await base.Close();

        }

        /// <summary>
        /// Gets the ButtonSave command.
        /// </summary>
        public Command ButtonSave { get; private set; }

        // TODO: Move code below to constructor

        // TODO: Move code above to constructor

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
            //DataAccessLayer.Model.Role role = new DataAccessLayer.Model.Role();
            //role.Username = Username;
            //role.Password = Password;
            //DataAccessLayer.Repository.RRole rrole = new DataAccessLayer.Repository.RRole();
            //rrole.SaveRole(role);
            //MainWindowViewModel main = this.ParentViewModel as MainWindowViewModel;
            //main.CommandShowLogin.Execute();
        }
        /// <summary>
        /// Gets the ButtonCancel command.
        /// </summary>
        public Command ButtonCancel { get; private set; }

        // TODO: Move code below to constructor

        // TODO: Move code above to constructor

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
            MainWindowViewModel main = this.ParentViewModel as MainWindowViewModel;
            main.CommandShowLogin.Execute();
        }

        /// <summary>
        /// Gets the FlyoutClose command.
        /// </summary>
        public Command FlyoutClose { get; private set; }

        // TODO: Move code below to constructor

        // TODO: Move code above to constructor

        /// <summary>
        /// Method to invoke when the FlyoutClose command is executed.
        /// </summary>
        private void OnFlyoutCloseExecute()
        {
            this.Close();
        }
    }
}
