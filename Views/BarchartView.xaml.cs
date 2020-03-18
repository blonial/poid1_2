using LiveCharts;
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
    /// Logika interakcji dla klasy BarchartView.xaml
    /// </summary>
    public partial class BarchartView : Window
    {
        public BarchartView(SeriesCollection series, ICollection<string> labels)
        {
            this.DataContext = new BarchartViewModel(series, labels);
            InitializeComponent();
        }
    }
}
