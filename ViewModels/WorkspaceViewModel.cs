using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace poid.ViewModels
{
    public class WorkspaceViewModel : INotifyPropertyChanged
    {
        #region Properties

        public int Index { get; }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Constructors

        public WorkspaceViewModel(int index)
        {
            this.Index = index;
        }

        #endregion

        #region Methods



        #endregion
    }
}
