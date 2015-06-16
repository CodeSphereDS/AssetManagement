using Catel.MVVM;
using Catel.Data;
using System.Threading.Tasks;
using Data.Model;
using Asset_Management_System.ViewModels.IReports;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using NHibernate;
using Data.Session;
using Catel.IoC;
using Catel.Services;
using Catel.Messaging;

namespace Asset_Management_System.ViewModels.Reports
{

    public class AssetMasterListViewModel : ViewModelBase
    {
        IMessageMediator _messagemediator;
        public AssetMasterListViewModel()
        {
            // register messages
            var dep = this.GetDependencyResolver();
            _messagemediator = dep.Resolve<IMessageMediator>();
            _messagemediator.Register<bool>(this, AssetCategoryIsBusy, "ItemCategoryIsBusy");
            _messagemediator.Register<Item>(this, UpdateItemCategoryCollection, "UpdateItemCategoryCollection");

            ButtonCommandAddCategory = new Command(OnButtonCommandAddCategoryExecute, OnButtonCommandAddCategoryCanExecute);
            ButtonCommandDeleteCategory = new Command(OnButtonCommandDeleteCategoryExecute, OnButtonCommandDeleteCategoryCanExecute);

            if (ItemCategoryCollection == null)
            {
                LoadItemCategory();
            }

        }

        public override string Title { get { return "View model title"; } }

        protected override async Task Initialize()
        {
            await base.Initialize();
        }
        protected override async Task Close()
        {
            await base.Close();
        }

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        public ItemCategory ItemCategorySelected
        {
            get { return GetValue<ItemCategory>(ItemCategorySelectedProperty); }
            set { SetValue(ItemCategorySelectedProperty, value); }
        }
        /// <summary>
        /// Register the ItemCategorySelected property so it is known in the class.
        /// </summary>
        public static readonly PropertyData ItemCategorySelectedProperty = RegisterProperty("ItemCategorySelected", typeof(ItemCategory), null, (sender, e) => ((AssetMasterListViewModel)sender).OnItemCategorySelectedChanged());
        /// <summary>
        /// Called when the ItemCategorySelected property has changed.
        /// </summary>
        private void OnItemCategorySelectedChanged()
        {
            AssetListContent = new AssetMasterListItemViewModel(ItemCategorySelected);
        }

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        public ObservableCollection<ItemCategory> ItemCategoryCollection
        {
            get { return GetValue<ObservableCollection<ItemCategory>>(ItemCategoryCollectionProperty); }
            set { SetValue(ItemCategoryCollectionProperty, value); }
        }

        /// <summary>
        /// Register the ItemCategoryCollection property so it is known in the class.
        /// </summary>
        public static readonly PropertyData ItemCategoryCollectionProperty = RegisterProperty("ItemCategoryCollection", typeof(ObservableCollection<ItemCategory>), null, (sender, e) => ((AssetMasterListViewModel)sender).OnItemCategoryCollectionChanged());

        /// <summary>
        /// Called when the ItemCategoryCollection property has changed.
        /// </summary>
        private void OnItemCategoryCollectionChanged()
        {
            if (ItemCategoryCollection.Count > 0)   //then we have a valid collection
            {
                ItemCategorySelected = ItemCategoryCollection[0];
            }
        }
        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        public IViewModel AssetListContent
        {
            get { return GetValue<ViewModelBase>(AssetListContentProperty); }
            set { SetValue(AssetListContentProperty, value); }
        }

        /// <summary>
        /// Register the AssetListContent property so it is known in the class.
        /// </summary>
        public static readonly PropertyData AssetListContentProperty = RegisterProperty("AssetListContent", typeof(ViewModelBase), null);

        #region Methods

        public void LoadItemCategory()
        {

            System.ComponentModel.BackgroundWorker worker = new System.ComponentModel.BackgroundWorker();

            worker.DoWork += ((o, ea) =>
            {
                try
                {
                    using (var session = Data.Session.NHibernateSession.OpenSession())
                    {
                        var category = session.CreateCriteria<ItemCategory>()
                            //.SetFetchMode("Items", FetchMode.)
                             .List<ItemCategory>();

                        foreach (ItemCategory itemcategory in category)
                        {
                            foreach (Item item in itemcategory.Items)
                            {
                                NHibernateUtil.Initialize(item);
                            }
                        }
                        if (category != null)
                        {
                            ea.Result = category;
                        }
                        else ea.Result = new List<ItemCategory>();
                    }
                }
                catch (Exception ex) { }
            });

            worker.RunWorkerCompleted += ((o, ea) =>
                {
                    if (ea.Result != null)
                    {
                        ItemCategoryCollection = new ObservableCollection<ItemCategory>(ea.Result as List<ItemCategory>);
                    }
                    else
                        ItemCategoryCollection = new ObservableCollection<ItemCategory>();

                    _messagemediator.SendMessage<bool>(false, "MainWindowIsBusy");

                });
            _messagemediator.SendMessage<bool>(true, "MainWindowIsBusy");
            worker.RunWorkerAsync();

        }

