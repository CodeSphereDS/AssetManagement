using Catel.MVVM;
using System.Threading.Tasks;
using Data.Model;
using Catel.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Data.Session;

namespace Asset_Management_System.ViewModels.Reports
{

    public class AddEditAssetWindowViewModel : ViewModelBase
    {

        public AddEditAssetWindowViewModel(Item itemselected, User currentUser)
        {
            //OldItemCategory = (itemselected != null) ? itemselected.ItemCategoryID : new ItemCategory();

            AddedBy = (currentUser != null) ? currentUser : new User();

            Item = (itemselected != null) ? itemselected : new Item();
            DateAdded = (itemselected.DateAdded != null || !itemselected.DateAdded.Equals(0)) ? itemselected.DateAdded : DateTime.Now;



            ButtonCommandCancel = new Command(OnButtonCommandCancelExecute, OnButtonCommandCancelCanExecute);
            ButtonCommandSave = new Command(OnButtonCommandSaveExecute, OnButtonCommandSaveCanExecute);

            if (ItemCategory == null)
            {
                GetListItemCategory();
            }
            if (UsersCollection == null)
            {
                GetListUsers();
            }
        }

        public override string Title { get { return (Item == null) ? "Add Asset" : "Edit Asset"; } }

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


        #region model and viewmodel map
        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        [Model]
        public Item Item
        {
            get { return GetValue<Item>(ItemProperty); }
            private set { SetValue(ItemProperty, value); }
        }

        /// <summary>
        /// Register the Item property so it is known in the class.
        /// </summary>
        public static readonly PropertyData ItemProperty = RegisterProperty("Item", typeof(Item));

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        [ViewModelToModel("Item")]
        public string ItemBrandName
        {
            get { return GetValue<string>(ItemBrandNameProperty); }
            set { SetValue(ItemBrandNameProperty, value); }
        }

        /// <summary>
        /// Register the ItemBrandName property so it is known in the class.
        /// </summary>
        public static readonly PropertyData ItemBrandNameProperty = RegisterProperty("ItemBrandName", typeof(string));

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        [ViewModelToModel("Item")]
        public string ItemBrandModel
        {
            get { return GetValue<string>(ItemBrandModelProperty); }
            set { SetValue(ItemBrandModelProperty, value); }
        }

        /// <summary>
        /// Register the ItemBrandModel property so it is known in the class.
        /// </summary>
        public static readonly PropertyData ItemBrandModelProperty = RegisterProperty("ItemBrandModel", typeof(string));

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        [ViewModelToModel("Item")]
        public string ItemSerialNumber
        {
            get { return GetValue<string>(ItemSerialNumberProperty); }
            set { SetValue(ItemSerialNumberProperty, value); }
        }

        /// <summary>
        /// Register the ItemSerialNumber property so it is known in the class.
        /// </summary>
        public static readonly PropertyData ItemSerialNumberProperty = RegisterProperty("ItemSerialNumber", typeof(string));

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        [ViewModelToModel("Item")]
        public string ItemDetails
        {
            get { return GetValue<string>(ItemDetailsProperty); }
            set { SetValue(ItemDetailsProperty, value); }
        }

        /// <summary>
        /// Register the ItemDetails property so it is known in the class.
        /// </summary>
        public static readonly PropertyData ItemDetailsProperty = RegisterProperty("ItemDetails", typeof(string));

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        [ViewModelToModel("Item")]
        public ItemStatus ItemStatus
        {
            get { return GetValue<ItemStatus>(ItemStatusProperty); }
            set { SetValue(ItemStatusProperty, value); }
        }

        /// <summary>
        /// Register the ItemStatus property so it is known in the class.
        /// </summary>
        public static readonly PropertyData ItemStatusProperty = RegisterProperty("ItemStatus", typeof(ItemStatus));

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        [ViewModelToModel("Item")]
        public DateTime? DatePurchased
        {
            get { return GetValue<DateTime?>(DatePurchasedProperty); }
            set { SetValue(DatePurchasedProperty, value); }
        }

        /// <summary>
        /// Register the DatePurchased property so it is known in the class.
        /// </summary>
        public static readonly PropertyData DatePurchasedProperty = RegisterProperty("DatePurchased", typeof(DateTime?));

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        [ViewModelToModel("Item")]
        public DateTime? WarrantyExpiration
        {
            get { return GetValue<DateTime?>(WarrantyExpirationProperty); }
            set { SetValue(WarrantyExpirationProperty, value); }
        }

        /// <summary>
        /// Register the WarrantyExpiration property so it is known in the class.
        /// </summary>
        public static readonly PropertyData WarrantyExpirationProperty = RegisterProperty("WarrantyExpiration", typeof(DateTime?));

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        [ViewModelToModel("Item")]
        public DateTime? DateAdded
        {
            get { return GetValue<DateTime?>(DateAddedProperty); }
            set { SetValue(DateAddedProperty, value); }
        }

        /// <summary>
        /// Register the DateAdded property so it is known in the class.
        /// </summary>
        public static readonly PropertyData DateAddedProperty = RegisterProperty("DateAdded", typeof(DateTime?));

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        [ViewModelToModel("Item")]
        public User RequestedBy
        {
            get { return GetValue<User>(RequestedByProperty); }
            set { SetValue(RequestedByProperty, value); }
        }

        /// <summary>
        /// Register the RequestedBy property so it is known in the class.
        /// </summary>
        public static readonly PropertyData RequestedByProperty = RegisterProperty("RequestedBy", typeof(User));

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        [ViewModelToModel("Item")]
        public User ApprovedBy
        {
            get { return GetValue<User>(ApprovedByProperty); }
            set { SetValue(ApprovedByProperty, value); }
        }

