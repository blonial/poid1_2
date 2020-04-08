using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poid.ViewModels.Operations
{
    public class RegionSplittingAndMergingOperationViewModel : OperationViewModel
    {
        #region Properties

        private string _Splitting;
        public string Splitting
        {
            get
            {
                return _Splitting;
            }
            set
            {
                _Splitting = value;
                NotifyPropertyChanged("Splitting");
            }
        }

        private string _Merging;
        public string Merging
        {
            get
            {
                return _Merging;
            }
            set
            {
                _Merging = value;
                NotifyPropertyChanged("Merging");
            }
        }

        #endregion

        #region Constructors

        public RegionSplittingAndMergingOperationViewModel(WorkspaceViewModel workspaceViewModel) : base(workspaceViewModel)
        {
        }

        #endregion

        #region Methods

        protected override void ProcessImage(object sender)
        {

        }

        #endregion
    }
}
