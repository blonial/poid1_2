using poid.ViewModels;
using poid.Views.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poid.Models.Operations
{
    public class BandbarraggeFilterOperation : Operation
    {
        #region Constructors

        public BandbarraggeFilterOperation(WorkspaceViewModel workspaceViewModel) : base("Bandbarragge filter", new BandbarraggeFilterOperationView(workspaceViewModel))
        {
        }

        #endregion
    }
}
