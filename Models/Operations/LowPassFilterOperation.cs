using poid.ViewModels;
using poid.Views.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poid.Models.Operations
{
    public class LowPassFilterOperation : Operation
    {
        #region Constructors

        public LowPassFilterOperation(WorkspaceViewModel workspaceViewModel) : base("Low pass filter", new LowPassFilterOperationView(workspaceViewModel))
        {
        }

        #endregion
    }
}
