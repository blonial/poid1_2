using Microsoft.Win32;
using poid.Commands;
using poid.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace poid.ViewModels
{
    public class ImageViewModel
    {
        #region Properties

        public string Title { get; }

        public Bitmap Image { get; }

        #endregion

        #region Constructors

        public ImageViewModel(string title, Bitmap image)
        {
            this.Title = title;
            this.Image = image;
            this.InitializeCommands();
        }

        #endregion

        #region Initializers

        private void InitializeCommands()
        {
            this._SaveOutput = new RelayCommand(o => this.Image != null, this.SaveOutput);
        }

        #endregion

        #region Commands

        public ICommand _SaveOutput { get; private set; }

        #endregion

        #region Methods

        private void SaveOutput(object o)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save BMP File";
            saveFileDialog.DefaultExt = "bmp";
            saveFileDialog.Filter = "BMP files (*.bmp)|*.bmp";

            if (saveFileDialog.ShowDialog() == true)
            {
                this.Image.Save(saveFileDialog.FileName);
                Notify.Info("Image saved successfully!");
            }
        }

        #endregion
    }
}
