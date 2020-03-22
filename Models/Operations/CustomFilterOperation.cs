using poid.ViewModels;
using poid.Views.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poid.Models.Operations
{
    public class CustomFilterOperation : Operation
    {
        #region Constructors

        public CustomFilterOperation(WorkspaceViewModel workspaceViewModel) : base("Custom filter", new CustomFilterOperationView(workspaceViewModel))
        {
        }

        #endregion
    }
}
