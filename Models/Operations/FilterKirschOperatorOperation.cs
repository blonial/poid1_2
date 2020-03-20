using poid.ViewModels;
using poid.Views.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poid.Models.Operations
{
    public class FilterKirschOperatorOperation : Operation
    {
        #region Constructors

        public FilterKirschOperatorOperation(WorkspaceViewModel workspaceViewModel) : base("Kirsch Operator", new FilterKirschOperatorOperationView(workspaceViewModel))
        {
        }

        #endregion
    }
}
