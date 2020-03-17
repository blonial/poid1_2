using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using poid.Models.Interfaces;
using poid.ViewModels;

namespace poid.Models
{
    public abstract class Operation : IOperation
    {
        #region Properties

        public string OperationName { get; }

        public UserControl View { get; }

        #endregion

        #region Constructors

        public Operation(string operationName, UserControl view)
        {
            this.OperationName = operationName;
            this.View = view;
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return this.OperationName;
        }

        #endregion
    }
}
