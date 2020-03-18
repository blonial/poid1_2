using poid.ViewModels;
using poid.Views.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poid.Models.Operations
{
    public class ContrastChangeOperation : Operation
    {
        #region Constructors

        public ContrastChangeOperation(WorkspaceViewModel workspaceViewModel) : base("Contrast", new ContrastChangeOperationView(workspaceViewModel))
        {
        }

        #endregion
    }
}
