using poid.ViewModels;
using poid.Views.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poid.Models.Operations
{
    public class NegativeOperation : Operation
    {
        #region Constructors

        public NegativeOperation(WorkspaceViewModel workspaceViewModel) : base("Negative", new NegativeOperationView(workspaceViewModel))
        {
        }

        #endregion
    }
}
