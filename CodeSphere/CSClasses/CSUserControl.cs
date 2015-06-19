using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MahApps.Metro.Controls;
using Catel.MVVM.Views;
using Catel.Windows.Controls;
using Catel.Data;

namespace CodeSphere.CSClasses
{
    public class CSUserControl : ProgressRing, IUserControl
    {

        public bool CloseViewModelOnUnloaded { get; set; }
        
        public bool DisableWhenNoViewModel { get; set; }

        public bool SkipSearchingForInfoBarMessageControl { get; set; }

        public bool SupportParentViewModelContainers { get; set; }
        
        public Catel.MVVM.IViewModel ViewModel
        {
            get { throw new NotImplementedException(); }
        }

        public new event EventHandler<DataContextChangedEventArgs> DataContextChanged;

        public new event EventHandler<EventArgs> Loaded;

        public new event EventHandler<EventArgs> Unloaded;
             

        public event EventHandler<EventArgs> ViewModelChanged;

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
    }
}