        /// <summary>
        /// Register the ApprovedBy property so it is known in the class.
        /// </summary>
        public static readonly PropertyData ApprovedByProperty = RegisterProperty("ApprovedBy", typeof(User));

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        [ViewModelToModel("Item")]
        public User AssignedTo
        {
            get { return GetValue<User>(AssignedToProperty); }
            set { SetValue(AssignedToProperty, value); }
        }

        /// <summary>
        /// Register the AssignedTo property so it is known in the class.
        /// </summary>
        public static readonly PropertyData AssignedToProperty = RegisterProperty("AssignedTo", typeof(User));

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        [ViewModelToModel("Item")]
        public User AddedBy
        {
            get { return GetValue<User>(AddedByProperty); }
            set { SetValue(AddedByProperty, value); }
        }

        /// <summary>
        /// Register the AddedBy property so it is known in the class.
        /// </summary>
        public static readonly PropertyData AddedByProperty = RegisterProperty("AddedBy", typeof(User));


        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        [ViewModelToModel("Item")]
        public User LastModifiedBy
        {
            get { return GetValue<User>(LastModifiedByProperty); }
            set { SetValue(LastModifiedByProperty, value); }
        }

        /// <summary>
        /// Register the LastModifiedBy property so it is known in the class.
        /// </summary>
        public static readonly PropertyData LastModifiedByProperty = RegisterProperty("LastModifiedBy", typeof(User));

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        [ViewModelToModel("Item")]
        public ItemCategory ItemCategoryID
        {
            get { return GetValue<ItemCategory>(ItemCategoryIDProperty); }
            set { SetValue(ItemCategoryIDProperty, value); }
        }

        /// <summary>
        /// Register the ItemCategoryID property so it is known in the class.
        /// </summary>
        public static readonly PropertyData ItemCategoryIDProperty = RegisterProperty("ItemCategoryID", typeof(ItemCategory));



        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        public string ItemStatusValue
        {
            get { return GetValue<string>(ItemStatusValueProperty); }
            set { SetValue(ItemStatusValueProperty, value); }
        }

        /// <summary>
        /// Register the ItemStatusValue property so it is known in the class.
        /// </summary>
        public static readonly PropertyData ItemStatusValueProperty = RegisterProperty("ItemStatusValue", typeof(string), null, (sender, e) => ((AddEditAssetWindowViewModel)sender).OnItemStatusValueChanged());

        /// <summary>
        /// Called when the ItemStatusValue property has changed.
        /// </summary>
        private void OnItemStatusValueChanged()
        {
            System.Diagnostics.Debug.WriteLine(string.Format("ItemStatus Value is {0}", ItemStatusValue));
            ItemStatus _itemstatus;
            Enum.TryParse<ItemStatus>(ItemStatusValue, out _itemstatus);

            ItemStatus = _itemstatus;
            System.Diagnostics.Debug.WriteLine(string.Format("ItemStatus is {0}", ItemStatus));
        }
        #endregion model to viewmodel map


        private void GetListUsers()
        {
            System.ComponentModel.BackgroundWorker worker = new System.ComponentModel.BackgroundWorker();

            worker.DoWork += ((o, ea) =>
                 {
                     using (var session = Data.Session.NHibernateSession.OpenSession())
                     {
                         try
                         {
                             ea.Result = session.CreateCriteria<User>().List<User>();
                         }
                         catch (Exception ex) { }
                     }

                 });

            worker.RunWorkerCompleted += ((o, ea) =>
            {
                var result = ea.Result as IList<User>;
                if (result != null)
                {
                    UsersCollection = new ObservableCollection<User>(result);
                }
                IsBusy = false;
            });

            IsBusy = true;
            worker.RunWorkerAsync();


        }

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        public ObservableCollection<User> UsersCollection
        {
            get { return GetValue<ObservableCollection<User>>(UsersCollectionProperty); }
            set { SetValue(UsersCollectionProperty, value); }
        }

        /// <summary>
        /// Register the UsersCollection property so it is known in the class.
        /// </summary>
        public static readonly PropertyData UsersCollectionProperty = RegisterProperty("UsersCollection", typeof(ObservableCollection<User>), null, (sender, e) => ((AddEditAssetWindowViewModel)sender).OnUsersCollectionChanged());

        /// <summary>
        /// Called when the UsersCollection property has changed.
        /// </summary>
        private void OnUsersCollectionChanged()
        {
            // TODO: Implement logic
        }

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
        /// Gets the ButtonCommandSave command.
        /// </summary>
        public Command ButtonCommandSave { get; private set; }
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
        public ObservableCollection<ItemCategory> ItemCategory { get; set; }
        //  private ItemCategory OldItemCategory { get; set; }

        #region Methods

        private void GetListItemCategory()
        {
            System.ComponentModel.BackgroundWorker worker = new System.ComponentModel.BackgroundWorker();
            worker.DoWork += ((o, ea) =>
            {
                try
                {
                    using (var session = NHibernateSession.OpenSession())
                    {
                        var category = session.CreateCriteria<ItemCategory>().List<ItemCategory>();
                        ItemCategory = new ObservableCollection<ItemCategory>(category);
                    }

                }

                catch (Exception ex) { }

                if (ItemCategory == null)
                {
                    ItemCategory = new ObservableCollection<ItemCategory>();
                }

                IsBusy = false;
            });

            IsBusy = true;
            worker.RunWorkerAsync();
        }

        #endregion Methods

    }
}
