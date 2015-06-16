using Catel.MVVM;
using System.Threading.Tasks;
using Catel.Data;
using Data.Model;
using System.Collections.ObjectModel;
using Catel.IoC;
using Catel.Services;

namespace Asset_Management_System.ViewModels
{


    public class testviewmodel : ViewModelBase
    {
        public testviewmodel()
        {
            additem = new Command(OnadditemExecute, OnadditemCanExecute);

            ItemCollection = new ObservableCollection<TestItem>();

            ItemCollection.Add(new TestItem { ItemBrandName = "Brand1" });

            ItemCollection.Add( new TestItem { ItemBrandName = "Brand2"});

            ItemCollection.Add(new TestItem { ItemBrandName = "Brand3" });




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

        /// <summary>
        /// Gets the additem command.
        /// </summary>
        public Command additem { get; private set; }


        /// <summary>
        /// Method to check whether the additem command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnadditemCanExecute()
        {
            return true;
        }

        private void OnadditemExecute()
        {
            
            var d = this.GetDependencyResolver();
            var service = d.Resolve<IUIVisualizerService>();

            service.Show(new test1viewmodel(Item));

        }

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        public ObservableCollection<TestItem> ItemCollection
        {
            get { return GetValue<ObservableCollection<TestItem>>(ItemCollectionProperty); }
            set { SetValue(ItemCollectionProperty, value); }
        }

        /// <summary>
        /// Register the ItemCollection property so it is known in the class.
        /// </summary>
        public static readonly PropertyData ItemCollectionProperty = RegisterProperty("ItemCollection", typeof(ObservableCollection<TestItem>), null);
        
        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        [Model]
        public TestItem Item
        {
            get { return GetValue<TestItem>(ItemProperty); }
            private set { SetValue(ItemProperty, value); }
        }

        /// <summary>
        /// Register the Item property so it is known in the class.
        /// </summary>
        public static readonly PropertyData ItemProperty = RegisterProperty("Item", typeof(TestItem));
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

    }
}
