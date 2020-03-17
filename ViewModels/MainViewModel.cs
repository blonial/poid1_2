using poid.Commands;
using poid.Models;
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
    public class MainViewModel : NotifyPropertyChangedEvent
    {
        #region Properties

        private Workspace _SelectedWorkspace;
        public Workspace SelectedWorkspace
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

        public ObservableCollection<Workspace> _Workspaces = new ObservableCollection<Workspace>();
        public ObservableCollection<Workspace> Workspaces
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
            this._AddWorkspace = new RelayCommand(this.AddWorkspace);
            this._RemoveWorkspace = new RelayCommand(this.RemoveWorkspace);
            this._ChangeWorkspace = new RelayCommand(this.ChangeWorkspace);
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
            Workspace newWorkspace = new Workspace(this.GetNewWorkspaceIndex());
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
            if(this.SelectedWorkspace != null)
            {
                int index = this.Workspaces.IndexOf(this.SelectedWorkspace);
                this.Workspaces.Remove(this.SelectedWorkspace);
                this.SelectedWorkspace = null;
                if(this.Workspaces.Count > 0)
                {
                    if(this.Workspaces.Count > index)
                    {
                        this.SelectedWorkspace = this.Workspaces[index];
                    } else
                    {
                        this.SelectedWorkspace = this.Workspaces[this.Workspaces.Count - 1];
                    }
                }
            }
        }

        private void ChangeWorkspace(object o)
        {
            if(o is Workspace) {
                this.SelectedWorkspace = this.Workspaces[this.Workspaces.IndexOf((Workspace)o)];
            }
        }

        #endregion
    }
}
