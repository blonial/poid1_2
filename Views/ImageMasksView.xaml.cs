using poid.Models;
using poid.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace poid.Views
{
    /// <summary>
    /// Logika interakcji dla klasy ImageMasksView.xaml
    /// </summary>
    public partial class ImageMasksView : Window
    {
        public ImageMasksView(List<RegionMask> masks)
        {
            this.DataContext = new ImageMasksViewModel(masks);
            InitializeComponent();
        }
    }
}
