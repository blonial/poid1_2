using poid.ViewModels;
using poid.ViewModels.Operations;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace poid.Views.Operations
{
    /// <summary>
    /// Logika interakcji dla klasy FilterRemoveNoiseOperationView.xaml
    /// </summary>
    public partial class FilterRemoveNoiseOperationView : UserControl
    {
        public FilterRemoveNoiseOperationViewModel OperationViewModel { get; }

        public FilterRemoveNoiseOperationView(FilterRemoveNoiseOperationViewModel operationViewModel)
        {
            this.OperationViewModel = operationViewModel;
            this.DataContext = this.OperationViewModel;
            InitializeComponent();
        }
    }
}
