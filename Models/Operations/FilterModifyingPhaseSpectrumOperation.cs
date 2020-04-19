using poid.ViewModels;
using poid.Views.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poid.Models.Operations
{
    public class FilterModifyingPhaseSpectrumOperation : Operation
    {
        #region Constructors

        public FilterModifyingPhaseSpectrumOperation(WorkspaceViewModel workspaceViewModel) : base("Filter modifying phase spectrum", new FilterModifyingPhaseSpectrumOperationView(workspaceViewModel))
        {
        }

        #endregion
    }
}
