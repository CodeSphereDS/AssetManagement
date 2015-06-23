using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Data.Model;

namespace Data.Mapping
{
    public class FluentNhibernateMappings
    {
        const string schema = "RAK_ACOAssets";
        public class ItemMap : ClassMap<Item>
        {
            public ItemMap()
            {
                Schema(schema);
                Table("Items");
                //LazyLoad();
                
                Id(x => x.ItemID).GeneratedBy.Native();             
                Map(x => x.ItemBrandName);                       
                Map(x => x.ItemBrandModel);
                Map(x => x.ItemSerialNumber);
                Map(x => x.ItemDetails);
                References(x => x.ItemCategoryID).Column("ItemCategory_id").LazyLoad(Laziness.False).Cascade.None();
                Map(x => x.ItemStatus).Column("ItemStatus").CustomType <GenericEnumMapper<ItemStatus>>().Not.Nullable();                                            //public virtual ItemCategory ItemCategory { get; set; }
                References(x => x.RequestedBy).LazyLoad(Laziness.False).Cascade.None();      
                References(x => x.ApprovedBy).LazyLoad(Laziness.False).Cascade.None();      
                Map(x => x.DatePurchased);                         
                Map(x => x.WarrantyExpiration);                   
                References(x => x.AssignedTo).LazyLoad(Laziness.False).Cascade.None();      
                References(x => x.AddedBy).LazyLoad(Laziness.False).Cascade.None();        
                Map(x => x.DateAdded);                            
                Map(x => x.LastDateModified);                     
                References(x => x.LastModifiedBy).LazyLoad(Laziness.False).Cascade.None();
                HasMany(x => x.Users).KeyColumn("UserID").Inverse().Cascade.SaveUpdate();      
      
            }          
        }
        public class ItemCategoryMap : ClassMap<ItemCategory>
        {
            public ItemCategoryMap()
            {
                Schema(schema);
                Table("ItemCategory");
                //LazyLoad();

                Id(x=>x.ItemCategoryID).GeneratedBy.Native();
                Map(x => x.ItemCategoryName);
                Map(x=>x.ItemCategoryDescription);
                HasMany(x=>x.Items).Inverse().Cascade.None();
            }
            
        }

        public class UserMap : ClassMap<User>
        {
            public UserMap()
            {
                Schema(schema);
                Table("Users");
                //LazyLoad();                
                Id(x => x.UserID).GeneratedBy.Native();
                Map(x => x.Username);    
                Map(x=>x.Password);                        
                         
                Map(x => x.Userlevel);                 
              
            }
        }
    }
}
