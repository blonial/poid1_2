using Microsoft.Win32;
using poid.Commands;
using poid.Models;
using poid.Views;
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
    public class WorkspaceViewModel : NotifyPropertyChangedEvent
    {
        #region Properties

        public int Index { get; }

        public ToolbarView ToolbarView { get; }

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
            set
            {
                _Output = value;
                NotifyPropertyChanged("Output");
            }
        }

        private System.Drawing.Size _InputSize;
        public System.Drawing.Size InputSize
        {
            get
            {
                return _InputSize;
            }
            private set
            {
                _InputSize = value;
                NotifyPropertyChanged("InputSize");
            }
        }

        private System.Drawing.Size _OutputSize;
        public System.Drawing.Size OutputSize
        {
            get
            {
                return _OutputSize;
            }
            private set
            {
                _OutputSize = value;
                NotifyPropertyChanged("OutputSize");
            }
        }

        #endregion

        #region Constructors

        public WorkspaceViewModel(int index)
        {
            this.Index = index;
            this.ToolbarView = new ToolbarView(this);
            this.InitializeCommands();
            this.InitializeEventListeners();
        }

        #endregion

        #region Initializers

        private void InitializeCommands()
        {
            this._LoadImage = new RelayCommand(this.LoadImage);
            this._SaveOutput = new RelayCommand(this.SaveOutput);
            this._ScaleInput_0_5 = new RelayCommand(this.ScaleInput_0_5);
            this._ScaleInput_1_0 = new RelayCommand(this.ScaleInput_1_0);
            this._ScaleInput_2_0 = new RelayCommand(this.ScaleInput_2_0);
            this._ScaleOutput_0_5 = new RelayCommand(this.ScaleOutput_0_5);
            this._ScaleOutput_1_0 = new RelayCommand(this.ScaleOutput_1_0);
            this._ScaleOutput_2_0 = new RelayCommand(this.ScaleOutput_2_0);
        }

        private void InitializeEventListeners()
        {
            this.PropertyChanged += HandleInputChange;
            this.PropertyChanged += HandleOutputChange;
        }

        #endregion

        #region Commands

        public ICommand _LoadImage { get; private set; }

        public ICommand _SaveOutput { get; private set; }

        public ICommand _ScaleInput_0_5 { get; private set; }

        public ICommand _ScaleInput_1_0 { get; private set; }

        public ICommand _ScaleInput_2_0 { get; private set; }

        public ICommand _ScaleOutput_0_5 { get; private set; }

        public ICommand _ScaleOutput_1_0 { get; private set; }

        public ICommand _ScaleOutput_2_0 { get; private set; }

        #endregion

        #region Event listeners

        private void HandleInputChange(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Input")
            {
                if (this.Input != null)
                {
                    this.InputSize = new System.Drawing.Size(this.Input.Size.Width, this.Input.Size.Height);
                }
                else
                {
                    this.InputSize = System.Drawing.Size.Empty;
                }
            }
        }

        private void HandleOutputChange(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Output")
            {
                if (this.Output != null)
                {
                    this.OutputSize = new System.Drawing.Size(this.Output.Size.Width, this.Output.Size.Height);
                }
                else
                {
                    this.OutputSize = System.Drawing.Size.Empty;
                }
            }
        }

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

        private void ScaleInput_0_5(object o)
        {
            if (this.InputSize != System.Drawing.Size.Empty)
            {
                this.InputSize = new System.Drawing.Size(Convert.ToInt32(this.InputSize.Width * 0.5), Convert.ToInt32(this.InputSize.Height * 0.5));
            }
        }

        private void ScaleInput_1_0(object o)
        {
            if (this.Input != null)
            {
                this.InputSize = new System.Drawing.Size(this.Input.Size.Width, this.Input.Size.Height);
            }
        }

        private void ScaleInput_2_0(object o)
        {
            if (this.InputSize != System.Drawing.Size.Empty)
            {
                this.InputSize = new System.Drawing.Size(this.InputSize.Width * 2, this.InputSize.Height * 2);
            }
        }

        private void ScaleOutput_0_5(object o)
        {
            if (this.OutputSize != System.Drawing.Size.Empty)
            {
                this.OutputSize = new System.Drawing.Size(Convert.ToInt32(this.OutputSize.Width * 0.5), Convert.ToInt32(this.OutputSize.Height * 0.5));
            }
        }

        private void ScaleOutput_1_0(object o)
        {
            if (this.Output != null)
            {
                this.OutputSize = new System.Drawing.Size(this.Output.Size.Width, this.Output.Size.Height);
            }
        }

        private void ScaleOutput_2_0(object o)
        {
            if (this.OutputSize != System.Drawing.Size.Empty)
            {
                this.OutputSize = new System.Drawing.Size(this.OutputSize.Width * 2, this.OutputSize.Height * 2);
            }
        }

        public Bitmap GetClonedInput()
        {
            return this.Input.Clone(new Rectangle(0, 0, this.Input.Width, this.Input.Height), this.Input.PixelFormat);
        }

        #endregion
    }
}
