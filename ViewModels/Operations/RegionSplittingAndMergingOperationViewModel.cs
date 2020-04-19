using poid.Models;
using poid.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace poid.ViewModels.Operations
{
    public class RegionSplittingAndMergingOperationViewModel : OperationViewModel
    {
        #region Properties

        private string _Splitting = "20";
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

        private string _Merging = "20";
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
            try
            {
                int splitting = int.Parse(this.Splitting);
                int merging = int.Parse(this.Merging);
                if (splitting < 0 || merging < 0 || splitting > 255 || merging > 255)
                {
                    throw new ArgumentException("Invalid input value!\nSplitting and merging musts be an ints between 0 and 255.");
                }
                List<RegionMask> masks = RegionMask.SplitAndMergeImageRegions(this.WorkspaceViewModel.Input, splitting, merging);

                Task.Run(() =>
                {
                    Application.Current.Dispatcher.Invoke(delegate
                    {
                        Window imageMasksWindow = new ImageMasksView(masks);
                        imageMasksWindow.Show();
                    });
                });
            }
            catch (Exception e)
            {
                Notify.Error(e.Message);
            }
        }

        #endregion
    }
}
