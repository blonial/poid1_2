using poid.ViewModels;
using poid.Views.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poid.Models.Operations
{
    public class RegionSplittingAndMergingOperation : Operation
    {
        #region Constructors

        public RegionSplittingAndMergingOperation(WorkspaceViewModel workspaceViewModel) : base("Region splitting and merging", new RegionSplittingAndMergingOperationView(workspaceViewModel))
        {
        }

        #endregion
    }
}
