using poid.ViewModels;
using poid.ViewModels.Operations;
using poid.Views.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poid.Models.Operations
{
    public class FilterRemoveNoiseArithmeticAverageOperation : Operation
    {
        #region Constructors

        public FilterRemoveNoiseArithmeticAverageOperation(WorkspaceViewModel workspaceViewModel) : base("Filter Remove Noise [AVG]", new FilterRemoveNoiseOperationView(new FilterRemoveNoiseArithmeticAverageOperationViewModel(workspaceViewModel)))
        {
        }

        #endregion
    }
}
