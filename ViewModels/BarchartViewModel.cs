using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poid.ViewModels
{
    public class BarchartViewModel
    {
        #region Properties

        public SeriesCollection Series { get; }
        
        public ICollection<string> Labels { get; }

        public Func<double, string> Formatter { get; } = value => value.ToString();

        #endregion

        #region Constructors

        public BarchartViewModel(SeriesCollection series, ICollection<string> labels)
        {
            this.Series = series;
            this.Labels = labels;
        }

        #endregion
    }
}
