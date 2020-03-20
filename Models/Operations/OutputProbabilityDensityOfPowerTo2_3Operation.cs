using poid.ViewModels;
using poid.Views.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poid.Models.Operations
{
    public class OutputProbabilityDensityOfPowerTo2_3Operation : Operation
    {
        #region Constructors

        public OutputProbabilityDensityOfPowerTo2_3Operation(WorkspaceViewModel workspaceViewModel) : base("Output probability density of power to 2/3", new OutputProbabilityDensityOfPowerTo2_3OperationView(workspaceViewModel))
        {
        }

        #endregion
    }
}
