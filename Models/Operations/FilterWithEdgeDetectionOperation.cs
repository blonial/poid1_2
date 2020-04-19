using poid.ViewModels;
using poid.Views.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poid.Models.Operations
{
    public class FilterWithEdgeDetectionOperation : Operation
    {
        #region Constructors

        public FilterWithEdgeDetectionOperation(WorkspaceViewModel workspaceViewModel) : base("Filter with edge detection", new FilterWithEdgeDetectionOperationView(workspaceViewModel))
        {
        }

        #endregion
    }
}
