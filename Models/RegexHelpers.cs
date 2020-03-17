using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace poid.Models
{
    public static class RegexHelpers
    {
        public static readonly Regex NumberFormat = new Regex("[0-9]+");
    }
}
