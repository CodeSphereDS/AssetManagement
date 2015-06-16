using Catel.MVVM;
using System.Threading.Tasks;
using Catel.Data;
using Data.Model;

namespace Asset_Management_System.ViewModels.Reports
{


    public class AddEditCategoryViewModel : ViewModelBase
    {
        public AddEditCategoryViewModel(ItemCategory itemcategory)
        {
            ButtonCommandSave = new Command(OnButtonCommandSaveExecute, OnButtonCommandSaveCanExecute);
            ButtonCommandCancel = new Command(OnButtonCommandCancelExecute, OnButtonCommandCancelCanExecute);

            ItemCategory = itemcategory;
        }

        #region properties

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        [Model]
        public ItemCategory ItemCategory
        {
            get { return GetValue<ItemCategory>(ItemCategoryProperty); }
            private set { SetValue(ItemCategoryProperty, value); }
        }

        /// <summary>
        /// Register the ItemCategory property so it is known in the class.
        /// </summary>
        public static readonly PropertyData ItemCategoryProperty = RegisterProperty("ItemCategory", typeof(ItemCategory));

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        [ViewModelToModel("ItemCategory")]
        public string ItemCategoryName
        {
            get { return GetValue<string>(ItemCategoryNameProperty); }
            set { SetValue(ItemCategoryNameProperty, value); }
        }

        /// <summary>
        /// Register the ItemCategoryName property so it is known in the class.
        /// </summary>
        public static readonly PropertyData ItemCategoryNameProperty = RegisterProperty("ItemCategoryName", typeof(string));

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        [ViewModelToModel("ItemCategory")]
        public string ItemCategoryDescription
        {
            get { return GetValue<string>(ItemCategoryDescriptionProperty); }
            set { SetValue(ItemCategoryDescriptionProperty, value); }
        }

        /// <summary>
        /// Register the ItemCategoryDescription property so it is known in the class.
        /// </summary>
        public static readonly PropertyData ItemCategoryDescriptionProperty = RegisterProperty("ItemCategoryDescription", typeof(string));

        #endregion properties


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

        /// <summary>
        /// Gets the ButtonCommandSave command.
        /// </summary>
        public Command ButtonCommandSave { get; private set; }

        // TODO: Move code below to constructor

        // TODO: Move code above to constructor

        /// <summary>
        /// Method to check whether the ButtonCommandSave command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnButtonCommandSaveCanExecute()
        {
            return true;
        }

        /// <summary>
        /// Method to invoke when the ButtonCommandSave command is executed.
        /// </summary>
        private void OnButtonCommandSaveExecute()
        {
            SaveAndCloseViewModel();
        }

        /// <summary>
        /// Gets the ButtonCommandCancel command.
        /// </summary>
        public Command ButtonCommandCancel { get; private set; }

        // TODO: Move code below to constructor

        // TODO: Move code above to constructor

        /// <summary>
        /// Method to check whether the ButtonCommandCancel command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnButtonCommandCancelCanExecute()
        {
            return true;
        }

        /// <summary>
        /// Method to invoke when the ButtonCommandCancel command is executed.
        /// </summary>
        private void OnButtonCommandCancelExecute()
        {
            CancelAndCloseViewModel();
        }
    }

}
