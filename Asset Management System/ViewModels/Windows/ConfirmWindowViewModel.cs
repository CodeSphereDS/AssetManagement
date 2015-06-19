namespace Asset_Management_System.ViewModels.Windows
{
    using Catel.MVVM;
    using System.Threading.Tasks;

    public class ConfirmWindowViewModel : ViewModelBase
    {
        public ConfirmWindowViewModel()
        {
            CommandOK = new Command(OnCommandOKExecute);
            CommandCancel = new Command(OnCommandCancelExecute);
        }

        public override string Title { get { return "Confirm"; } }
      
        protected override async Task Initialize()
        {
            await base.Initialize();           
        }

        protected override async Task Close()
        {
           await base.Close();
        }

        /// <summary>
/// Gets the CommandOK command.
/// </summary>
public Command CommandOK { get; private set; }



/// <summary>
/// Method to invoke when the CommandOK command is executed.
/// </summary>
private void OnCommandOKExecute()
{
    this.CloseViewModel(true);
}

        /// <summary>
/// Gets the CommandCancel command.
/// </summary>
public Command CommandCancel { get; private set; }


/// <summary>
/// Method to invoke when the CommandCancel command is executed.
/// </summary>
private void OnCommandCancelExecute()
{
    this.CancelAndCloseViewModel();
}
    }
}
