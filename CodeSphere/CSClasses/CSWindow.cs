using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MahApps.Metro.Controls;
using Catel.MVVM.Views;
using Catel.MVVM;
using Catel.MVVM.Providers;
using System.ComponentModel;
using Catel;
using Catel.Windows;

namespace CodeSphere.CSClasses
{
    public class CSWindow : MetroWindow, IDataWindow
    {
        private readonly WindowLogic _logic;

        public event EventHandler<DataContextChangedEventArgs> _viewDataContextChanged;
        public event EventHandler<EventArgs> _viewLoaded;
        public event EventHandler<EventArgs> _viewUnloaded;
        public event EventHandler<EventArgs> ViewModelChanged;
        public event PropertyChangedEventHandler PropertyChanged;
        //public event PropertyChangedEventHandler AddDataContextChangedHandler;

       //public CSWindow() { }
        public CSWindow(IViewModel viewModel)
        {
           
            _logic = new WindowLogic(this, null, viewModel);
            _logic.ViewModelChanged += (sender, e) => ViewModelChanged.SafeInvoke(this, e);
            _logic.PropertyChanged += (sender, e) => PropertyChanged.SafeInvoke(this, e);
            Loaded += (sender, e) => _viewLoaded.SafeInvoke(this);
            Unloaded += (sender, e) => _viewUnloaded.SafeInvoke(this);
            this.AddDataContextChangedHandler((sender,e)=> _viewDataContextChanged.SafeInvoke(this,new DataContextChangedEventArgs(e.OldValue,e.NewValue)));
           
            // Because the RadWindow does not close when DialogResult is set, the following code is required
            ViewModelChanged += (sender, e) => OnViewModelChanged();

            // Call manually the first time (for injected view models)
            OnViewModelChanged();

            // WindowStartupLocation = WindowStartupLocation.
            // SetBinding(RadWindow.HeaderProperty, new Binding("Title"));
            //this.RightWindowCommandsOverlayBehavior = WindowCommandsOverlayBehavior.Always;
            //          this.LeftWindowCommandsOverlayBehavior = WindowCommandsOverlayBehavior.Always;
            
           
        }

        
        public IViewModel ViewModel
        {
            get {              
                return _logic.ViewModel;               
            }
        }


        event EventHandler<EventArgs> IView.Loaded
        {
            add { _viewLoaded += value; }
            remove { _viewLoaded -= value; }
        }

        event EventHandler<EventArgs> IView.Unloaded
        {
            add { _viewUnloaded += value; }
            remove { _viewUnloaded -= value; }
        }

        event EventHandler<DataContextChangedEventArgs> IView.DataContextChanged
        {
            add { _viewDataContextChanged += value; }
            remove { _viewDataContextChanged -= value; }
        }

        private void OnViewModelChanged()
        {
            if (ViewModel != null && !ViewModel.IsClosed)
            {
                ViewModel.Closed += ViewModelClosed;
            }
        }


        private void ViewModelClosed(object sender, ViewModelClosedEventArgs e)
        {
            Close();
        }

        //protected virtual Type GetViewModelType()
        //{
        //    return null;
        //}


    }
}
