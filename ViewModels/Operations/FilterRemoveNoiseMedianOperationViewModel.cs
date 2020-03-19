using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poid.ViewModels.Operations
{
    public class FilterRemoveNoiseMedianOperationViewModel : FilterRemoveNoiseOperationViewModel
    {
        #region Constructors

        public FilterRemoveNoiseMedianOperationViewModel(WorkspaceViewModel workspaceViewModel) : base(workspaceViewModel)
        {
        }

        #endregion

        #region Methods

        protected override int CalculatePixelValue(IEnumerable<int> pixels)
        {
            List<int> orderedPixels = new List<int>(pixels);
            orderedPixels.Sort();
            if (orderedPixels.Count % 2 == 1)
            {
                return orderedPixels[orderedPixels.Count / 2];
            } else
            {
                int lower = orderedPixels.Count;
                return (orderedPixels[lower] + orderedPixels[lower + 1]) / 2;
            } 
        }

        #endregion
    }
}
