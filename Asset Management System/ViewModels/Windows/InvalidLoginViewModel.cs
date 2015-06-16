namespace Asset_Management_System.ViewModels.Windows
{
    using Catel.MVVM;
    using System.Threading.Tasks;

    public class InvalidLoginViewModel : ViewModelBase
    {
        public InvalidLoginViewModel()
        {
            CommandClose = new Command(OnCommandCloseExecute);
        }

       // public override string Title { get { return "View model title"; } }

        // TODO: Register models with the vmpropmodel codesnippet
        // TODO: Register view model properties with the vmprop or vmpropviewmodeltomodel codesnippets
        // TODO: Register commands with the vmcommand or vmcommandwithcanexecute codesnippets

        protected override async Task Initialize()
        {
            await base.Initialize();

            // TODO: subscribe to events here
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
            this.CloseViewModel(false);
        }

        protected override async Task Close()
        {
            // TODO: unsubscribe from events here

            await base.Close();
        }
    }
}
