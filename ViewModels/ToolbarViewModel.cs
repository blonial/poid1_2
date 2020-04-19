using poid.Models;
using poid.Models.Interfaces;
using poid.Models.Operations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poid.ViewModels
{
    public class ToolbarViewModel : NotifyPropertyChangedEvent
    {
        #region Properties

        public ObservableCollection<IOperation> Operations { get; } = new ObservableCollection<IOperation>();

        private IOperation _SelectedOperation;

        public IOperation SelectedOperation
        {
            get
            {
                return _SelectedOperation;
            }
            set
            {
                _SelectedOperation = value;
                NotifyPropertyChanged("SelectedOperation");
            }
        }

        #endregion

        #region Constructors

        public ToolbarViewModel(WorkspaceViewModel workspaceViewModel)
        {
            this.InitializeOperations(workspaceViewModel);
        }

        #endregion

        #region Initializers

        private void InitializeOperations(WorkspaceViewModel workspaceViewModel)
        {
            this.Operations.Add(new BrightnessChangeOperation(workspaceViewModel));
            this.Operations.Add(new NegativeOperation(workspaceViewModel));
            this.Operations.Add(new ContrastChangeOperation(workspaceViewModel));
            this.Operations.Add(new BarchartOperation(workspaceViewModel));
            this.Operations.Add(new FilterRemoveNoiseArithmeticAverageOperation(workspaceViewModel));
            this.Operations.Add(new FilterRemoveNoiseMedianOperation(workspaceViewModel));
            this.Operations.Add(new FilterKirschOperatorOperation(workspaceViewModel));
            this.Operations.Add(new FilterEdgeSharpeningOperation(workspaceViewModel));
            this.Operations.Add(new OutputProbabilityDensityOfPowerTo2_3Operation(workspaceViewModel));
            this.Operations.Add(new CustomFilterOperation(workspaceViewModel));
            this.Operations.Add(new RegionSplittingAndMergingOperation(workspaceViewModel));
            this.Operations.Add(new LowPassFilterOperation(workspaceViewModel));
            this.Operations.Add(new HighPassFilterOperation(workspaceViewModel));
            this.Operations.Add(new BandpassFilterOperation(workspaceViewModel));
            this.Operations.Add(new BandbarraggeFilterOperation(workspaceViewModel));
            this.Operations.Add(new FilterWithEdgeDetectionOperation(workspaceViewModel));
            this.Operations.Add(new FilterModifyingPhaseSpectrumOperation(workspaceViewModel));

            this.SelectedOperation = this.Operations[0];
        }

        #endregion
    }
}
