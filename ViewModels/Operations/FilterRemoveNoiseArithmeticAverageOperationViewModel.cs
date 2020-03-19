using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poid.ViewModels.Operations
{
    public class FilterRemoveNoiseArithmeticAverageOperationViewModel : FilterRemoveNoiseOperationViewModel
    {
        #region Constructors

        public FilterRemoveNoiseArithmeticAverageOperationViewModel(WorkspaceViewModel workspaceViewModel) : base(workspaceViewModel)
        {
        }

        #endregion

        #region Methods

        protected override int CalculatePixelValue(IEnumerable<int> pixels)
        {
            int sum = 0;
            int count = 0;
            foreach(int pixel in pixels)
            {
                sum += pixel;
                count++;
            }
            return sum / count;
        }

        #endregion
    }
}
