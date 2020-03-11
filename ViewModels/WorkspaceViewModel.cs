using Microsoft.Win32;
using poid.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace poid.ViewModels
{
    public class WorkspaceViewModel : INotifyPropertyChanged
    {
        #region Properties

        public int Index { get; }

        private Bitmap _Input;
        public Bitmap Input
        {
            get
            {
                return _Input;
            }
            private set
            {
                _Input = value;
                NotifyPropertyChanged("Input");
            }
        }

        private Bitmap _Output;
        public Bitmap Output
        {
            get
            {
                return _Output;
            }
            private set
            {
                _Output = value;
                NotifyPropertyChanged("Output");
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

        public WorkspaceViewModel(int index)
        {
            this.Index = index;
            this.InitializeCommands();
        }

        #endregion

        #region Initializers

        private void InitializeCommands()
        {
            this._LoadImage = new RelayCommand(this.LoadImage);
            this._SaveOutput = new RelayCommand(this.SaveOutput);
        }

        #endregion

        #region Commands

        public ICommand _LoadImage { get; private set; }

        public ICommand _SaveOutput { get; private set; }

        #endregion

        #region Methods

        private void LoadImage(object o)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Browse BMP Files";
            openFileDialog.DefaultExt = "bmp";
            openFileDialog.Filter = "BMP files (*.bmp)|*.bmp";

            if (openFileDialog.ShowDialog() == true)
            {
                this.Input = new Bitmap(openFileDialog.FileName);
                this.Output = null;
            }
        }

        private void SaveOutput(object o)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save BMP File";
            saveFileDialog.DefaultExt = "bmp";
            saveFileDialog.Filter = "BMP files (*.bmp)|*.bmp";

            if (saveFileDialog.ShowDialog() == true)
            {
                this.Output.Save(saveFileDialog.FileName);
            }
        }

        #endregion
    }
}
