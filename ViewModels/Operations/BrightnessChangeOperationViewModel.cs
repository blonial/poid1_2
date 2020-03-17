using poid.Commands;
using poid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace poid.ViewModels.Operations
{
    public class BrightnessChangeOperationViewModel : OperationViewModel
    {
        #region Properties

        private string _Value;
        public string Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
                NotifyPropertyChanged("Value");
            }
        }

        #endregion

        #region Constructors

        public BrightnessChangeOperationViewModel(WorkspaceViewModel workspaceViewModel) : base(workspaceViewModel)
        {
            this.InitializeCommands();
        }

        #endregion

        #region Initializers

        private void InitializeCommands()
        {
            this._ProcessImage = new RelayCommand(this.ProcessImage);
        }

        #endregion

        #region Commands

        public ICommand _ProcessImage { get; private set; }

        #endregion

        #region Methods

        private void ProcessImage(object sender)
        {

        }

        #endregion
    }
}
