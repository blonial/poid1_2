using poid.Models;
using poid.ViewModels;
using poid.ViewModels.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Logika interakcji dla klasy BrightnessChangeOperationView.xaml
    /// </summary>
    public partial class BrightnessChangeOperationView : UserControl
    {
        public OperationViewModel OperationViewModel { get; }

        public BrightnessChangeOperationView(WorkspaceViewModel workspaceViewModel)
        {
            this.OperationViewModel = new BrightnessChangeOperationViewModel(workspaceViewModel);
            this.DataContext = this.OperationViewModel;
            InitializeComponent();
        }
    }
}
