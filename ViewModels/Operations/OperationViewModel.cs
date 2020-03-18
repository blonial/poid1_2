using poid.Commands;
using poid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace poid.ViewModels.Operations
{
    public abstract class OperationViewModel : NotifyPropertyChangedEvent
    {
        #region Properties

        protected WorkspaceViewModel WorkspaceViewModel { get; }

        private bool _Working = false;
        public bool Working
        {
            get
            {
                return _Working;
            }
            private set
            {
                _Working = value;
                NotifyPropertyChanged("Working");
            }
        }

        #endregion

        #region Constructors

        public OperationViewModel(WorkspaceViewModel workspaceViewModel)
        {
            this.WorkspaceViewModel = workspaceViewModel;
            this.InitializeCommands();
        }

        #endregion

        #region Initializers

        private void InitializeCommands()
        {
            this._ProcessImage = new RelayCommand(o => this.WorkspaceViewModel.Input != null, this.ProcessImageDecorator);
        }

        #endregion

        #region Commands

        public ICommand _ProcessImage { get; private set; }

        #endregion

        #region Methods

        private void ProcessImageDecorator(object sender)
        {
            this.Working = true;
            Task processImageTask = new Task(new Action(() => {
                this.ProcessImage(sender);
                this.Working = false;
            }));
            processImageTask.Start();
            processImageTask.Wait();
        }

        protected abstract void ProcessImage(object sender);

        #endregion
    }
}
