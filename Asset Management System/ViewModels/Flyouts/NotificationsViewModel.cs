namespace Asset_Management_System.ViewModels.Flyouts
{
    using Catel.MVVM;
    using System.Threading.Tasks;

    public class NotificationsViewModel : ViewModelBase
    {
        public NotificationsViewModel()
        {
        }

        public override string Title { get { return "View model title"; } }

        // TODO: Register models with the vmpropmodel codesnippet
        // TODO: Register view model properties with the vmprop or vmpropviewmodeltomodel codesnippets
        // TODO: Register commands with the vmcommand or vmcommandwithcanexecute codesnippets

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
