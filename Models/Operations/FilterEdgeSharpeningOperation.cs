using poid.ViewModels;
using poid.Views.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poid.Models.Operations
{
    class FilterEdgeSharpeningOperation : Operation
    {
        #region Constructors

        public FilterEdgeSharpeningOperation(WorkspaceViewModel workspaceViewModel) : base("Edge sharpening", new FilterEdgeSharpeningOperationView(workspaceViewModel))
        {
        }

        #endregion
    }
}