        [MessageRecipient(Tag = "UpdateItemCategoryCollection")]
        private void UpdateItemCategoryCollection(Item item)
        {
            var itemcategory = item.ItemCategoryID;
            //find old item category and delete

            foreach (ItemCategory category in ItemCategoryCollection)
            {
                if (category.Items.Contains(item))
                {
                    category.Items.Remove(item);
                }
            }


            //add new item in new category
            if (itemcategory != null)
            {
                foreach (ItemCategory category in ItemCategoryCollection)

                    if (category.ItemCategoryID == itemcategory.ItemCategoryID)
                    {
                        category.Items.Add(item);
                    }
            }

        }
        [MessageRecipient(Tag = "ItemCategoryIsBusy")]
        private void AssetCategoryIsBusy(bool busy)
        {
            IsBusy = busy;
        }


        #endregion Methods

        #region Commands
        /// <summary>
        /// Gets the ButtonCommandAddCategory command.
        /// </summary>
        public Command ButtonCommandAddCategory { get; private set; }

        // TODO: Move code below to constructor

        // TODO: Move code above to constructor

        /// <summary>
        /// Method to check whether the ButtonCommandAddCategory command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnButtonCommandAddCategoryCanExecute()
        {
            return true;
        }

        /// <summary>
        /// Method to invoke when the ButtonCommandAddCategory command is executed.
        /// </summary>
        private async void OnButtonCommandAddCategoryExecute()
        {
            ItemCategory i = new ItemCategory();
            var viewmodel = new AddEditCategoryViewModel(i);

            var dep = this.GetDependencyResolver();
            var service = dep.Resolve<IUIVisualizerService>();

            if (await service.ShowDialogAsync(viewmodel) ?? false)
            {
                System.ComponentModel.BackgroundWorker worker = new System.ComponentModel.BackgroundWorker();
                worker.DoWork += ((o, e) =>
                {
                    try
                    {
                        using (var session = NHibernateSession.OpenSession())
                        {
                            session.Save(i);
                        }
                    }
                    catch (Exception ex) { }
                });

                worker.RunWorkerCompleted += ((o, ea) =>
                {
                    ItemCategoryCollection.Add(i);
                    IsBusy = false;
                });

                IsBusy = true;
                worker.RunWorkerAsync();
            }
        }


        /// <summary>
        /// Gets the ButtonCommandDeleteCategory command.
        /// </summary>
        public Command ButtonCommandDeleteCategory { get; private set; }

        /// <summary>
        /// Method to check whether the ButtonCommandDeleteCategory command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnButtonCommandDeleteCategoryCanExecute()
        {
            return ItemCategorySelected!=null && ItemCategorySelected.Items.Count==0;
        }

        /// <summary>
        /// Method to invoke when the ButtonCommandDeleteCategory command is executed.
        /// </summary>
        private void OnButtonCommandDeleteCategoryExecute()
        {
            System.ComponentModel.BackgroundWorker worker = new System.ComponentModel.BackgroundWorker();
            worker.DoWork += ((o, e) =>
            {
                try
                {
                    using (var session = NHibernateSession.OpenSession())
                    {
                        session.BeginTransaction();
                        session.Delete(ItemCategorySelected);
                        session.Transaction.Commit();
                    }
                }
                catch (Exception ex)
                { }

            });

            worker.RunWorkerCompleted += ((o, ea) =>
            {
                ItemCategoryCollection.Remove(ItemCategorySelected);
                IsBusy = false;

            });

            IsBusy = true;
            worker.RunWorkerAsync();
        }

        public bool IsBusy
        {
            get { return GetValue<bool>(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }

        /// <summary>
        /// Register the IsBusy property so it is known in the class.
        /// </summary>
        public static readonly PropertyData IsBusyProperty = RegisterProperty("IsBusy", typeof(bool), null, (sender, e) => ((AssetMasterListViewModel)sender).OnIsBusyChanged());

        /// <summary>
        /// Called when the IsBusy property has changed.
        /// </summary>
        private void OnIsBusyChanged()
        {
            // TODO: Implement logic
        }
        #endregion Commands



    }
}
