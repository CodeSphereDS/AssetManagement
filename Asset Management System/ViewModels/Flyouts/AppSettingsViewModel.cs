using Catel.MVVM;
using System.Threading.Tasks;
using System;
using Catel.Data;


namespace Asset_Management_System.ViewModels.Flyouts
{

    public class AppSettingsViewModel : ViewModelBase
    {
        public AppSettingsViewModel()
        {
          //  DatabaseCloud = new Uri();

        }

        public override string Title { get { return "Settings"; } }     

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
        /// Gets or sets the property value.
        /// </summary>
        public Uri DatabaseUri
        {
            get { return GetValue<Uri>(DatabaseUriProperty); }
            set { SetValue(DatabaseUriProperty, value); }
        }

        /// <summary>
        /// Register the DatabaseUri property so it is known in the class.
        /// </summary>
        public static readonly PropertyData DatabaseUriProperty = RegisterProperty("DatabaseUri", typeof(Uri), null, (sender, e) => ((AppSettingsViewModel)sender).OnDatabaseUriChanged());

        /// <summary>
        /// Called when the DatabaseUri property has changed.
        /// </summary>
        private void OnDatabaseUriChanged()
        {
            // TODO: Implement logic
        }

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        public string DatabasePassword
        {
            get { return GetValue<string>(DatabasePasswordProperty); }
            set { SetValue(DatabasePasswordProperty, value); }
        }

        /// <summary>
        /// Register the DatabasePassword property so it is known in the class.
        /// </summary>
        public static readonly PropertyData DatabasePasswordProperty = RegisterProperty("DatabasePassword", typeof(string), null);

   
    }
}
