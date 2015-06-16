namespace Asset_Management_System.ViewModels.Windows
{
    using Catel.MVVM;
    using System.Threading.Tasks;

    public class SignOutViewModel : ViewModelBase
    {
        public SignOutViewModel()
        {
            CommandCancel = new Command(OnCommandCancelExecute);
            CommandClose = new Command(OnCommandCloseExecute);
        }

        public override string Title { get { return "View model title"; } }

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

        /// <summary>
        /// Gets the CommandClose command.
        /// </summary>
        public Command CommandClose { get; private set; }

        // TODO: Move code below to constructor

        // TODO: Move code above to constructor

        /// <summary>
        /// Method to invoke when the CommandClose command is executed.
        /// </summary>
        private void OnCommandCloseExecute()
        {
            this.CloseViewModel(true);
        }

        /// <summary>
        /// Gets the CommandCancel command.
        /// </summary>
        public Command CommandCancel { get; private set; }

        // TODO: Move code below to constructor

        // TODO: Move code above to constructor

        /// <summary>
        /// Method to invoke when the CommandCancel command is executed.
        /// </summary>
        private void OnCommandCancelExecute()
        {
            this.CancelAndCloseViewModel();
        }
    }
}
