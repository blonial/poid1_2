using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poid.Models
{
    public class Pixel
    {
        #region Properties

        public readonly Color Color;

        public readonly int X;

        public readonly int Y;

        #endregion

        #region Constructors

        public Pixel(Color color, int x, int y)
        {
            this.Color = color;
            this.X = x;
            this.Y = y;
        }

        #endregion
    }
}
