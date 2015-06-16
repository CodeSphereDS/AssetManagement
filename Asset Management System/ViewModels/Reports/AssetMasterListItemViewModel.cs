using Catel.MVVM;
using Catel.Services;
using Catel.IoC;
using System.Threading.Tasks;
using Data.Session;
using Data.Model;
using System.Collections.ObjectModel;
using Catel.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using Catel.Reflection;
using CodeSphere.CSClasses;
using Catel.Messaging;

namespace Asset_Management_System.ViewModels.Reports
{


    public class AssetMasterListItemViewModel : ViewModelBase
    {
        private ItemCategory _oldcategory;       
        private IMessageMediator _messagemediator;
        public AssetMasterListItemViewModel(ItemCategory ItemCategory)
        {

            _oldcategory = ItemCategory;
            ItemCategoryID = ItemCategory;

            ButtonCommandDeleteItem = new Command(OnButtonCommandDeleteItemExecute, OnButtonCommandDeleteItemCanExecute);
            ButtonCommandEditItem = new Command(OnButtonCommandEditItemExecute, OnButtonCommandEditItemCanExecute);
            ButtonCommandAddItem = new Command(OnButtonCommandAddItemExecute, OnButtonCommandAddItemCanExecute);


            ItemCollection = (ItemCategory != null) ? new ObservableCollection<Item>(ItemCategory.Items) : new ObservableCollection<Item>();

            if (ItemCollection.Count > 0)
            {
                ItemSelected = ItemCollection[0];
            }

            //foreach(Item item in ItemCategory.Items)
            //{
            //    ItemCollection.Add(item);
            //}

            //register mediator
            var dep = this.GetDependencyResolver();
            _messagemediator = dep.Resolve<IMessageMediator>();
            _messagemediator.Register<bool>(this, AssetMasterListIsBusy, "AssetMasterListIsBusy");
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

        #region properties

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>     
        public Item ItemSelected
        {
            get { return GetValue<Item>(ItemSelectedProperty); }
            set { SetValue(ItemSelectedProperty, value); }
        }

        /// <summary>
        /// Register the ItemSelected property so it is known in the class.
        /// </summary>
        public static readonly PropertyData ItemSelectedProperty = RegisterProperty("ItemSelected", typeof(Item), null, (sender, e) => ((AssetMasterListItemViewModel)sender).OnItemSelectedChanged());

        /// <summary>
        /// Called when the ItemSelected property has changed.
        /// </summary>
        private void OnItemSelectedChanged()
        {
            // TODO: Implement logic
        }

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        public ObservableCollection<Item> ItemCollection
        {
            get { return GetValue<ObservableCollection<Item>>(ItemCollectionProperty); }
            set { SetValue(ItemCollectionProperty, value); }
        }

        /// <summary>
        /// Register the ItemCollection property so it is known in the class.
        /// </summary>
        public static readonly PropertyData ItemCollectionProperty = RegisterProperty("ItemCollection", typeof(ObservableCollection<Item>), null, (sender, e) => ((AssetMasterListItemViewModel)sender).OnItemCollectionChanged());

        /// <summary>
        /// Called when the ItemCollection property has changed.
        /// </summary>
        private void OnItemCollectionChanged()
        {

        }

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        public ItemCategory ItemCategoryID
        {
            get { return GetValue<ItemCategory>(ItemCategoryIDProperty); }
            set { SetValue(ItemCategoryIDProperty, value); }
        }

        /// <summary>
        /// Register the ItemCategoryID property so it is known in the class.
        /// </summary>
        public static readonly PropertyData ItemCategoryIDProperty = RegisterProperty("ItemCategoryID", typeof(ItemCategory), null);

        #endregion properties

        #region Commands

        /// <summary>
        /// Gets the ButtonCommandDeleteItem command.
        /// </summary>
        public Command ButtonCommandDeleteItem { get; private set; }
        /// <summary>
        /// Method to check whether the ButtonCommandDeleteItem command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnButtonCommandDeleteItemCanExecute()
        {
            return ItemSelected!=null;
        }

        /// <summary>
        /// Method to invoke when the ButtonCommandDeleteItem command is executed.
        /// </summary>
        private void OnButtonCommandDeleteItemExecute()
        {
            if (ItemSelected != null)
                using (var session = Data.Session.NHibernateSession.OpenSession())
                {
                    try
                    {
                                              

                        session.BeginTransaction();
                        session.Delete(ItemSelected);
                        session.Transaction.Commit();                      
                    }
                    catch (Exception ex) { }
                    ItemSelected.ItemCategoryID = null;
                    _messagemediator.SendMessage<Item>(ItemSelected, "UpdateItemCategoryCollection");
                    ItemCollection.Remove(ItemSelected);

                    
                }
        }

        /// <summary>
        /// Gets the ButtonCommandEditItem command.
        /// </summary>
        public Command ButtonCommandEditItem { get; private set; }


        /// <summary>
        /// Method to check whether the ButtonCommandEditItem command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnButtonCommandEditItemCanExecute()
        {
            return ItemSelected != null;
        }

        /// <summary>
        /// Method to invoke when the ButtonCommandEditItem command is executed.
        /// </summary>
        private void OnButtonCommandEditItemExecute()
        {
            if (ItemSelected != null)
            {
                //_oldItem = ItemSelected.ShallowCopy();
               // ItemCategory category = ItemSelected.ItemCategoryID;
                var uiService = this.GetDependencyResolver();
                var service = uiService.Resolve<UIVisualizerService>();
                var index = ItemCollection.IndexOf(ItemSelected);
                var viewmodel = new AddEditAssetWindowViewModel(ItemCollection[index], new User());

                if (service.ShowDialog(viewmodel) ?? false)
                {
                    var newitem = viewmodel.Item;
                    //if(!CSPropertyCompare.PublicInstancePropertiesEqual<Item>(_oldItem,newitem))
                    //{
                    //    System.Diagnostics.Debug.WriteLine("Object is not equal");
                    //}
                    //else insert to another category     
                    ItemCollection.Remove(newitem);
                    if(newitem.ItemCategoryID.ItemCategoryID!=(_oldcategory.ItemCategoryID))
                    {
                        _messagemediator.SendMessage<Item>(newitem, "UpdateItemCategoryCollection");                    
                       
                    }
                    else ItemCollection.Insert(index, newitem);

                    System.ComponentModel.BackgroundWorker worker = new System.ComponentModel.BackgroundWorker();
                    worker.DoWork += ((o, ea) =>
                        {
                            try
                            {
                                using (var session = NHibernateSession.OpenSession())
                                {
                                    session.BeginTransaction();
                                    session.SaveOrUpdate((object)newitem);
                                    session.Transaction.Commit();
                                }

                            }
                            catch (Exception ex) { }
                        });

                    worker.RunWorkerCompleted += ((o, ea) =>
                        {
                            IsBusy = false;
                        });
                    IsBusy = true;
                    worker.RunWorkerAsync();
                }
            }
        }


        /// <summary>
        /// Gets the ButtonCommandAddItem command.
        /// </summary>
        public Command ButtonCommandAddItem { get; private set; }

        /// <summary>
        /// Method to check whether the ButtonCommandAddItem command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnButtonCommandAddItemCanExecute()
        {
            return true;
        }

        /// <summary>
        /// Method to invoke when the ButtonCommandAddItem command is executed.
        /// </summary>
        private async void OnButtonCommandAddItemExecute()
        {
            var item = new Item();
            item.ItemCategoryID = ItemCategoryID;
           
            var viewmodel = new AddEditAssetWindowViewModel(item, new User());
            var uiService = this.GetDependencyResolver();

            var service = uiService.Resolve<UIVisualizerService>();

          //  var index = ItemCollection.IndexOf(ItemSelected);

            if (await service.ShowDialogAsync(viewmodel) ?? false)
            {
                var newitem = viewmodel.Item;
                //if added to same category
                if (newitem.ItemCategoryID.ItemCategoryID == (_oldcategory.ItemCategoryID))
                {
                   ItemCollection.Add(newitem);
                }
               
                _messagemediator.SendMessage<Item>(newitem, "UpdateItemCategoryCollection");

                System.ComponentModel.BackgroundWorker worker = new System.ComponentModel.BackgroundWorker();
                worker.DoWork += ((o, ea) =>
                {
                    try
                    {
                        using (var session = NHibernateSession.OpenSession())
                        {
                            session.BeginTransaction();
                            session.SaveOrUpdate((object)newitem);
                            session.Transaction.Commit();
                        }

                    }
                    catch (Exception ex) { }
                });

                worker.RunWorkerCompleted += ((o, ea) =>
                {
                    IsBusy = false;
                });
                IsBusy = true;
                worker.RunWorkerAsync();
            }
        }

        #endregion Commands

        #region Methods

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

        [MessageRecipient(Tag = "AssetMasterListIsBusy")]
        private void AssetMasterListIsBusy(bool isbusy)
        {
            IsBusy = isbusy;
        }


        #endregion Methods

    }
}
