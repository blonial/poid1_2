using poid.ViewModels;
using poid.Views.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poid.Models.Operations
{
    public class BarchartOperation : Operation
    {
        #region Constructors

        public BarchartOperation(WorkspaceViewModel workspaceViewModel) : base("Barchart", new BarchartOperationView(workspaceViewModel))
        {
        }

        #endregion
    }
}
