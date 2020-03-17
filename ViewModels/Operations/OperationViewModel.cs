using poid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        #endregion
    }
}
