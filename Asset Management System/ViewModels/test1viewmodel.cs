using Catel.MVVM;
using System.Threading.Tasks;
using Catel.Data;
using Data.Model;


namespace Asset_Management_System.ViewModels
{
   
    public class test1viewmodel : ViewModelBase
    {
        public test1viewmodel(TestItem item)
        {
            Item = item;
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
        /// Gets or sets the property value.
        /// </summary>
        [Model]
        public TestItem Item
        {
            get { return GetValue<TestItem>(ItemProperty); }
            set { SetValue(ItemProperty, value); }
        }

        /// <summary>
        /// Register the Item property so it is known in the class.
        /// </summary>
        public static readonly PropertyData ItemProperty = RegisterProperty("Item", typeof(TestItem), null);

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
