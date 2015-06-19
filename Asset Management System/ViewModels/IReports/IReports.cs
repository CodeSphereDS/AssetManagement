using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace Asset_Management_System.ViewModels.IReports
{
    public interface IReports<T>
    {
        IList<T> ItemCategory { get; set; }
    }
}
