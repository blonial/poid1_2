using poid.Commands;
using poid.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace poid.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Properties

        private WorkspaceView _SelectedWorkspace;
        public WorkspaceView SelectedWorkspace
        {
            get
            {
                return _SelectedWorkspace;
            }
            private set
            {
                _SelectedWorkspace = value;
                NotifyPropertyChanged("SelectedWorkspace");
            }
        }

        public ObservableCollection<WorkspaceView> _Workspaces = new ObservableCollection<WorkspaceView>();
        public ObservableCollection<WorkspaceView> Workspaces
        {
            get
            {
                return _Workspaces;
            }
            private set
            {
                _Workspaces = value;
                NotifyPropertyChanged("Workspaces");
            }
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Constructors

        public MainViewModel()
        {
            this.InitializeDefaultWorkspace();
            this.InitializeCommands();
        }

        #endregion

        #region Initializers

        private void InitializeCommands()
        {
            this._AddWorkspace = new RelayCommand(o => true, this.AddWorkspace);
            this._RemoveWorkspace = new RelayCommand(o => true, this.RemoveWorkspace);
            this._ChangeWorkspace = new RelayCommand(o => true, this.ChangeWorkspace);
        }

        private void InitializeDefaultWorkspace()
        {
            this.AddWorkspace(null);
        }

        #endregion

        #region Commands

        public ICommand _AddWorkspace { get; private set; }

        public ICommand _RemoveWorkspace { get; private set; }

        public ICommand _ChangeWorkspace { get; private set; }

        #endregion

        #region Methods

        private void AddWorkspace(object o)
        {
            WorkspaceView newWorkspace = new WorkspaceView(this.GetNewWorkspaceIndex());
            this.Workspaces.Add(newWorkspace);
            this.SelectedWorkspace = newWorkspace;
        }

        private int GetNewWorkspaceIndex()
        {
            if (this.Workspaces.Count == 0)
            {
                return 1;
            }
            else
            {
                return this.Workspaces[this.Workspaces.Count - 1].Index + 1;
            }
        }

        private void RemoveWorkspace(object o)
        {
            this.Workspaces.Remove(this.SelectedWorkspace);
            this.SelectedWorkspace = null;
        }

        private void ChangeWorkspace(object o)
        {

        }

        #endregion
    }
}
