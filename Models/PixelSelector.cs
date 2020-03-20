using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poid.Models
{
    public static class PixelSelector
    {
        #region Properties

        public static Func<Color, int> Red { get; } = color => color.R;

        public static Func<Color, int> Green { get; } = color => color.G;

        public static Func<Color, int> Blue { get; } = color => color.B;

        #endregion
    }
}
