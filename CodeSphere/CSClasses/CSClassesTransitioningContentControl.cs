using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MahApps.Metro.Controls;
using Catel.MVVM.Views;
using Catel.MVVM;
using Catel.MVVM.Providers;
using Catel;
using Catel.Windows;
using System.ComponentModel;
using System.Windows.Data;

namespace CodeSphere.CSClasses
{
    public class CSClassesTransitioningContentControl : TransitioningContentControl , IUserControl
    {
        private readonly UserControlLogic _logic;

        private event EventHandler<EventArgs> _viewLoaded;
        private event EventHandler<EventArgs> _viewUnloaded;
        private event EventHandler<Catel.MVVM.Views.DataContextChangedEventArgs> _viewDataContextChanged;
        private event EventHandler<EventArgs> _viewModelChanged;
        public event PropertyChangedEventHandler PropertyChanged;
      
        public bool DisableWhenNoViewModel { get; set; }
        public bool SkipSearchingForInfoBarMessageControl { get; set; }
        public bool SupportParentViewModelContainers { get; set; }
        public bool CloseViewModelOnUnloaded { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="TabItem"/> class.
        /// </summary>
        public CSClassesTransitioningContentControl()         
            :base()
        {
            _logic = new UserControlLogic(this,typeof(IViewModel),null);
            _logic.PropertyChanged += (sender, e) => PropertyChanged.SafeInvoke(this, e);

            Loaded += (sender, e) => _viewLoaded.SafeInvoke(this);
            Unloaded += (sender, e) => _viewUnloaded.SafeInvoke(this);
            this.AddDataContextChangedHandler((sender, e) => 
                _viewDataContextChanged.SafeInvoke(this, new Catel.MVVM.Views.DataContextChangedEventArgs(e.OldValue, e.NewValue)));

            //SetBinding(this.Tag, new Binding("Title"));

        }


        /// <summary>
        /// Gets the view model that is contained by the container.
        /// </summary>
        /// <value>The view model.</value>
        public IViewModel ViewModel
        {
            get { return _logic.ViewModel; }
        }

      
        /// <summary>
        /// Occurs when the view is loaded.
        /// </summary>
        event EventHandler<EventArgs> IView.Loaded
        {
            add { _viewLoaded += value; }
            remove { _viewLoaded -= value; }
        }

        /// <summary>
        /// Occurs when the view is unloaded.
        /// </summary>
        event EventHandler<EventArgs> IView.Unloaded
        {
            add { _viewUnloaded += value; }
            remove { _viewUnloaded -= value; }
        }

        /// <summary>
        /// Occurs when the data context has changed.
        /// </summary>
        event EventHandler<Catel.MVVM.Views.DataContextChangedEventArgs> IView.DataContextChanged
        {
            add { _viewDataContextChanged += value; }
            remove { _viewDataContextChanged -= value; }
        }

        event EventHandler<EventArgs> IViewModelContainer.ViewModelChanged
        {
            add { _viewModelChanged += value; }
            remove { _viewModelChanged -= value; }
        }




        public Task<bool> CancelAndCloseViewModel()
        {
            throw new NotImplementedException();
        }

        public Task<bool> CancelViewModel()
        {
            throw new NotImplementedException();
        }

        public event EventHandler<EventArgs> Canceled;

        public event EventHandler<CancelingEventArgs> Canceling;

        public Task CloseViewModel(bool? result)
        {
            throw new NotImplementedException();
        }

        public event EventHandler<ViewModelClosedEventArgs> Closed;

        public event EventHandler<EventArgs> Closing;

        public event EventHandler<CommandExecutedEventArgs> CommandExecuted;

        public bool HasDirtyModel
        {
            get { throw new NotImplementedException(); }
        }

        public Task InitializeViewModel()
        {
            throw new NotImplementedException();
        }

        public new event EventHandler<EventArgs> Initialized;

        public bool IsClosed
        {
            get { throw new NotImplementedException(); }
        }

        public Task<bool> SaveAndCloseViewModel()
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveViewModel()
        {
            throw new NotImplementedException();
        }

        public event EventHandler<EventArgs> Saved;

        public event EventHandler<SavingEventArgs> Saving;

        public string Title
        {
            get { throw new NotImplementedException(); }
        }

        public int UniqueIdentifier
        {
            get { throw new NotImplementedException(); }
        }

        public bool ValidateViewModel(bool force = false, bool notifyChangedPropertiesOnly = true)
        {
            throw new NotImplementedException();
        }

        public DateTime ViewModelConstructionTime
        {
            get { throw new NotImplementedException(); }
        }
    }
}
