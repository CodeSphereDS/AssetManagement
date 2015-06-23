using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class Item
    {
        public virtual int ItemID { get; set; }
        public virtual string ItemBrandName { get; set; }
        public virtual string ItemBrandModel { get; set; }
        public virtual string ItemSerialNumber { get; set; }
        public virtual string ItemDetails { get; set; }
        public virtual ItemStatus ItemStatus { get; set; }
        public virtual ItemCategory ItemCategoryID { get; set; }
        public virtual User RequestedBy { get; set; }
        public virtual User ApprovedBy { get; set; }
        public virtual DateTime? DatePurchased { get; set; }
        public virtual DateTime? WarrantyExpiration { get; set; }
        public virtual User AssignedTo { get; set; }
        public virtual User AddedBy { get; set; }
        public virtual DateTime? DateAdded { get; set; }
        public virtual DateTime? LastDateModified { get; set; }
        public virtual User LastModifiedBy { get; set; }
        public virtual IList<User> Users { get; set; }

        public Item()
        {
            Users = new List<User>();
        }
        public virtual Item ShallowCopy()
        {
            return (Item)this.MemberwiseClone();
        }
    }
}
