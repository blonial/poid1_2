using poid.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poid.Models
{
    public class Workspace
    {
        #region Properties

        public int Index { get; }

        public WorkspaceView View { get; }

        #endregion

        #region Constructors

        public Workspace(int index)
        {
            this.Index = index;
            this.View = new WorkspaceView(index);
        }

        #endregion
    }
}
