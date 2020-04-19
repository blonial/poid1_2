using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        #endregion
    }
}
