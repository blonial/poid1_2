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
    /// Interaction logic for MaskCreatorView.xaml
    /// </summary>
    public partial class MaskCreatorView : Window
    {
        public MaskCreatorViewModel MaskCreatorViewModel { get; }

        public MaskCreatorView(int x, int y, string name)
        {
            this.MaskCreatorViewModel = new MaskCreatorViewModel(x, y, name, this);
            this.DataContext = this.MaskCreatorViewModel;
            InitializeComponent();
            this.MaskCreatorViewModel.InitializeGrid();
        }
    }
}
