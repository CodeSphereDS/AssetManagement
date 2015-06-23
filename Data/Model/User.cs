using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
   public class User
    {
     
       public virtual int UserID { get; set; }
       public virtual string Username { get; set; }
       public virtual string Password { get; set; }     
       public virtual bool UserIsAuthenticated { get; set; }
       public virtual int Userlevel { get; set; }
       public virtual IList<Item> Items { get; set; }

       public User()
       {
           Items = new List<Item>();

       }


    }
}
