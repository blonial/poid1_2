using poid.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace poid.Models.Interfaces
{
    public interface IOperation
    {
        #region Properties

        string OperationName { get; }

        UserControl View { get; }

        #endregion
    }
}
