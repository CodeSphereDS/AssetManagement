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
                
                Id(x => x.ItemID).GeneratedBy.Native();             //public virtual int ItemID{get;set;}
                Map(x => x.ItemBrandName);                          //public virtual string ItemDescription { get; set; }
                Map(x => x.ItemBrandModel);
                Map(x => x.ItemSerialNumber);
                Map(x => x.ItemDetails);
                References(x => x.ItemCategoryID).Column("ItemCategory_id").LazyLoad(Laziness.False).Cascade.None();
                Map(x => x.ItemStatus).Column("ItemStatus").CustomType <GenericEnumMapper<ItemStatus>>().Not.Nullable();                                            //public virtual ItemCategory ItemCategory { get; set; }
                References(x => x.RequestedBy).LazyLoad(Laziness.False).Cascade.None();      //public virtual User RequestdBy { get; set; }
                References(x => x.ApprovedBy).LazyLoad(Laziness.False).Cascade.None();       //public virtual User ApprovedBy { get; set; }
                Map(x => x.DatePurchased);                          //public virtual DateTime DatePurchased { get; set; }
                Map(x => x.WarrantyExpiration);                     //public virtual DateTime WarrantyExpiration { get; set; }
                References(x => x.AssignedTo).LazyLoad(Laziness.False).Cascade.None();       //public virtual User AssignedTo { get; set; }
                References(x => x.AddedBy).LazyLoad(Laziness.False).Cascade.None();          //public virtual User AddedBy { get; set; }
                Map(x => x.DateAdded);                              //public virtual DateTime DateAdded { get; set; }
                Map(x => x.LastDateModified);                       //public virtual DateTime LastDateModefified { get; set; }
                References(x => x.LastModifiedBy).LazyLoad(Laziness.False).Cascade.None();   //public virtual User LastModifiedBy { get; set; }
                HasMany(x => x.Users).KeyColumn("UserID").Inverse().Cascade.None();
               // HasMany(x => x.Categories).KeyColumn("ItemCategoryID").Inverse().Cascade.None();
                

        //public virtual int ItemID{get;set;}
        //public virtual string ItemDescription { get; set; }
        //public virtual ItemCategory ItemCategory { get; set; }
        //public virtual User RequestdBy { get; set; }
        //public virtual User ApprovedBy { get; set; }
        //public virtual DateTime DatePurchased { get; set; }
        //public virtual DateTime WarrantyExpiration { get; set; }
        //public virtual User AssignedTo { get; set; }
        //public virtual User AddedBy { get; set; }
        //public virtual DateTime DateAdded { get; set; }
        //public virtual DateTime LastDateModefified { get; set; }
        //public virtual User LastModifiedBy { get; set; }
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
                Id(x => x.UserID).GeneratedBy.Native();                  //public virtual int UserID { get; set; }
                Map(x => x.Username);               //public virtual string Username { get; set; }
                Map(x=>x.Password);                 //public virtual string Password { get; set; }
                Map(x => x.Lastname);               //public virtual string Lastname { get; set; }
                Map(x => x.Firstname);              //public virtual String Firstname { get; set; }
                Map(x => x.MiddleName);             //public virtual string MiddleName { get; set; }
                Map(x => x.Userlevel);               //public virtual int Userlevel { get; set; }
                //HasManyToMany(x => x.Items).Inverse().Cascade.None();
            }
        }

        //public class ItemStatusMap : ClassMap<ItemStatus>
        //{
        //    //public ItemStatusMap()
        //    //{
        //    //    Schema("ItemStatus");
        //    //    Table("ItemStatus");
        //    //    LazyLoad();

        //    //    Id(x => x.ItemStatusID);
        //    //    Map(x => x.ItemStatusName);              

        //    //}
        //}
    }
}
