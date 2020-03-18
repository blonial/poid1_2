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
    /// Logika interakcji dla klasy NegativeOperationView.xaml
    /// </summary>
    public partial class NegativeOperationView : UserControl
    {
        public OperationViewModel OperationViewModel { get; }

        public NegativeOperationView(WorkspaceViewModel workspaceViewModel)
        {
            this.OperationViewModel = new NegativeOperationViewModel(workspaceViewModel);
            this.DataContext = this.OperationViewModel;
            InitializeComponent();
        }
    }
}
