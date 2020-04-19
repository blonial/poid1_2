using poid.ViewModels;
using poid.Views.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poid.Models.Operations
{
    public class HighPassFilterOperation : Operation
    {
        #region Constructors

        public HighPassFilterOperation(WorkspaceViewModel workspaceViewModel) : base("High pass filter", new HighPassFilterOperationView(workspaceViewModel))
        {
        }

        #endregion
    }
}
