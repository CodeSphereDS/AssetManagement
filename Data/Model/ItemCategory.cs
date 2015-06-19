using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class ItemCategory
    {
        public virtual int ItemCategoryID { get; set; }
        public virtual string ItemCategoryName { get; set; }
        public virtual string ItemCategoryDescription { get; set; }
        public virtual IList<Item> Items { get; set; }
        public ItemCategory()
        {
            Items = new List<Item>();
        }
        public virtual void addItem(Item item)
        {
            item.ItemCategoryID = this;
            Items.Add(item);
        }
        public virtual void removeItem(Item item)
        {
            if(this.Items.Contains(item))
            {
                this.Items.Remove(item);
            }
        }


    }
}
