using poid.ViewModels;
using poid.ViewModels.Operations;
using poid.Views.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poid.Models.Operations
{
    public class FilterRemoveNoiseMedianOperation : Operation
    {
        #region Constructors

        public FilterRemoveNoiseMedianOperation(WorkspaceViewModel workspaceViewModel) : base("Filter Remove Noise [M]", new FilterRemoveNoiseOperationView(new FilterRemoveNoiseMedianOperationViewModel(workspaceViewModel)))
        {
        }

        #endregion
    }
}
