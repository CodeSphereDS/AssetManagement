using Catel.Data;
using Catel.MVVM.Views;
using Catel.MVVM;
using Catel;
using Data.Model;
using System.Windows;

namespace Asset_Management_System.Views.Reports
{
    public partial class AssetMasterListItem : IUserControl
    {
        public AssetMasterListItem()
            : this(null) { }
        public AssetMasterListItem(ViewModels.Reports.AssetMasterListItemViewModel viewmodel)
        :base(viewmodel)
        {
            
            InitializeComponent();
        }



        [ViewToViewModel(MappingType = ViewToViewModelMappingType.ViewModelToView)]
        public ItemCategory ItemCategoryID
        {
            get { return (ItemCategory)GetValue(ItemCategoryIDProperty); }
            set { SetValue(ItemCategoryIDProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MapCenter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemCategoryIDProperty =
            DependencyProperty.Register("ItemCategoryID", typeof(ItemCategory),
            typeof(AssetMasterListItem), new PropertyMetadata(null, (sender, e) => 
                ((AssetMasterListItem)sender).ItemCategoryIDChanged()));
        public void ItemCategoryIDChanged()
        {

        }




        

    }
}
