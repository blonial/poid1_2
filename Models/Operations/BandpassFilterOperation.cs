using poid.ViewModels;
using poid.Views.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poid.Models.Operations
{
    public class BandpassFilterOperation : Operation
    {
        #region Constructors

        public BandpassFilterOperation(WorkspaceViewModel workspaceViewModel) : base("Bandpass filter", new BandpassFilterOperationView(workspaceViewModel))
        {
        }

        #endregion
    }
}
