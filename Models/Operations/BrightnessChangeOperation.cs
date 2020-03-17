using poid.ViewModels;
using poid.Views.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace poid.Models.Operations
{
    public class BrightnessChangeOperation : Operation
    {
        #region Constructors

        public BrightnessChangeOperation(WorkspaceViewModel workspaceViewModel) : base("Brightness", new BrightnessChangeOperationView(workspaceViewModel))
        {
        }

        #endregion
    }
}
