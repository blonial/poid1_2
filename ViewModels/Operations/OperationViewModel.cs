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
            this._ProcessImage = new RelayCommand(o => this.WorkspaceViewModel.Input != null, this.ProcessImage);
        }

        #endregion

        #region Commands

        public ICommand _ProcessImage { get; private set; }

        #endregion

        #region Methods

        protected abstract void ProcessImage(object sender);

        #endregion
    }
}
